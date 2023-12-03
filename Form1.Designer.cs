
namespace OMA2SVG
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            convertBtn = new System.Windows.Forms.Button();
            fileRenamingGridView = new System.Windows.Forms.DataGridView();
            FileInput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            FileOutput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            LPerimeterCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            RPerimeterCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            deleteSelected = new System.Windows.Forms.Button();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            getFolder_btn = new System.Windows.Forms.Button();
            ViewPortWidth = new System.Windows.Forms.TextBox();
            ViewPortHeight = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            nbPtHoles = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            drillHoleSize = new System.Windows.Forms.TextBox();
            newFolderPath = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            rescaleFactor = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            applyRescaleToDrill = new System.Windows.Forms.CheckBox();
            label5 = new System.Windows.Forms.Label();
            rotationRightLens = new System.Windows.Forms.TextBox();
            rotationLeftLens = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)fileRenamingGridView).BeginInit();
            SuspendLayout();
            // 
            // convertBtn
            // 
            convertBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            convertBtn.Location = new System.Drawing.Point(12, 12);
            convertBtn.Name = "convertBtn";
            convertBtn.Size = new System.Drawing.Size(168, 43);
            convertBtn.TabIndex = 9;
            convertBtn.Text = "CONVERT (enter)";
            toolTip1.SetToolTip(convertBtn, "enter");
            convertBtn.UseVisualStyleBackColor = true;
            convertBtn.Click += convertBtn_Click;
            // 
            // fileRenamingGridView
            // 
            fileRenamingGridView.AllowDrop = true;
            fileRenamingGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            fileRenamingGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            fileRenamingGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            fileRenamingGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { FileInput, FileOutput, LPerimeterCol, RPerimeterCol });
            fileRenamingGridView.Location = new System.Drawing.Point(12, 186);
            fileRenamingGridView.Name = "fileRenamingGridView";
            fileRenamingGridView.RowHeadersWidth = 51;
            fileRenamingGridView.RowTemplate.Height = 25;
            fileRenamingGridView.Size = new System.Drawing.Size(776, 391);
            fileRenamingGridView.TabIndex = 0;
            fileRenamingGridView.TabStop = false;
            fileRenamingGridView.DragDrop += fileRenamingGridView_DragDrop;
            fileRenamingGridView.DragEnter += fileRenamingGridView_DragEnter;
            // 
            // FileInput
            // 
            FileInput.HeaderText = "Input";
            FileInput.MinimumWidth = 6;
            FileInput.Name = "FileInput";
            FileInput.ReadOnly = true;
            // 
            // FileOutput
            // 
            FileOutput.HeaderText = "Ouput";
            FileOutput.MinimumWidth = 6;
            FileOutput.Name = "FileOutput";
            // 
            // LPerimeterCol
            // 
            LPerimeterCol.HeaderText = "L Perimeter ";
            LPerimeterCol.MinimumWidth = 6;
            LPerimeterCol.Name = "LPerimeterCol";
            LPerimeterCol.ReadOnly = true;
            // 
            // RPerimeterCol
            // 
            RPerimeterCol.HeaderText = "R Perimeter ";
            RPerimeterCol.MinimumWidth = 6;
            RPerimeterCol.Name = "RPerimeterCol";
            RPerimeterCol.ReadOnly = true;
            // 
            // deleteSelected
            // 
            deleteSelected.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            deleteSelected.Location = new System.Drawing.Point(12, 61);
            deleteSelected.Name = "deleteSelected";
            deleteSelected.Size = new System.Drawing.Size(168, 43);
            deleteSelected.TabIndex = 10;
            deleteSelected.Tag = "";
            deleteSelected.Text = "DELETE SELECTED (delete)";
            toolTip1.SetToolTip(deleteSelected, "del");
            deleteSelected.UseVisualStyleBackColor = true;
            deleteSelected.Click += deleteSelected_Click;
            // 
            // getFolder_btn
            // 
            getFolder_btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            getFolder_btn.Location = new System.Drawing.Point(12, 110);
            getFolder_btn.Name = "getFolder_btn";
            getFolder_btn.Size = new System.Drawing.Size(168, 43);
            getFolder_btn.TabIndex = 11;
            getFolder_btn.Tag = "";
            getFolder_btn.Text = "BROWSE FOR OUTPUT PATH";
            toolTip1.SetToolTip(getFolder_btn, "del");
            getFolder_btn.UseVisualStyleBackColor = true;
            // 
            // ViewPortWidth
            // 
            ViewPortWidth.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            ViewPortWidth.Location = new System.Drawing.Point(539, 3);
            ViewPortWidth.Name = "ViewPortWidth";
            ViewPortWidth.Size = new System.Drawing.Size(55, 23);
            ViewPortWidth.TabIndex = 3;
            ViewPortWidth.Text = "75";
            // 
            // ViewPortHeight
            // 
            ViewPortHeight.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            ViewPortHeight.Location = new System.Drawing.Point(539, 32);
            ViewPortHeight.Name = "ViewPortHeight";
            ViewPortHeight.Size = new System.Drawing.Size(55, 23);
            ViewPortHeight.TabIndex = 4;
            ViewPortHeight.Text = "55";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(488, 3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(43, 15);
            label1.TabIndex = 9;
            label1.Text = "width :";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(484, 32);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(47, 15);
            label2.TabIndex = 10;
            label2.Text = "height :";
            // 
            // nbPtHoles
            // 
            nbPtHoles.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            nbPtHoles.Location = new System.Drawing.Point(733, 3);
            nbPtHoles.Name = "nbPtHoles";
            nbPtHoles.Size = new System.Drawing.Size(55, 23);
            nbPtHoles.TabIndex = 6;
            nbPtHoles.Text = "360";
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(609, 3);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(118, 15);
            label6.TabIndex = 16;
            label6.Text = "nb of points for drill :";
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(598, 35);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(129, 15);
            label7.TabIndex = 17;
            label7.Text = "drill diameter (in mm) :";
            // 
            // drillHoleSize
            // 
            drillHoleSize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            drillHoleSize.Location = new System.Drawing.Point(733, 32);
            drillHoleSize.Name = "drillHoleSize";
            drillHoleSize.Size = new System.Drawing.Size(55, 23);
            drillHoleSize.TabIndex = 7;
            drillHoleSize.Text = "0";
            // 
            // newFolderPath
            // 
            newFolderPath.AllowDrop = true;
            newFolderPath.Location = new System.Drawing.Point(186, 121);
            newFolderPath.Name = "newFolderPath";
            newFolderPath.PlaceholderText = "Or drag and drop";
            newFolderPath.Size = new System.Drawing.Size(408, 23);
            newFolderPath.TabIndex = 12;
            newFolderPath.DragDrop += newFolderPath_DragDrop;
            newFolderPath.DragEnter += newFolderPath_DragEnter;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(450, 64);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(83, 15);
            label3.TabIndex = 21;
            label3.Text = "rescale factor :";
            // 
            // rescaleFactor
            // 
            rescaleFactor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            rescaleFactor.Location = new System.Drawing.Point(539, 61);
            rescaleFactor.Name = "rescaleFactor";
            rescaleFactor.Size = new System.Drawing.Size(55, 23);
            rescaleFactor.TabIndex = 5;
            rescaleFactor.Text = "1";
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(644, 64);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(75, 30);
            label4.TabIndex = 23;
            label4.Text = "apply rescale\r\nto drill holes";
            // 
            // applyRescaleToDrill
            // 
            applyRescaleToDrill.AutoSize = true;
            applyRescaleToDrill.Location = new System.Drawing.Point(733, 65);
            applyRescaleToDrill.Name = "applyRescaleToDrill";
            applyRescaleToDrill.Size = new System.Drawing.Size(15, 14);
            applyRescaleToDrill.TabIndex = 8;
            applyRescaleToDrill.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(245, 5);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(107, 15);
            label5.TabIndex = 25;
            label5.Text = "rotation right lens :";
            // 
            // rotationRightLens
            // 
            rotationRightLens.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            rotationRightLens.Location = new System.Drawing.Point(368, 3);
            rotationRightLens.Name = "rotationRightLens";
            rotationRightLens.Size = new System.Drawing.Size(55, 23);
            rotationRightLens.TabIndex = 1;
            rotationRightLens.Text = "0";
            // 
            // rotationLeftLens
            // 
            rotationLeftLens.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            rotationLeftLens.Location = new System.Drawing.Point(368, 32);
            rotationLeftLens.Name = "rotationLeftLens";
            rotationLeftLens.Size = new System.Drawing.Size(55, 23);
            rotationLeftLens.TabIndex = 2;
            rotationLeftLens.Text = "0";
            // 
            // label8
            // 
            label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(254, 34);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(99, 15);
            label8.TabIndex = 27;
            label8.Text = "rotation left lens :";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 590);
            Controls.Add(rotationRightLens);
            Controls.Add(rotationLeftLens);
            Controls.Add(ViewPortWidth);
            Controls.Add(ViewPortHeight);
            Controls.Add(rescaleFactor);
            Controls.Add(nbPtHoles);
            Controls.Add(drillHoleSize);
            Controls.Add(applyRescaleToDrill);
            Controls.Add(convertBtn);
            Controls.Add(deleteSelected);
            Controls.Add(getFolder_btn);
            Controls.Add(newFolderPath);
            Controls.Add(label8);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(fileRenamingGridView);
            KeyPreview = true;
            Name = "Form1";
            Text = "OMA to SVG";
            FormClosing += Form1_FormClosing;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)fileRenamingGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button convertBtn;
        private System.Windows.Forms.DataGridView fileRenamingGridView;
        private System.Windows.Forms.Button deleteSelected;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox ViewPortWidth;
        private System.Windows.Forms.TextBox ViewPortHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nbPtHoles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox drillHoleSize;
        private System.Windows.Forms.Button getFolder_btn;
        private System.Windows.Forms.TextBox newFolderPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn LPerimeterCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn RPerimeterCol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox rescaleFactor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox applyRescaleToDrill;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox rotationRightLens;
        private System.Windows.Forms.TextBox rotationLeftLens;
        private System.Windows.Forms.Label label8;
    }
}

