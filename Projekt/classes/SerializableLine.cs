using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class SerializableLine
    {
        public List<SerializableLinePart> lineParts { get; set; }
        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public tools Tool { get; set; }
        public int LineSize { get; set; }
        
        public SerializableLine() { }
    }
}
