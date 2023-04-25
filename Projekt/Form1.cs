namespace Projekt
{
    public partial class Form1 : Form
    {
        FileManager fileManager { get; }
        CanvasManager canvasManager { get; }

        public Form1()
        {
            InitializeComponent();

            fileManager = new FileManager();
            canvasManager = new CanvasManager(canvasPictureBox, penColorButton, layerList);
            canvasManager.loadLayers();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(canvasManager.toolBoxManager.currentTool != tools.textBoxer)
                canvasManager.drawStart(e);
            else
                canvasManager.drawTextBox(e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canvasManager.toolBoxManager.currentTool != tools.textBoxer)
            {
                canvasManager.draw(e);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (canvasManager.toolBoxManager.currentTool != tools.textBoxer)
            {
                canvasManager.drawEnd();
            }
        }

        private void toolBoxItem_Click(object sender, EventArgs e)
        {
            canvasManager.toolBoxManager.selectTool((ToolStripButton)sender, penSizeBar);
        }

        private void selectColor(object sender, EventArgs e)
        {
            canvasManager.selectColor(sender, layerList);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Layer> layers = fileManager.loadFromJson();
                canvasManager.layers = layers;
                canvasManager.loadLayers();
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
            canvasManager.toolBoxManager.changePenSize((TrackBar)sender);
        }

        public void selectLayerButton(object sender, EventArgs e)
        {
            ToolStripButton clickedButton = (ToolStripButton)sender;

            switch (clickedButton.Name)
            {
                case "addLayerBtn":
                    canvasManager.addLayer();
                    break;
                case "removeLayerBtn":
                    canvasManager.removeLayer();
                    break;
                case "duplicateLayerBtn":
                    canvasManager.duplicateLayer();
                    break;
                case "hideLayerBtn":
                    canvasManager.hideShowLayer();
                    break;
            }
        }

        private void layerList_Click(object sender, EventArgs e)
        {
            canvasManager.selectLayer(layerList.SelectedItems[0]);
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ten projekt to wcale nie podróba Painta... (serio)\nAutorzy: Szymañski, Wróblewski, Uzarowicz\nNa potrzeby zaliczenia przedmiotu 'Programowanie Windows Forms'");
        }
    }
}