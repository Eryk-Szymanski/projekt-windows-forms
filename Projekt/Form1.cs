using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Projekt
{
    public partial class Form1 : Form
    {
        FileManager fileManager { get; }
        CanvasManager canvasManager { get; }

        bool saved = false;
        bool exit = false;

        public Form1()
        {
            InitializeComponent();

            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            WindowState = FormWindowState.Maximized;

            fileManager = new FileManager();
            canvasManager = new CanvasManager(canvasPictureBox, penColorButton, layerList);
            canvasManager.loadLayers();
            canvasManager.highlightSelectedLayer();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            saved = false;
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
            canvasManager.selectColor(sender);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Layer> layers = fileManager.loadFromJson();
                canvasManager.layers = layers;
                canvasManager.loadLayers();
                saved = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            fileManager.saveToJson(canvasManager.layers, canvasManager.canvasPictureBox);
            saved = true;
        }

        private void penSizeBar_Scroll(object sender, EventArgs e)
        {
            canvasManager.toolBoxManager.changePenSize((TrackBar)sender);
        }

        public void selectLayerButton(object sender, EventArgs e)
        {
            ToolStripButton clickedButton = (ToolStripButton)sender;

            saved = false;
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

        private void newFileButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
            this.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                DialogResult dialogResult = MessageBox.Show("Czy chcesz wyjœæ bez zapisywania?", "Niezapisane zmiany", MessageBoxButtons.YesNo);
                if (dialogResult != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}