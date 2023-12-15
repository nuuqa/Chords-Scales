using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chords
{
    public class Chord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RootNote { get; set; }
        public int Transpose { get; set; }
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    }
}
