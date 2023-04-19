using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class CanvasManager
    {
        public delegate void displayLayerDelegate(Layer layer, Func<tools, int, Color, Pen> createPen);
        public delegate Pen createPenDelegate(tools penType, int newPenSize, Color color);

        Graphics graphics;
        int x = -1;
        int y = -1;
        bool moving = false;
        Color canvasColor = Color.White;
        public List<Layer> layers { get; set; }
        public Layer currentLayer { get; set; }
        SerializableLine currentLine { get; set; }
        public PictureBox canvasPictureBox { get; set; }

        public CanvasManager(PictureBox pictureBox)
        {
            canvasPictureBox = pictureBox;

            layers = new List<Layer>();
            currentLayer = new Layer();
            currentLayer.Name = "Nowa warstwa";
            currentLayer.TextBoxes = new List<SerializableTextBox>();
            currentLayer.Lines = new List<SerializableLine>();
            layers.Add(currentLayer);

            canvasPictureBox.BackColor = canvasColor;
            graphics = canvasPictureBox.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void drawTextBox(MouseEventArgs e)
        {
            SerializableTextBox newTextBoxToSerialize = new SerializableTextBox();
            newTextBoxToSerialize.Name = "nextTextBox" + layers.SelectMany(l => l.TextBoxes).Count();
            newTextBoxToSerialize.Location = e.Location;
            newTextBoxToSerialize.MultiLine = true;
            newTextBoxToSerialize.BackColor = canvasColor;
            newTextBoxToSerialize.BorderStyle = BorderStyle.None;
            currentLayer.TextBoxes.Add(newTextBoxToSerialize);
            createTextBoxFromSerializable(newTextBoxToSerialize);
        }

        public void drawStart(MouseEventArgs e, Color penColor, tools currentTool, int penSize)
        {
            moving = true;
            x = e.X;
            y = e.Y;
            canvasPictureBox.Cursor = Cursors.Cross;
            currentLine = new SerializableLine();
            currentLine.lineParts = new List<SerializableLinePart>();
            currentLine.A = penColor.A;
            currentLine.R = penColor.R;
            currentLine.G = penColor.G;
            currentLine.B = penColor.B;
            currentLine.Tool = currentTool;
            currentLine.LineSize = penSize;
        }

        public void draw(MouseEventArgs e, Pen pen)
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

        public void drawEnd(tools currentTool)
        {
            if (currentTool != tools.textBoxer)
            {
                moving = false;
                x = -1;
                y = -1;
                canvasPictureBox.Cursor = Cursors.Default;
                currentLayer.Lines.Add(currentLine);
            }
        }

        public void drawLine(SerializableLine line, Func<tools,int,Color,Pen>createPen)
        {
            Pen newPen = (Pen)canvasPictureBox.Invoke(new createPenDelegate(createPen), line.Tool, line.LineSize, Color.FromArgb(line.A, line.R, line.G, line.B));
            foreach (SerializableLinePart linePart in line.lineParts)
            {
                graphics.DrawLine(newPen, linePart.Pt1, linePart.Pt2);
            }
        }
        public void createTextBoxFromSerializable(SerializableTextBox textBox)
        {
            TextBox newTextBox = new TextBox();
            newTextBox.Name = textBox.Name;
            newTextBox.Text = textBox.Text;
            newTextBox.Location = textBox.Location;
            newTextBox.Multiline = textBox.MultiLine;
            newTextBox.BackColor = textBox.BackColor;
            newTextBox.BorderStyle = textBox.BorderStyle;
            canvasPictureBox.Controls.Add(newTextBox);
        }
        public void displayLayer(Layer layer, Func<tools, int, Color, Pen> createPen)
        {
            foreach (SerializableTextBox textBox in layer.TextBoxes)
            {
                createTextBoxFromSerializable(textBox);
            }
            foreach (SerializableLine line in layer.Lines)
            {
                drawLine(line, createPen);
            }
        }

        public void loadLayers(List<Layer> loadLayers, Func<tools, int, Color, Pen> createPen)
        {
            layers = loadLayers;
            var watek = new Thread(new ThreadStart(() => {
                foreach (Layer layer in layers)
                {
                    canvasPictureBox.Invoke(new displayLayerDelegate(displayLayer), layer, createPen);
                }
            }));
            watek.IsBackground = true;
            watek.Start();
            currentLayer = new Layer();
            currentLayer.Name = "Nowa warstwa";
            currentLayer.TextBoxes = new List<SerializableTextBox>();
            currentLayer.Lines = new List<SerializableLine>();
            layers.Add(currentLayer);
        }

        public void addLayer() { }

        public void removeLayer() { }

        public void duplicateLayer() { }

        public void selectLayer() { }

        public void hideLayer() { }
    }
}
