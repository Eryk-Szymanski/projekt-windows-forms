using System.Windows.Forms;

namespace Projekt
{
    public partial class Form1 : Form
    {
        Graphics g;
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
            currentLayer = new Layer("Warstwa 1", pictureBox1);
            layers.Add(currentLayer);

            // Wartoœci dla rysowania
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
                TextBox newTextBox = new TextBox();
                newTextBox.Location = e.Location;
                newTextBox.Multiline = true;
                newTextBox.BackColor = Color.White;
                pictureBox1.Controls.Add(newTextBox);
                currentLayer.textBoxes.Add(newTextBox);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentTool == tools.brush || currentTool == tools.pencil)
            {
                if (moving && x != -1 && y != -1)
                {
                    currentLayer.graphics.DrawLine(pen, new Point(x, y), e.Location);
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
    }
    public class Layer
    {
        public List<TextBox> textBoxes { get; set; }
        public Graphics graphics { get; set; }
        public string Name { get; set; }
        public Layer(string name, PictureBox pictureBox)
        {
            this.Name = name;
            this.textBoxes = new List<TextBox>();
            graphics = pictureBox.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }
    }
}