using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class SerializableTextBox
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Point Location { get; set; }
        public bool MultiLine { get; set; }
        public Color BackColor { get; set; }
        public BorderStyle BorderStyle { get; set; }
        [JsonConstructor]
        public SerializableTextBox() { }
    }
}
