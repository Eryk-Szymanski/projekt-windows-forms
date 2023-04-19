using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Projekt.Form1;

namespace Projekt
{
    public class FileManager
    {
        public void saveToJson(List<Layer> layers, PictureBox canvasPictureBox)
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
                    foreach (Layer layer in layers)
                    {
                        foreach (SerializableTextBox textBox in layer.TextBoxes)
                        {
                            TextBox box = canvasPictureBox.Controls.Find(textBox.Name, true)[0] as TextBox;
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

        public List<Layer> loadFromJson()
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

            List<Layer> layers = new List<Layer>();

            try
            {
                layers = JsonSerializer.Deserialize<List<Layer>>(File.ReadAllText(filePath))!;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return layers;
        }
    }
}
