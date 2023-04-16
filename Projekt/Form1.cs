using System.Text;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Drawing;
using System.Xml.Linq;
using System.Numerics;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;
using static System.Windows.Forms.LinkLabel;
using System.Windows.Forms.Design;
using System.Diagnostics.Tracing;

namespace Projekt
{
    public partial class Form1 : Form
    {
        public delegate void displayLayerDelegate(Layer layer);
        public delegate Pen createPenDelegate(tools penType, int newPenSize, Color color);
        
        Graphics graphics;
        int x = -1;
        int y = -1;
        bool moving = false;
        Pen pen;
        int penSize = 5;
        Color canvasColor = Color.White;
        Color penColor = Color.Black;
        Color rubberColor = Color.White;
        Bitmap brushImage = (Bitmap) Image.FromFile(Directory.GetCurrentDirectory() + "/images/brush.png");
        tools currentTool { get; set; }
        List<Layer> layers { get; set; }
        Layer currentLayer { get; set; }
        SerializableLine currentLine { get; set; }

        public Form1()
        {
            InitializeComponent();

            // Ustawienie wartoœci pocz¹tkowych
            layers = new List<Layer>();
            currentLayer = new Layer();
            currentLayer.Name = "Warstwa 1";
            currentLayer.TextBoxes = new List<SerializableTextBox>();
            currentLayer.Lines = new List<SerializableLine>();
            layers.Add(currentLayer);

            // Wartoœci dla rysowania
            pictureBox1.BackColor = canvasColor;
            graphics = pictureBox1.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            currentTool = tools.pencil;
            pen = createPen(currentTool, penSize, penColor);
            penColorButton.BackColor = penColor;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentTool != tools.textBoxer)
            {
                moving = true;
                x = e.X;
                y = e.Y;
                pictureBox1.Cursor = Cursors.Cross;
                currentLine = new SerializableLine();
                currentLine.lineParts = new List<SerializableLinePart>();
                currentLine.A = penColor.A;
                currentLine.R = penColor.R;
                currentLine.G = penColor.G;
                currentLine.B = penColor.B;
                currentLine.Tool = currentTool;
                currentLine.LineSize = penSize;
            } else
            {
                SerializableTextBox newTextBoxToSerialize = new SerializableTextBox();
                newTextBoxToSerialize.Name = "nextTextBox" + currentLayer.TextBoxes.Count.ToString();
                newTextBoxToSerialize.Location = e.Location;
                newTextBoxToSerialize.MultiLine = true;
                newTextBoxToSerialize.BackColor = canvasColor;
                newTextBoxToSerialize.BorderStyle = BorderStyle.None;
                currentLayer.TextBoxes.Add(newTextBoxToSerialize);
                createTextBoxFromSerializable(newTextBoxToSerialize);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentTool != tools.textBoxer)
            {
                if (moving && x != -1 && y != -1)
                {
                    SerializableLinePart linePart = new SerializableLinePart();
                    linePart.Pt1 = new Point(x, y);
                    linePart.Pt2 = e.Location;
                    currentLine.lineParts.Add(linePart);
                    graphics.DrawLine(pen, new Point(x, y), e.Location);
                    x = e.X;
                    y = e.Y;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentTool != tools.textBoxer)
            {
                moving = false;
                x = -1;
                y = -1;
                pictureBox1.Cursor = Cursors.Default;
                currentLayer.Lines.Add(currentLine);
            }
        }

        private void selectTool(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            switch(button.Name)
            {
                case "brushButton":
                    currentTool = tools.brush;
                    break;
                case "pencilButton":
                    currentTool = tools.pencil;
                    break;
                case "sprayButton":
                    currentTool= tools.spray;
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
            if (currentTool != tools.textBoxer) { 
                pen = createPen(currentTool, penSize, penColor);
                penSizeBar.Enabled = true;
            } else
            {
                penSizeBar.Enabled = false;
            }
        }

        private Pen createPen(tools newPenType, int newPenSize, Color color)
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
        private void selectColor(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = false;
            colorDialog.ShowHelp = true;
            colorDialog.Color = penColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    penColor = colorDialog.Color;
                    pen.Color = penColor;
                    penColorButton.BackColor = penColor;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void drawLine(SerializableLine line)
        {
            Pen newPen = (Pen)Invoke(new createPenDelegate(createPen), line.Tool, line.LineSize, Color.FromArgb(line.A, line.R, line.G, line.B));
            foreach (SerializableLinePart linePart in line.lineParts)
            {
                graphics.DrawLine(newPen, linePart.Pt1, linePart.Pt2);
            }
        }
        private void createTextBoxFromSerializable(SerializableTextBox textBox)
        {
            TextBox newTextBox = new TextBox();
            newTextBox.Name = textBox.Name;
            newTextBox.Text = textBox.Text;
            newTextBox.Location = textBox.Location;
            newTextBox.Multiline = textBox.MultiLine;
            newTextBox.BackColor = textBox.BackColor;
            newTextBox.BorderStyle = textBox.BorderStyle;
            pictureBox1.Controls.Add(newTextBox);
        }
        private void displayLayer(Layer layer)
        {
            foreach (SerializableTextBox textBox in layer.TextBoxes)
            {
                createTextBoxFromSerializable(textBox);
            }
            foreach (SerializableLine line in layer.Lines)
            {
                drawLine(line);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                layers = FileManager.loadFromJson();
                var watek = new Thread(new ThreadStart(() => {
                foreach (Layer layer in layers)
                {
                    Invoke(new displayLayerDelegate(displayLayer), layer);
                }
                }));
                watek.IsBackground = true;
                watek.Start();
                currentLayer = new Layer();
                currentLayer.Name = "Warstwa 1";
                currentLayer.TextBoxes = new List<SerializableTextBox>();
                currentLayer.Lines = new List<SerializableLine>();
                layers.Add(currentLayer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            FileManager.saveToJson(layers, pictureBox1);
        }

        private void penSizeBar_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            penSize = trackBar.Value;
            pen = createPen(currentTool, penSize, penColor);
        }
    }
}