using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chords
{
    public class Note
    {
        public int Id;
        public string Name { get; set; }
        public int Octave { get; set; }
        public int MidiValue { get; set; }

    }
}
