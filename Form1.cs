using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OMA2SVG
{
    public partial class Form1 : Form
    {
        Dictionary<string, (string path, string newName)> filePaths;
        public Form1()
        {
            InitializeComponent();
            filePaths = new Dictionary<string, (string path, string newName)>();

            this.Load += ApplySettings;
            //this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void fileRenamingGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void fileRenamingGridView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string path in files)
                {
                    if (File.Exists(path))
                        addFileToDictionary(path);
                    else if (Directory.Exists(path))
                    {
                        var listFiles = searchDirectoryForOMAFiles(new DirectoryInfo(path), 5);
                        foreach (var file in listFiles)
                            addFileToDictionary(file.FullName);
                    }
                }
            }
        }

        private List<FileInfo> searchDirectoryForOMAFiles(DirectoryInfo parent, int maxDepth)
        {
            var files = new List<FileInfo>();

            files.AddRange(parent.GetFiles("*.OMA"));

            if (maxDepth <= 1) return files;

            var directories = parent.GetDirectories();
            foreach (var d in directories)
                files.AddRange(searchDirectoryForOMAFiles(d, maxDepth - 1));

            return files;
        }

        private void addFileToDictionary(string path)
        {
            if (!filePaths.Values.Select(f => f.path).Contains(path) && path.ToLower().EndsWith(".oma"))
            {
                string filename = path.Split('\\').Last();

                int n = 1;
                string uniqueName = filename;
                while (filePaths.ContainsKey(uniqueName))
                {
                    uniqueName = filename.Split('.')[0] + "_other_" + n + '.' + filename.Split('.')[1];
                    n++;
                }

                fileRenamingGridView.Rows.Add(uniqueName, uniqueName);
                filePaths.Add(uniqueName, (path, uniqueName));
            }
        }

        private void deleteSelected_Click(object sender, EventArgs e)
        {
            List<int> rowsToDelete = new List<int>();

            foreach (DataGridViewCell cell in fileRenamingGridView.SelectedCells)
                if (!rowsToDelete.Contains(cell.RowIndex))
                    rowsToDelete.Add(cell.RowIndex);

            rowsToDelete = rowsToDelete.OrderByDescending(rowIndex => rowIndex).ToList();
            foreach (var rowIndex in rowsToDelete)
            {
                if (fileRenamingGridView.Rows[rowIndex].Cells[0].Value != null)
                {
                    filePaths.Remove(fileRenamingGridView.Rows[rowIndex].Cells[0].Value.ToString());
                    fileRenamingGridView.Rows.RemoveAt(rowIndex);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                deleteSelected_Click(sender, e);
            if (e.KeyCode == Keys.Enter)
                convertBtn_Click(sender, e);
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            Dictionary<string, (float LPerimeter, float RPerimeter)> perimetersDict = new Dictionary<string, (float LPerimeter, float RPerimeter)>();
            for (int r = 0; r < fileRenamingGridView.Rows.Count; r++)
            {
                if (fileRenamingGridView.Rows[r].Cells[0].Value != null)
                {
                    string oldName = fileRenamingGridView.Rows[r].Cells[0].Value.ToString();
                    string newName = fileRenamingGridView.Rows[r].Cells[1].Value.ToString();

                    filePaths[oldName] = (filePaths[oldName].path, newName);
                }
            }

            foreach (var kvp in filePaths)
            {
                string folderPath = newFolderPath.Text;
                if (folderPath.Length == 0)
                {
                    List<string> s = kvp.Value.path.Split('\\').ToList();
                    s.RemoveRange(s.Count - 1, 1);
                    folderPath = String.Join('\\', s.ToArray());
                }

                var filename = kvp.Value.newName;
                if (filename.ToLower().EndsWith(".oma"))
                {
                    var filenameSplit = kvp.Value.newName.Split('.');
                    filenameSplit = filenameSplit.Take(filenameSplit.Length - 1).ToArray();
                    filename = String.Join('.', filenameSplit);
                }

                var newPerimeters = Convert(kvp.Value.path, folderPath, filename);
                perimetersDict[kvp.Key] = newPerimeters;
            }

            for (int r = 0; r < fileRenamingGridView.Rows.Count; r++)
            {
                if (fileRenamingGridView.Rows[r].Cells[0].Value != null)
                {
                    string oldName = fileRenamingGridView.Rows[r].Cells[0].Value.ToString();
                    fileRenamingGridView.Rows[r].Cells[2].Value = perimetersDict[oldName].LPerimeter.ToString();
                    fileRenamingGridView.Rows[r].Cells[3].Value = perimetersDict[oldName].RPerimeter.ToString();
                }
            }
        }

        (float LPerimeter, float RPerimeter) Convert(string inputPath, string outputPath, string filename)
        {
            (float LPerimeter, float RPerimeter) perimeters;

            float rescale = float.Parse(rescaleFactor.Text);
            float viewPortWidth = float.Parse(ViewPortWidth.Text);
            float viewPortHeight = float.Parse(ViewPortHeight.Text);
            int nbPointsPerDrill = int.Parse(nbPtHoles.Text);

            if (!File.Exists(inputPath)) return (0, 0);

            #region READ
            FileStream obj = new FileStream(inputPath, FileMode.Open);
            StreamReader reader = new StreamReader(obj);

            obj.Position = 0;
            reader.DiscardBufferedData();

            (float x, float y, float z)[][] pts = new (float x, float y, float z)[2][];
            pts[0] = new (float x, float y, float z)[0]; //RightLens
            pts[1] = new (float x, float y, float z)[0]; //LeftLens

            float[] ZTILT = new float[2];
            float MPD = 75;
            float DBL = -1;
            float[] FED = new float[2];
            float[] FEDAX = new float[2];
            string FMFR = "";
            string FRAM = "";
            List<(float x, float y, float size)> DRILLER = new List<(float x, float y, float size)>();
            List<(float x, float y, float size)> DRILLEL = new List<(float x, float y, float size)>();

            string line = "";

            int count = 0;

            while ((line = reader.ReadLine()) != null && count <= 100000)
            {
                #region ENTETE
                if (line.StartsWith("ZTILT="))
                {
                    string[] a = line.Remove(0, 6).Split(';');
                    if (a.Length > 1)
                    {
                        ZTILT[0] = float.Parse(a[0]);
                        ZTILT[1] = float.Parse(a[1]);
                    }
                    else
                    {
                        ZTILT[0] = float.Parse(a[0]);
                        ZTILT[1] = float.Parse(a[0]);
                    }
                }
                if (line.StartsWith("MPD="))
                    MPD = float.Parse(line.Remove(0, 4));
                if (line.StartsWith("DBL="))
                    DBL = float.Parse(line.Remove(0, 4));
                if (line.StartsWith("FED="))
                {
                    string[] a = line.Remove(0, 4).Split(';');
                    if (a.Length > 1)
                    {
                        FED[0] = float.Parse(a[0]);
                        FED[1] = float.Parse(a[1]);
                    }
                    else
                    {
                        FED[0] = float.Parse(a[0]);
                        FED[1] = float.Parse(a[0]);
                    }
                }
                if (line.StartsWith("FEDAX="))
                {
                    string[] a = line.Remove(0, 6).Split(';');
                    if (a.Length > 1)
                    {
                        FEDAX[0] = float.Parse(a[0]);
                        FEDAX[1] = float.Parse(a[1]);
                    }
                    else
                    {
                        FEDAX[0] = float.Parse(a[0]);
                        FEDAX[1] = float.Parse(a[0]);
                    }
                }
                if (line.StartsWith("FMFR="))
                    FMFR = line.Remove(0, 5);
                if (line.StartsWith("FNAM=") || line.StartsWith("FRAM="))
                    FRAM = line.Remove(0, 5);
                if (line.StartsWith("DRILLE="))
                {
                    var splits = line.Split(';');

                    if (splits.Length > 5)
                    {
                        float size = 0;
                        float.TryParse(splits[4], out size);

                        var add = (float.Parse(splits[2]), float.Parse(splits[3]), size);

                        if (splits[0].EndsWith('R'))
                            DRILLER.Add(add);
                        else if (splits[0].EndsWith('L'))
                            DRILLEL.Add(add);
                        else
                        {
                            DRILLER.Add(add);
                            DRILLEL.Add(add);
                        }
                    }

                }
                #endregion ENTETE

                #region Content
                if (line.StartsWith("TRCFMT="))
                {
                    bool right = (line.Split(';')[3] == "R");
                    int pointCount = int.Parse(line.Split(';')[1]);
                    pts[right ? 0 : 1] = pts[right ? 0 : 1].Length == 0 ? new (float x, float y, float z)[pointCount] : pts[right ? 1 : 0];
                    int index = 0;

                    while (index < pointCount && (line = reader.ReadLine()) != null)
                    {
                        foreach (string s in line.Split(';'))
                        {
                            float hyp = (float.Parse(s.Trim(new char[] { 'R', '=' })) /*+ float.Parse(txtOversize.Text) * 100*/) / 100f;
                            pts[right ? 0 : 1][index].x = (float)(hyp * Math.Cos(((index == 0 ? 0 : -360f / pts[right ? 0 : 1].Length * index) - ZTILT[right ? 0 : 1] / 2) * Math.PI / 180f));
                            pts[right ? 0 : 1][index].y = (float)(hyp * Math.Sin(((index == 0 ? 0 : -360f / pts[right ? 0 : 1].Length * index) - ZTILT[right ? 0 : 1] / 2) * Math.PI / 180f));

                            index++;
                        }
                    }
                }

                if (line.StartsWith("ZFMT"))
                {
                    bool right = (line.Split(';')[3] == "R");
                    int pointCount = int.Parse(line.Split(';')[1]);
                    pts[right ? 0 : 1] = pts[right ? 0 : 1].Length == 0 ? new (float x, float y, float z)[pointCount] : pts[right ? 0 : 1];
                    int index = 0;

                    while (index < pointCount && (line = reader.ReadLine()) != null)
                        foreach (string s in line.Split(';'))
                            pts[right ? 0 : 1][index++].z = float.Parse(s.Trim(new char[] { 'Z', '=' })) / 100f;
                }
                #endregion Content

                count++;
            }

            obj.Close();
            reader.Close();
            #endregion READ

            #region WRITE
            //string FileName = obj.Name.Split('\\')[obj.Name.Split('\\').Length-1].Split('.')[0];
            string FileName = !string.IsNullOrEmpty(filename) ? filename :
                (FMFR + "_" + FRAM) == "_" ? obj.Name.Split('\\')[obj.Name.Split('\\').Length - 1].Split('.')[0] :
                FMFR != "" && FRAM != "" ? FMFR + "_" + FRAM :
                FMFR == "" ? FRAM : FMFR;

            bool HasRight = pts[0].Length > 0;
            bool HasLeft = pts[1].Length > 0;

            perimeters.LPerimeter = WriteLens(pts[HasLeft ? 1 : 0], DRILLEL,
                outputPath + "\\" + FileName + "_Left" + ".svg",
                viewPortWidth, viewPortHeight, rescale, nbPointsPerDrill,
                rotationLens: float.Parse(rotationLeftLens.Text),
                xIsReversed: !HasLeft);
            perimeters.LPerimeter *= rescale;

            perimeters.RPerimeter = WriteLens(pts[HasRight ? 0 : 1], DRILLER,
                outputPath + "\\" + FileName + "_Right" + ".svg",
                viewPortWidth, viewPortHeight, rescale, nbPointsPerDrill,
                rotationLens: float.Parse(rotationRightLens.Text),
                xIsReversed: !HasRight);
            perimeters.RPerimeter *= rescale;

            #endregion WRITE

            return perimeters;
        }

        public float WriteLens(
            (float x, float y, float z)[] lensPoints, List<(float x, float y, float size)> drillPoints,
            string filePath,
            float viewPortWidth, float viewPortHeight, float rescale, int nbDrillPoint,
            float rotationLens = 0, float rotationDrill = 0,
            bool xIsReversed = false)
        {
            float perimeter = 0;

            FileStream newSvgFile = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(newSvgFile);
            DrawSvg svgDrawer = new DrawSvg(sw);

            float MaxX = lensPoints.Max(p => xIsReversed ? -p.x : p.x);
            float MinX = lensPoints.Min(p => xIsReversed ? -p.x : p.x);
            float MaxY = lensPoints.Max(p => p.y);
            float MinY = lensPoints.Min(p => p.y);

            float decalX = (viewPortWidth - rescale * (MaxX - MinX)) / 2f;
            float decalY = (viewPortHeight - rescale * (MaxY - MinY)) / 2f;

            sw.WriteLine("<svg width=\"" + viewPortWidth + "mm\" height=\"" + viewPortHeight + "mm\" viewBox =\"0 0 " + viewPortWidth + " " + viewPortHeight + "\" xmlns=\"http://www.w3.org/2000/svg\">");


            #region Debugging
            /*sw.WriteLine(svgDrawPath(new (float x, float y, float z)[]
            {
                (decalX, decalY, 0),
                ((viewPortWidth - decalX), decalY, 0),
                ((viewPortWidth - decalX), (viewPortHeight - decalY), 0),
                (decalX, (viewPortHeight - decalY), 0)
            }, "#a83287", 1, indentation:1).s); // purple : frame is in here

            sw.WriteLine(svgDrawPath(new (float x, float y, float z)[]
            {
                (0, 0, 0),
                (viewPortWidth, 0, 0),
                (viewPortWidth, viewPortHeight, 0),
                (0, viewPortHeight, 0)
            }, "#2de0ab", 1, indentation:1).s); // aqua : svg viewport*/
            #endregion Debugging

            #region Lens
            sw.WriteLine("\n<!-- Lens-->\n");

            svgDrawer.WriteTransformStart(((-MinX) * rescale + decalX), ((-MinY) * rescale + decalY), rescale, rescale, rotationLens, 1);

            var svgPathPerimeter = svgDrawer.DrawPath(lensPoints, "#ff0000", 0.1f, indentation: 2);
            perimeter = svgPathPerimeter;

            svgDrawer.WriteTransformEnd(1);
            #endregion Lens

            #region Drill Holes
            sw.WriteLine("\n<!-- Drill Holes -->\n");

            svgDrawer.WriteTransformStart(((-MinX) * rescale + decalX), ((-MinY) * rescale + decalY), rescale, rescale, rotationDrill, 1);

            foreach (var drill in drillPoints)
            {
                float holeSize = 0;
                float.TryParse(drillHoleSize.Text, out holeSize);
                holeSize = holeSize != 0 ? holeSize : drill.size;

                if (holeSize != 0)
                    svgDrawer.DrawDrillHole(drill.x, -drill.y, nbDrillPoint, holeSize, applyRescaleToDrill.Checked ? 1 : 1 / rescale);
            }

            svgDrawer.WriteTransformEnd(1);
            #endregion Drill Holes

            sw.WriteLine("</svg>");

            sw.Close();
            newSvgFile.Close();

            return perimeter;
        }

        private void newFolderPath_DragEnter(object sender, DragEventArgs e)
        {
            DragDropEffects effects = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                if (Directory.Exists(path))
                    effects = DragDropEffects.Copy;
            }

            e.Effect = effects;
        }
        public float Round(float num) => (float)Math.Round(num, 4);
        private void newFolderPath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                newFolderPath.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.applyRescaleToHoles = applyRescaleToDrill.Checked;
            Properties.Settings.Default.nbOfPtPerHole = int.Parse(nbPtHoles.Text);
            Properties.Settings.Default.viewPortWidth = int.Parse(ViewPortWidth.Text);
            Properties.Settings.Default.viewPortHeight = int.Parse(ViewPortHeight.Text);
            Properties.Settings.Default.rescale = float.Parse(rescaleFactor.Text);
            Properties.Settings.Default.drilldiameter = float.Parse(drillHoleSize.Text);
            Properties.Settings.Default.folderPath = newFolderPath.Text;

            Properties.Settings.Default.Save();
        }

        private void ApplySettings(Object sender, EventArgs e)
        {
            applyRescaleToDrill.Checked = Properties.Settings.Default.applyRescaleToHoles;
            nbPtHoles.Text = Properties.Settings.Default.nbOfPtPerHole.ToString();
            ViewPortWidth.Text = Properties.Settings.Default.viewPortWidth.ToString();
            ViewPortHeight.Text = Properties.Settings.Default.viewPortHeight.ToString();
            rescaleFactor.Text = Properties.Settings.Default.rescale.ToString();
            drillHoleSize.Text = Properties.Settings.Default.drilldiameter.ToString();
            newFolderPath.Text = Properties.Settings.Default.folderPath;
        }
    }

    public class FormSettings
    {
        public FormSettings() { }

        public FormSettings(string s)
        {
            string[] splits = s.Split('|');
            applyRescaleToHoles = bool.Parse(splits[0]);
            nbOfPtPerHole = int.Parse(splits[1]);
            viewPortWidth = int.Parse(splits[2]);
            viewPortHeight = int.Parse(splits[3]);
            rescale = float.Parse(splits[4]);
            drilldiameter = float.Parse(splits[5]);
            folderPath = splits[6];
        }

        public bool applyRescaleToHoles;
        public int nbOfPtPerHole;
        public int viewPortWidth;
        public int viewPortHeight;
        public float rescale;
        public float drilldiameter;
        public string folderPath;

        public override string ToString()
        {
            return applyRescaleToHoles + "|"
                + nbOfPtPerHole + "|"
                + viewPortWidth + "|"
                + viewPortHeight + "|"
                + rescale + "|"
                + drilldiameter + "|"
                + folderPath;
        }
    }

    public class DrawSvg
    {
        private StreamWriter _sw;
        public DrawSvg(StreamWriter sw)
        {
            _sw = sw;
        }

        public float Round(float num) => (float)Math.Round(num, 4);

        #region Drill Hole
        public void DrawDrillHole(float xCenter, float yCenter, int nbPtHoles, float holeSize, float rescale = 1)
        {
            float radius = holeSize / 2;

            //string returnString = "<circle cx=\"" + xCenter + "\" cy=\"" + yCenter + "\" r=\"1\" />";
            string returnString = "\t\t<path fill=\"none\" stroke=\"#ff0000\" stroke-width=\"0.1\"\n\t\t\td=\"M ";

            for (int i = 0; i < nbPtHoles; i++)
            {
                float angle = (float)i / (float)nbPtHoles * 360 * (float)Math.PI / 180;
                float x = (float)Math.Cos(angle) * radius * rescale;
                float y = (float)Math.Sin(angle) * radius * rescale;

                returnString += Round(xCenter + x) + "," + Round(yCenter + y) + " L ";
            }

            returnString += Round(xCenter + radius * rescale) + "," + Round(yCenter) + " z\" />";
            _sw.WriteLine(returnString);
        }
        #endregion Drill Hole

        #region Circle
        public void DrawCircle(float x, float y, float radius, string color)
            => _sw.WriteLine("<circle cx=\"" + x + "\" cy =\"" + y + "\" r=\"" + radius + "\" stroke=\"" + color + "\" fill=\"" + color + "\" />");
        #endregion Circle

        #region Path
        private string svgPathStart(string hexColor, float strokeWidth) => "<path fill=\"none\" stroke=\"" + hexColor + "\" stroke-width=\"" + strokeWidth + "\"\n";
        private string svgPathEnd() => " z\" />\n";
        private string svgPathAddPoint(float x, float y) => Round(x) + "," + Round(y) + " L ";
        public float DrawPath((float x, float y, float z)[] pts, string color, float strokeWidth,
            float decalX = 0, float decalY = 0, int indentation = 1)
        {
            string returnString = new string('\t', indentation) + svgPathStart(color, strokeWidth); // <path ...
            returnString += new string('\t', indentation) + "\td=\"M "; // d="M ...

            float perimeter = 0;
            var prevPts = pts[0];

            returnString += svgPathAddPoint(pts[0].x + decalX, pts[0].y + decalY);

            foreach (var p in pts.Skip(1))
            {
                returnString += svgPathAddPoint(p.x + decalX, p.y + decalY); // x,y L ...
                perimeter += (float)Math.Sqrt((prevPts.x - p.x) * (prevPts.x - p.x) + (prevPts.y - p.y) * (prevPts.y - p.y));
                prevPts = p;
            }

            perimeter += (float)Math.Sqrt((prevPts.x - pts[0].x) * (prevPts.x - pts[0].x) + (prevPts.y - pts[0].y) * (prevPts.y - pts[0].y));

            returnString += Round(pts[0].x + decalX) + "," + Round(pts[0].y + decalY); // finish the loop
            returnString += svgPathEnd(); // z" />

            _sw.WriteLine(returnString);

            return perimeter;
        }
        #endregion Path

        public void WriteTransformStart(
            float translateX = 1, float translateY = 1,
            float scaleX = 1, float scaleY = 1,
            float rotation = 0,
            int indentation = 0)
            => _sw.WriteLine(new string('\t', indentation) + "<g transform=\"" +
                "translate(" + translateX + " " + translateY + ") " +
                "rotate(" + rotation + " 0 0) " +
                "scale(" + scaleX + " " + scaleY + ")\">");

        public void WriteTransformEnd(int indentation = 0) => _sw.WriteLine(new string('\t', indentation) + "</g>");
    }
}
