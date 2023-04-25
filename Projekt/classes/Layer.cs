using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Layer
    {
        public List<SerializableTextBox> TextBoxes { get; set; }
        public List<SerializableLine> Lines { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public Layer() { }
    }
}
