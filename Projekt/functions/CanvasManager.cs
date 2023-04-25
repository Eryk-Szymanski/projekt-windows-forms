using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
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
        public ToolBoxManager toolBoxManager { get; }
        public ListView layerList { get; set; }

        public CanvasManager(PictureBox pictureBox, ToolStripButton penColorButton, ListView layerList)
        {
            canvasPictureBox = pictureBox;
            toolBoxManager = new ToolBoxManager(penColorButton);
            this.layerList = layerList;

            layers = new List<Layer>();
            currentLayer = new Layer();
            currentLayer.Name = $"Nowa warstwa {layers.Count + 1}";
            currentLayer.TextBoxes = new List<SerializableTextBox>();
            currentLayer.Lines = new List<SerializableLine>();
            currentLayer.IsVisible = true;
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

        public void drawStart(MouseEventArgs e)
        {
            Color penColor = toolBoxManager.penColor;
            tools currentTool = toolBoxManager.currentTool;
            int penSize = toolBoxManager.penSize;
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

        public void draw(MouseEventArgs e)
        {
            Pen pen = toolBoxManager.pen;
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

        public void drawEnd()
        {
            tools currentTool = toolBoxManager.currentTool;
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

        public void addLayerToListView(Layer layer)
        {
            var listViewItem = new ListViewItem();
            listViewItem.Text = $"{layer.Name} - {(layer.IsVisible ? "Widoczna" : "Niewidoczna")}";
            listViewItem.Name = layer.Name;
            layerList.Items.Add(listViewItem);
        }

        public void displayLayer(Layer layer, Func<tools, int, Color, Pen> createPen)
        {
            if (layer.IsVisible)
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
            addLayerToListView(layer);
        }

        public void loadLayers()
        {
            graphics.Clear(canvasColor);
            layerList.Items.Clear();
            toolBoxManager.rubberColor = canvasColor;

            var watek = new Thread(new ThreadStart(() => {
                foreach (Layer layer in layers)
                {
                    canvasPictureBox.Invoke(new displayLayerDelegate(displayLayer), layer, toolBoxManager.createPen);
                }
            }));
            watek.IsBackground = true;
            watek.SetApartmentState(ApartmentState.STA);
            watek.Start();

            currentLayer = layers.Last();
            highlightSelectedLayer();
        }

        public void selectColor(object sender)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = false;
            colorDialog.ShowHelp = true;
            
            if(sender.GetType() == typeof(ToolStripButton))
            {
                colorDialog.Color = toolBoxManager.penColor;
            } else
            {
                colorDialog.Color = canvasColor;
            }

            if (colorDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    if (sender.GetType() == typeof(ToolStripButton))
                    {
                        toolBoxManager.changePenColor(colorDialog.Color);
                    }
                    else
                    {
                        canvasColor = colorDialog.Color;
                        loadLayers();
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        public void selectLayer(ListViewItem listViewItem)
        {
            Layer previousLayer = currentLayer;
            currentLayer = layers.Where(l => listViewItem.Name.Equals(l.Name)).First();
            highlightSelectedLayer(previousLayer);
        }

        public void highlightSelectedLayer(Layer previousLayer = null)
        {
            string row = String.Empty;
            ListViewItem listViewItem;
            if (previousLayer != null)
            {
                row = $"{previousLayer.Name} - {(previousLayer.IsVisible ? "Widoczna" : "Niewidoczna")}";
                listViewItem = layerList.FindItemWithText(row);
                if(listViewItem != null)
                {
                    listViewItem.BackColor = Color.FromArgb(224, 224, 224);
                }
            }
            row = $"{currentLayer.Name} - {(currentLayer.IsVisible ? "Widoczna" : "Niewidoczna")}";
            listViewItem = layerList.FindItemWithText(row);
            if(listViewItem != null)
            {
                listViewItem.BackColor = Color.Aqua;
            }
        }

        public void addLayer() 
        {
            Layer newLayer = new Layer();
            newLayer.Name = $"Nowa warstwa {layers.Count + 1}";
            newLayer.TextBoxes = new List<SerializableTextBox>();
            newLayer.Lines = new List<SerializableLine>();
            newLayer.IsVisible = true;
            layers.Add(newLayer);
            currentLayer = newLayer;
            loadLayers();
            highlightSelectedLayer();
        }

        public void removeLayer() 
        {
            if (layers.Count > 1)
            {
                layers.Remove(currentLayer);
                currentLayer = layers.Last();
                loadLayers();
                highlightSelectedLayer();
            }
            else
            {
                MessageBox.Show("Brak warstw do usunięcia - nie można usunąć więcej warstw.");
            }
        }

        public void duplicateLayer() 
        {
            
            Layer newLayer = Helper.CreateDeepCopy<Layer>(currentLayer);
            newLayer.Name = currentLayer.Name + " (kopia)";
            layers.Add(newLayer);
            currentLayer = newLayer;
            loadLayers();
            highlightSelectedLayer();
        }

        public void hideShowLayer() 
        {
            if (currentLayer.IsVisible)
            {
                currentLayer.IsVisible = false;
            }
            else
            {
                currentLayer.IsVisible = true;
            }
            loadLayers();
        }
    }
}
