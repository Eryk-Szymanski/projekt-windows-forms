using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class ToolBoxManager
    {
        public Pen pen;
        public int penSize = 5;
        public Color penColor = Color.Black;
        Color rubberColor = Color.White;
        Color canvasColor = Color.White;
        Bitmap brushImage = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + "/../../../images/brush.png");
        public tools currentTool { get; set; }
        ToolStripButton penColorButton { get; set; }

        public ToolBoxManager(ToolStripButton button)
        {
            currentTool = tools.pencil;
            pen = createPen(currentTool, penSize, penColor);
            penColorButton = button;
            penColorButton.BackColor = penColor;
        }

        public Pen createPen(tools newPenType, int newPenSize, Color color)
        {
            Brush? brush = null;
            switch (newPenType)
            {
                case tools.brush:
                    brush = new TextureBrush(brushImage);
                    break;
                case tools.pencil:
                    brush = new SolidBrush(color);
                    break;
                case tools.spray:
                    brush = new HatchBrush(HatchStyle.Cross, color, canvasColor);
                    break;
                case tools.rubber:
                    brush = new SolidBrush(rubberColor);
                    break;
                default:
                    break;
            }
            Pen newPen = new Pen(brush, newPenSize);
            newPen.StartCap = newPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            return newPen;
        }

        public void selectTool(ToolStripButton button, TrackBar penSizeBar)
        {
            switch (button.Name)
            {
                case "brushButton":
                    currentTool = tools.brush;
                    break;
                case "pencilButton":
                    currentTool = tools.pencil;
                    break;
                case "sprayButton":
                    currentTool = tools.spray;
                    break;
                case "rubberButton":
                    currentTool = tools.rubber;
                    break;
                case "textBoxerButton":
                    currentTool = tools.textBoxer;
                    break;
                default:
                    break;
            }
            if (currentTool != tools.textBoxer)
            {
                pen = createPen(currentTool, penSize, penColor);
                penSizeBar.Enabled = true;
            }
            else
            {
                penSizeBar.Enabled = false;
            }
        }

        public void selectColor()
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = false;
            colorDialog.ShowHelp = true;
            colorDialog.Color = penColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    penColor = colorDialog.Color;
                    penColorButton.BackColor = penColor;
                    if (currentTool == tools.pencil || currentTool == tools.spray)
                        pen.Color = penColor;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        public void changePenSize(TrackBar trackBar)
        {
            penSize = trackBar.Value;
            pen = createPen(currentTool, penSize, penColor);
        }
    }
}
