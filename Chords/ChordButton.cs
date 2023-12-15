using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chords
{
    public class ChordButton : Button
    {
        Chord Chord { get; set; }
        string RootNote { get; set; }

    }
}
