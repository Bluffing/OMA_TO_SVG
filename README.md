# OMA to SVG Converter

A tool to convert `.oma` files into `.svg` format with customizable options.

## Features

- Convert `.oma` files to `.svg` format.
- Batch processing: Drag and drop multiple `.oma` files or entire folders.
- Customizable output settings:
    - Rotation
    - Window size (width and height)
    - Rescale factor
    - Drill point configuration
- Option to rename output paths directly in the grid.

## How to Use

### 1. Choose the Output Folder

- Click the **"BROWSE FOR OUTPUT PATH"** button or drag and drop a folder.
- If left empty, the `.svg` files will be saved in the same folder as the `.oma` files.

### 2. Add Files

- Drag and drop the `.oma` file(s) you want to convert onto the grid.
- If you drop a folder, all `.oma` files within it will be added automatically.

### 3. Adjust Settings

Customize the following options for your output:
- **Rotation**: Set the clockwise rotation for each lens.
- **Width and Height**: Define the output window size.
- **Rescale Factor**: Adjust the scaling of the output.
- **Drill Point Number**: Specify the number of points for drill holes in the `.oma` file. If set to `0`, no drill holes will be drawn.
- **Apply Rescale to Drill Holes**: Choose whether to apply the rescale factor to drill holes.
- **Rename Output Path**: Modify the output column in the grid to rename the output file path.

### 4. Convert

- Click the **CONVERT** button to generate the `.svg` files.

## Notes

- Ensure the `.oma` files are properly formatted before conversion.
- The tool automatically handles batch processing for multiple files or folders.
