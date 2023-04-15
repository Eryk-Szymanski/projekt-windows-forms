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
using Projekt.classes;

namespace Projekt
{
    public partial class Form1 : Form
    {
        public delegate void createLineFromSerializableDelegate(SerializableLine line);
        public delegate void createColorDelegate(SerializableLine line);
        public delegate void drawLineDelegate(SerializableLine line);

        Graphics graphics;
        int x = -1;
        int y = -1;
        bool moving = false;
        Pen pen;
        Color canvasColor = Color.White;
        Color penColor = Color.Black;
        int penSize = 5;
        Bitmap brushImage = (Bitmap) Image.FromFile(Directory.GetCurrentDirectory() + "/images/brush.png");
        tools currentTool { get; set; }
        List<Layer> layers { get; set; }
        Layer currentLayer { get; set; }

        // Narzêdzia z lewego tool boxa
        public enum tools {
            brush,
            pencil,
            spray,
            rubber,
            textBoxer
        }
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
            SolidBrush solidBrush = new SolidBrush(penColor);
            pen = new Pen(penColor, penSize);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentTool != tools.textBoxer)
            {
                moving = true;
                x = e.X;
                y = e.Y;
                pictureBox1.Cursor = Cursors.Cross;
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
                    SerializableLine line = new SerializableLine();
                    line.Pt1 = new Point(x, y);
                    line.Pt2 = e.Location;
                    line.LineA = penColor.A;
                    line.LineR = penColor.R;
                    line.LineG = penColor.G;
                    line.LineB = penColor.B;
                    currentLayer.Lines.Add(line);
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
            }
        }

        private void selectTool(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            pen.Dispose();
            switch(button.Name)
            {
                case "brushButton":
                    currentTool = tools.brush;
                    TextureBrush textureBrush = new TextureBrush(brushImage);
                    textureBrush.WrapMode = WrapMode.Clamp;
                    pen = new Pen(textureBrush, penSize);
                    break;
                case "pencilButton":
                    currentTool = tools.pencil;
                    SolidBrush solidBrush = new SolidBrush(penColor);
                    pen = new Pen(solidBrush, penSize);
                    pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    break;
                case "sprayButton":
                    currentTool = tools.spray;
                    HatchBrush hatchBrush = new HatchBrush(HatchStyle.Cross, penColor, Color.White);
                    pen = new Pen(hatchBrush, penSize);
                    break;
                case "rubberButton":
                    currentTool = tools.rubber;
                    pen = new Pen(Color.White, penSize);
                    break;
                case "textBoxerButton":
                    currentTool = tools.textBoxer;
                    break;
                default:
                    break;
            }
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
                    if (sender.GetType() == typeof(ToolStripButton))
                    {
                        penColor = colorDialog.Color;
                        pen.Color = penColor;
                    } else if (sender.GetType() == typeof(ToolStripMenuItem))
                    {
                        canvasColor = colorDialog.Color;
                        pictureBox1.BackColor = canvasColor;
                        Task t = new Task(() => Console.WriteLine());
                        TimeSpan ts = TimeSpan.FromMilliseconds(150);
                        t.Wait(ts);
                        foreach (Layer layer in layers)
                        {
                            foreach (SerializableTextBox textBox in layer.TextBoxes)
                            {
                                textBox.BackColor = canvasColor;
                                TextBox box = pictureBox1.Controls.Find(textBox.Name, true)[0] as TextBox;
                                box.BackColor = canvasColor;
                            }
                            var watek = new Thread(new ThreadStart(() =>
                            {
                                foreach (SerializableLine line in layer.Lines)
                                {
                                    Invoke(new createLineFromSerializableDelegate(createLineFromSerializable), line);
                                }
                            }));
                            watek.IsBackground = true;
                            watek.Start();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void createLineFromSerializable(SerializableLine line)
        {
            var watek = new Thread(new ThreadStart(() =>
            {
                Invoke(new createColorDelegate(createColor), line);
                Invoke(new drawLineDelegate(drawLine), line);
            }));
            watek.IsBackground = true;
            watek.Start();
            
        }
        private void createColor(SerializableLine line)
        {
            pen.Color = Color.FromArgb(line.LineA, line.LineR, line.LineG, line.LineB);
        }
        private void drawLine(SerializableLine line)
        {
            graphics.DrawLine(pen, line.Pt1, line.Pt2);
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

        private void loadButton_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Json files (*.json)|*.json";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                }
            }

            try
            {
                layers = JsonSerializer.Deserialize<List<Layer>>(File.ReadAllText(filePath))!;
                foreach(Layer layer in layers)
                {
                    foreach(SerializableTextBox textBox in layer.TextBoxes)
                    {
                        createTextBoxFromSerializable(textBox);
                    }
                    var watek = new Thread(new ThreadStart(() =>
                    {
                        foreach (SerializableLine line in layer.Lines)
                        {
                            Invoke(new createLineFromSerializableDelegate(createLineFromSerializable), line);
                        }
                    }));
                    watek.IsBackground = true;
                    watek.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Json files (*.json)|*.json";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            var filePath = string.Empty;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
            }

            if (!String.IsNullOrEmpty(filePath))
            {
                try
                {
                    foreach(Layer layer in layers)
                    {
                        foreach(SerializableTextBox textBox in layer.TextBoxes)
                        {
                            TextBox box = pictureBox1.Controls.Find(textBox.Name, true)[0] as TextBox;
                            textBox.Text = box.Text;
                        }
                    }
                    string jsonString = JsonSerializer.Serialize<List<Layer>>(layers);

                    File.WriteAllText(filePath, jsonString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}