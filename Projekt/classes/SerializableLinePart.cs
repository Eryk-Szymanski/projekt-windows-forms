using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class SerializableLinePart
    {
        public Point Pt1 { get; set; }
        public Point Pt2 { get; set; }
        public SerializableLinePart() { }
    }
}
