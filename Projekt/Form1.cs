using System.Text;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Drawing;
using System.Xml.Linq;
using System.Numerics;

namespace Projekt
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        int x = -1;
        int y = -1;
        bool moving = false;
        Pen pen;
        tools currentTool { get; set; }
        List<Layer> layers { get; set; }
        Layer currentLayer { get; set; }

        // Narzêdzia z lewego tool boxa
        public enum tools {
            brush,
            pencil,
            textBoxer
        }
        public Form1()
        {
            InitializeComponent();

            // Ustawienie wartoœci pocz¹tkowych
            currentTool = tools.brush;
            layers = new List<Layer>();
            currentLayer = new Layer();
            currentLayer.Name = "Warstwa 1";
            currentLayer.textBoxes = new List<SerializableTextBox>();
            currentLayer.lines = new List<SerializableLine>();
            layers.Add(currentLayer);

            // Wartoœci dla rysowania
            graphics = pictureBox1.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 5);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentTool == tools.brush || currentTool == tools.pencil)
            {
                moving = true;
                x = e.X;
                y = e.Y;
                pictureBox1.Cursor = Cursors.Cross;
            } else
            {
                SerializableTextBox newTextBoxToSerialize = new SerializableTextBox();
                newTextBoxToSerialize.Name = "nextTextBox" + currentLayer.textBoxes.Count.ToString();
                newTextBoxToSerialize.Location = e.Location;
                newTextBoxToSerialize.MultiLine = true;
                newTextBoxToSerialize.BackColor = Color.White;
                currentLayer.textBoxes.Add(newTextBoxToSerialize);
                createTextBoxFromSerializable(newTextBoxToSerialize);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentTool == tools.brush || currentTool == tools.pencil)
            {
                if (moving && x != -1 && y != -1)
                {
                    SerializableLine line = new SerializableLine();
                    line.pt1 = new Point(x, y);
                    line.pt2 = e.Location;
                    currentLayer.lines.Add(line);
                    graphics.DrawLine(pen, new Point(x, y), e.Location);
                    x = e.X;
                    y = e.Y;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentTool == tools.brush || currentTool == tools.pencil)
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
            switch(button.Name)
            {
                case "brushButton":
                    currentTool = tools.brush;
                    break;
                case "pencilButton":
                    currentTool = tools.pencil;
                    break;
                case "textBoxerButton":
                    currentTool = tools.textBoxer;
                    break;
            }
        }

        private void createLineFromSerializable(SerializableLine line)
        {
            graphics.DrawLine(pen, line.pt1, line.pt2);
        }
        private void createTextBoxFromSerializable(SerializableTextBox textBox)
        {
            TextBox newTextBox = new TextBox();
            newTextBox.Name = textBox.Name;
            newTextBox.Text = textBox.Text;
            newTextBox.Location = textBox.Location;
            newTextBox.Multiline = textBox.MultiLine;
            newTextBox.BackColor = textBox.BackColor;
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
                    foreach(SerializableTextBox textBox in layer.textBoxes)
                    {
                        createTextBoxFromSerializable(textBox);
                    }
                    foreach(SerializableLine line in layer.lines)
                    {
                        createLineFromSerializable(line);
                    }
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
                        foreach(SerializableTextBox textBox in layer.textBoxes)
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
    [Serializable]
    public class SerializableTextBox
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Point Location { get; set; }
        public bool MultiLine { get; set; }
        public Color BackColor { get; set; }
        [JsonConstructor]
        public SerializableTextBox() { }
    }
    [Serializable]
    public class SerializableLine
    {
        public Point pt1 { get; set; }
        public Point pt2 { get; set; }
        public SerializableLine() { }
    }
    public class Layer
    {
        public List<SerializableTextBox> textBoxes { get; set; }
        public List<SerializableLine> lines { get; set; }
        public string Name { get; set; }
        public Layer() { }
    }
}