using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.classes
{
    [Serializable]
    public class SerializableLine
    {
        public Point Pt1 { get; set; }
        public Point Pt2 { get; set; }
        public int LineA { get; set; }
        public int LineR { get; set; }
        public int LineG { get; set; }
        public int LineB { get; set; }
        public SerializableLine() { }
    }
}
