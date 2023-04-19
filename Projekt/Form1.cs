namespace Projekt
{
    public partial class Form1 : Form
    {
        public delegate void displayLayerDelegate(Layer layer);
        public delegate Pen createPenDelegate(tools penType, int newPenSize, Color color);
        
        FileManager fileManager { get; }
        ToolBoxManager toolBoxManager { get; }
        CanvasManager canvasManager { get; }

        public Form1()
        {
            InitializeComponent();

            fileManager = new FileManager();
            canvasManager = new CanvasManager(canvasPictureBox);
            toolBoxManager = new ToolBoxManager(penColorButton);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(toolBoxManager.currentTool != tools.textBoxer)
                canvasManager.drawStart(e, toolBoxManager.penColor, toolBoxManager.currentTool, toolBoxManager.penSize);
            else
                canvasManager.drawTextBox(e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (toolBoxManager.currentTool != tools.textBoxer)
            {
                canvasManager.draw(e, toolBoxManager.pen);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (toolBoxManager.currentTool != tools.textBoxer)
            {
                canvasManager.drawEnd(toolBoxManager.currentTool);
            }
        }

        private void toolBoxItem_Click(object sender, EventArgs e)
        {
            toolBoxManager.selectTool(sender as ToolStripButton, penSizeBar);
        }

        private void selectColor(object sender, EventArgs e)
        {
            toolBoxManager.selectColor();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Layer> layers = fileManager.loadFromJson();
                canvasManager.loadLayers(layers, toolBoxManager.createPen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            fileManager.saveToJson(canvasManager.layers, canvasManager.canvasPictureBox);
        }

        private void penSizeBar_Scroll(object sender, EventArgs e)
        {
            toolBoxManager.changePenSize(sender as TrackBar);
        }
    }
}