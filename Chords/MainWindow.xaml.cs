using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Path = System.IO.Path;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System.IO;
using NAudio.Wave.SampleProviders;


namespace Chords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // 3 Octave
        private static readonly string currentDirectory = Environment.CurrentDirectory;
        private static readonly string C3 = Path.Combine(currentDirectory, "Notes", "C3.wav");
        private static readonly string Csharp3 = Path.Combine(currentDirectory, "Notes", "C#3.wav");
        private static readonly string D3 = Path.Combine(currentDirectory, "Notes", "D3.wav");
        private static readonly string Dsharp3 = Path.Combine(currentDirectory, "Notes", "D#3.wav");
        private static readonly string E3 = Path.Combine(currentDirectory, "Notes", "E3.wav");
        private static readonly string F3 = Path.Combine(currentDirectory, "Notes", "F3.wav");
        private static readonly string Fsharp3 = Path.Combine(currentDirectory, "Notes", "F#3.wav");
        private static readonly string G3 = Path.Combine(currentDirectory, "Notes", "G3.wav");
        private static readonly string Gsharp3 = Path.Combine(currentDirectory, "Notes", "G#3.wav");
        private static readonly string A3 = Path.Combine(currentDirectory, "Notes", "A3.wav");
        private static readonly string Asharp3 = Path.Combine(currentDirectory, "Notes", "A#3.wav");
        private static readonly string B3 = Path.Combine(currentDirectory, "Notes", "B3.wav");

        // 4 Octave
        private static readonly string C = Path.Combine(currentDirectory, "Notes", "C.wav");
        private static readonly string Csharp = Path.Combine(currentDirectory, "Notes", "C#.wav");
        private static readonly string D = Path.Combine(currentDirectory, "Notes", "D.wav");
        private static readonly string Dsharp = Path.Combine(currentDirectory, "Notes", "D#.wav");
        private static readonly string E = Path.Combine(currentDirectory, "Notes", "E.wav");
        private static readonly string F = Path.Combine(currentDirectory, "Notes", "F.wav");
        private static readonly string Fsharp = Path.Combine(currentDirectory, "Notes", "F#.wav");
        private static readonly string G = Path.Combine(currentDirectory, "Notes", "G.wav");
        private static readonly string Gsharp = Path.Combine(currentDirectory, "Notes", "G#.wav");
        private static readonly string A = Path.Combine(currentDirectory, "Notes", "A.wav");
        private static readonly string Asharp = Path.Combine(currentDirectory, "Notes", "A#.wav");
        private static readonly string B = Path.Combine(currentDirectory, "Notes", "B.wav");

        // 5 Octave
        private static readonly string C5 = Path.Combine(currentDirectory, "Notes", "C5.wav");
        private static readonly string Csharp5 = Path.Combine(currentDirectory, "Notes", "C#5.wav");
        private static readonly string D5 = Path.Combine(currentDirectory, "Notes", "D5.wav");
        private static readonly string Dsharp5 = Path.Combine(currentDirectory, "Notes", "D#5.wav");
        private static readonly string E5 = Path.Combine(currentDirectory, "Notes", "E5.wav");
        private static readonly string F5 = Path.Combine(currentDirectory, "Notes", "F5.wav");
        private static readonly string Fsharp5 = Path.Combine(currentDirectory, "Notes", "F#5.wav");
        private static readonly string G5 = Path.Combine(currentDirectory, "Notes", "G5.wav");
        private static readonly string Gsharp5 = Path.Combine(currentDirectory, "Notes", "G#5.wav");
        private static readonly string A5 = Path.Combine(currentDirectory, "Notes", "A5.wav");
        private static readonly string Asharp5 = Path.Combine(currentDirectory, "Notes", "A#5.wav");
        private static readonly string B5 = Path.Combine(currentDirectory, "Notes", "B5.wav");

        // 6 Octave
        private static readonly string C6 = Path.Combine(currentDirectory, "Notes", "C6.wav");

        private new Chord selectedChord = new Chord();
        private new Scale selectedScale = new Scale();
        private float volumeStream = 0.7f;
        private ChordsRepo repo = new ChordsRepo();
        private ObservableCollection<Chord> chordProgression = new ObservableCollection<Chord>();
        private ChordsDatabase createDb = new ChordsDatabase();


        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists("ChordsAndScales.db"))
            {
                createDb.CreateDatabaseIfNotExists();
                createDb.InsertData();
            }
            lbChords.DataContext = new Chord();
            lbChords.ItemsSource = repo.GetAllChords();
            lbScales.DataContext = new Scale();
            lbScales.ItemsSource = repo.GetAllScales();

            slVolume.Value = volumeStream;
            slVolume.ValueChanged += VolumeChanged;



        }

        private void lbNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbChords.SelectedValue != null)
            {
                lbChords_SelectionChanged(sender, e);
            }
            else
            {
                lbScales_SelectionChanged(sender, e);
            }
        }

        private void lbChords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbScales.SelectedItem = null;
            if (lbNotes.SelectedValue != null && lbScales.SelectedValue == null)
            {

                ResetNotes();
                tbTranspose.Text = "0";
                selectedChord = repo.GetChord(lbChords.SelectedValue as Chord);
                selectedChord.RootNote = (string)lbNotes.SelectedValue;
                selectedChord.Transpose = Convert.ToInt32(tbTranspose.Text);
                selectedChord = repo.CheckNote(selectedChord);
                SelectedNotes(selectedChord);
            }
        }

        private void lbScales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbChords.SelectedItem = null;
            if (lbNotes.SelectedValue != null && lbChords.SelectedValue == null)
            {
                ResetNotes();
                tbTranspose.Text = "0";
                selectedScale = repo.GetScale(lbScales.SelectedValue as Scale);
                selectedScale.RootNote = (string)lbNotes.SelectedValue;
                selectedScale.Transpose = Convert.ToInt32(tbTranspose.Text);
                selectedScale = repo.CheckNote(selectedScale);
                SelectedNotes(selectedScale);
            }
        }


        public void SelectedNotes(Chord chord)
        {
            foreach (var item in chord.Notes)
            {
                switch (item.Id)
                {
                    // 3 Octave
                    case 1:
                        btnC3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 2:
                        btnCC3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 3:
                        btnD3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 4:
                        btnDD3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 5:
                        btnE3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 6:
                        btnF3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 7:
                        btnFF3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 8:
                        btnG3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 9:
                        btnGG3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 10:
                        btnA3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 11:
                        btnAA3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 12:
                        btnB3.Background = (Brush)FindResource("SelectedNotes");
                        break;


                    // 4 Octave
                    case 13:
                        btnC4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 14:
                        btnCC4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 15:
                        btnD4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 16:
                        btnDD4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 17:
                        btnE4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 18:
                        btnF4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 19:
                        btnFF4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 20:
                        btnG4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 21:
                        btnGG4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 22:
                        btnA4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 23:
                        btnAA4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 24:
                        btnB4.Background = (Brush)FindResource("SelectedNotes");
                        break;

                    // 5 Octave
                    case 25:
                        btnC5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 26:
                        btnCC5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 27:
                        btnD5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 28:
                        btnDD5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 29:
                        btnE5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 30:
                        btnF5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 31:
                        btnFF5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 32:
                        btnG5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 33:
                        btnGG5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 34:
                        btnA5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 35:
                        btnAA5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 36:
                        btnB5.Background = (Brush)FindResource("SelectedNotes");
                        break;

                    // 6 Octave
                    case 37:
                        btnC6.Background = (Brush)FindResource("SelectedNotes");
                        break;

                }
            }
        }

        public void SelectedNotes(Scale scale)
        {
            foreach (var item in scale.Notes)
            {
                switch (item.Id)
                {
                    // 3 Octave
                    case 1:
                        btnC3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 2:
                        btnCC3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 3:
                        btnD3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 4:
                        btnDD3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 5:
                        btnE3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 6:
                        btnF3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 7:
                        btnFF3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 8:
                        btnG3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 9:
                        btnGG3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 10:
                        btnA3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 11:
                        btnAA3.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 12:
                        btnB3.Background = (Brush)FindResource("SelectedNotes");
                        break;


                    // 4 Octave
                    case 13:
                        btnC4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 14:
                        btnCC4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 15:
                        btnD4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 16:
                        btnDD4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 17:
                        btnE4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 18:
                        btnF4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 19:
                        btnFF4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 20:
                        btnG4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 21:
                        btnGG4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 22:
                        btnA4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 23:
                        btnAA4.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 24:
                        btnB4.Background = (Brush)FindResource("SelectedNotes");
                        break;

                    // 5 Octave
                    case 25:
                        btnC5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 26:
                        btnCC5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 27:
                        btnD5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 28:
                        btnDD5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 29:
                        btnE5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 30:
                        btnF5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 31:
                        btnFF5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 32:
                        btnG5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 33:
                        btnGG5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 34:
                        btnA5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 35:
                        btnAA5.Background = (Brush)FindResource("SelectedNotes");
                        break;
                    case 36:
                        btnB5.Background = (Brush)FindResource("SelectedNotes");
                        break;

                    // 6 Octave
                    case 37:
                        btnC6.Background = (Brush)FindResource("SelectedNotes");
                        break;

                }
            }
        }

        private void ResetNotes()
        {
            btnC3.Background = (Brush)FindResource("OriginalState");
            btnCC3.Background = (Brush)FindResource("OriginalStateSharp");
            btnD3.Background = (Brush)FindResource("OriginalState");
            btnDD3.Background = (Brush)FindResource("OriginalStateSharp");
            btnE3.Background = (Brush)FindResource("OriginalState");
            btnF3.Background = (Brush)FindResource("OriginalState");
            btnFF3.Background = (Brush)FindResource("OriginalStateSharp");
            btnG3.Background = (Brush)FindResource("OriginalState");
            btnGG3.Background = (Brush)FindResource("OriginalStateSharp");
            btnA3.Background = (Brush)FindResource("OriginalState");
            btnAA3.Background = (Brush)FindResource("OriginalStateSharp");
            btnB3.Background = (Brush)FindResource("OriginalState");
            btnC3.Background = (Brush)FindResource("OriginalState");

            btnC4.Background = (Brush)FindResource("OriginalState");
            btnCC4.Background = (Brush)FindResource("OriginalStateSharp");
            btnD4.Background = (Brush)FindResource("OriginalState");
            btnDD4.Background = (Brush)FindResource("OriginalStateSharp");
            btnE4.Background = (Brush)FindResource("OriginalState");
            btnF4.Background = (Brush)FindResource("OriginalState");
            btnFF4.Background = (Brush)FindResource("OriginalStateSharp");
            btnG4.Background = (Brush)FindResource("OriginalState");
            btnGG4.Background = (Brush)FindResource("OriginalStateSharp");
            btnA4.Background = (Brush)FindResource("OriginalState");
            btnAA4.Background = (Brush)FindResource("OriginalStateSharp");
            btnB4.Background = (Brush)FindResource("OriginalState");

            btnC5.Background = (Brush)FindResource("OriginalState");
            btnCC5.Background = (Brush)FindResource("OriginalStateSharp");
            btnD5.Background = (Brush)FindResource("OriginalState");
            btnDD5.Background = (Brush)FindResource("OriginalStateSharp");
            btnE5.Background = (Brush)FindResource("OriginalState");
            btnF5.Background = (Brush)FindResource("OriginalState");
            btnFF5.Background = (Brush)FindResource("OriginalStateSharp");
            btnG5.Background = (Brush)FindResource("OriginalState");
            btnGG5.Background = (Brush)FindResource("OriginalStateSharp");
            btnA5.Background = (Brush)FindResource("OriginalState");
            btnAA5.Background = (Brush)FindResource("OriginalStateSharp");
            btnB5.Background = (Brush)FindResource("OriginalState");
            btnC6.Background = (Brush)FindResource("OriginalState");

        }

        // Window settings
        private void MoveWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }
        private void rctClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void rctClose_MouseEnter(object sender, MouseEventArgs e)
        {
            rctClose.Fill = Brushes.Red;
        }
        private void rctClose_MouseLeave(object sender, MouseEventArgs e)
        {
            rctClose.Fill = Brushes.DarkRed;
        }
        private void rctMinimize_MouseEnter(object sender, MouseEventArgs e)
        {

            rctMinimize.Fill = Brushes.Gold;
        }
        private void rctMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            rctMinimize.Fill = Brushes.DarkGoldenrod;
        }
        private void rctMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Keys
        private void PlayNote_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            PlayNote((string)button.Content);


        }
        private void PlayNote(string sound)
        {

            switch (sound)
            {
                // 3 Octave
                case "C3":
                    repo.PlaySound(C3, volumeStream);
                    break;
                case "C#/Db3":
                    repo.PlaySound(Csharp3, volumeStream);
                    break;
                case "D3":
                    repo.PlaySound(D3, volumeStream);
                    break;
                case "D#/Eb3":
                    repo.PlaySound(Dsharp3, volumeStream);
                    break;
                case "E3":
                    repo.PlaySound(E3, volumeStream);
                    break;
                case "F3":
                    repo.PlaySound(F3, volumeStream);
                    break;
                case "F#/Gb3":
                    repo.PlaySound(Fsharp3, volumeStream);
                    break;
                case "G3":
                    repo.PlaySound(G3, volumeStream);
                    break;
                case "G#/Ab3":
                    repo.PlaySound(Gsharp3, volumeStream);
                    break;
                case "A3":
                    repo.PlaySound(A3, volumeStream);
                    break;
                case "A#/Bb3":
                    repo.PlaySound(Asharp3, volumeStream);
                    break;
                case "B3":
                    repo.PlaySound(B3, volumeStream);
                    break;


                // 4 Octave
                case "C4":
                    repo.PlaySound(C, volumeStream);
                    break;
                case "C#/Db4":
                    repo.PlaySound(Csharp, volumeStream);
                    break;
                case "D4":
                    repo.PlaySound(D, volumeStream);
                    break;
                case "D#/Eb4":
                    repo.PlaySound(Dsharp, volumeStream);
                    break;
                case "E4":
                    repo.PlaySound(E, volumeStream);
                    break;
                case "F4":
                    repo.PlaySound(F, volumeStream);
                    break;
                case "F#/Gb4":
                    repo.PlaySound(Fsharp, volumeStream);
                    break;
                case "G4":
                    repo.PlaySound(G, volumeStream);
                    break;
                case "G#/Ab4":
                    repo.PlaySound(Gsharp, volumeStream);
                    break;
                case "A4":
                    repo.PlaySound(A, volumeStream);
                    break;
                case "A#/Bb4":
                    repo.PlaySound(Asharp, volumeStream);
                    break;
                case "B4":
                    repo.PlaySound(B, volumeStream);
                    break;

                // 5 Octave
                case "C5":
                    repo.PlaySound(C5, volumeStream);
                    break;
                case "C#/Db5":
                    repo.PlaySound(Csharp5, volumeStream);
                    break;
                case "D5":
                    repo.PlaySound(D5, volumeStream);
                    break;
                case "D#/Eb5":
                    repo.PlaySound(Dsharp5, volumeStream);
                    break;
                case "E5":
                    repo.PlaySound(E5, volumeStream);
                    break;
                case "F5":
                    repo.PlaySound(F5, volumeStream);
                    break;
                case "F#/Gb5":
                    repo.PlaySound(Fsharp5, volumeStream);
                    break;
                case "G5":
                    repo.PlaySound(G5, volumeStream);
                    break;
                case "G#/Ab5":
                    
                    repo.PlaySound(Gsharp5, volumeStream);
                    break;
                case "A5":
                    repo.PlaySound(A5, volumeStream);
                    break;
                case "A#/Bb5":
                    repo.PlaySound(Asharp5, volumeStream);
                    break;
                case "B5":
                    repo.PlaySound(B5, volumeStream);
                    break;

                // 6 Octave
                case "C6":
                    repo.PlaySound(C6, volumeStream);
                    break;
            }

        }

        // Play button
        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Chord newChord = (Chord)button.DataContext;
            if (button.Content != string.Empty)
            {
                if (button.Name == "btnBeat1" || button.Name == "btnBeat2" || button.Name == "btnBeat3" || button.Name == "btnBeat4")
                {
                    if (button.DataContext != null)
                    {

                        foreach (var item in newChord.Notes)
                        {
                            await PlayChordAsync(item.Id, button);

                        }
                        return;
                    }
                }

                if (selectedChord != null)
                {
                    foreach (var item in selectedChord.Notes) // chords
                    {
                        await PlayChordAsync(item.Id, button);
                    }
                }

                if (selectedScale != null)
                {
                    foreach (var item in selectedScale.Notes) // scales
                    {
                        await PlayScaleAsync(item.Id, button);


                    }
                }
            }
        }


        private async Task PlayScaleAsync(int noteId,  Button button)
        {
            switch (noteId)
            {
                // 3 Octave
                case 1:
                    repo.PlaySound(C3, volumeStream);
                    break;
                case 2:
                    repo.PlaySound(Csharp3, volumeStream);
                    break;
                case 3:
                    repo.PlaySound(D3, volumeStream);
                    break;
                case 4:
                    repo.PlaySound(Dsharp3, volumeStream);
                    break;
                case 5:
                    repo.PlaySound(E3, volumeStream);
                    break;
                case 6:
                    repo.PlaySound(F3, volumeStream);
                    break;
                case 7:
                    repo.PlaySound(Fsharp3, volumeStream);
                    break;
                case 8:
                    repo.PlaySound(G3, volumeStream);
                    break;
                case 9:
                    repo.PlaySound(Gsharp3, volumeStream);
                    break;
                case 10:
                    repo.PlaySound(A3, volumeStream);
                    break;
                case 11:
                    repo.PlaySound(Asharp3, volumeStream);
                    break;
                case 12:
                    repo.PlaySound(B3, volumeStream);
                    break;


                // 4 Octave
                case 13:
                    repo.PlaySound(C, volumeStream);
                    break;
                case 14:
                    repo.PlaySound(Csharp, volumeStream);
                    break;
                case 15:
                    repo.PlaySound(D, volumeStream);
                    break;
                case 16:
                    repo.PlaySound(Dsharp, volumeStream);
                    break;
                case 17:
                    repo.PlaySound(E, volumeStream);
                    break;
                case 18:
                    repo.PlaySound(F, volumeStream);
                    break;
                case 19:
                    repo.PlaySound(Fsharp, volumeStream);
                    break;
                case 20:
                    repo.PlaySound(G, volumeStream);
                    break;
                case 21:
                    repo.PlaySound(Gsharp, volumeStream);
                    break;
                case 22:
                    repo.PlaySound(A, volumeStream);
                    break;
                case 23:
                    repo.PlaySound(Asharp, volumeStream);
                    break;
                case 24:
                    repo.PlaySound(B, volumeStream);
                    break;

                // 5 Octave
                case 25:
                    repo.PlaySound(C5, volumeStream);
                    break;
                case 26:
                    repo.PlaySound(Csharp5, volumeStream);
                    break;
                case 27:
                    repo.PlaySound(D5, volumeStream);
                    break;
                case 28:
                    repo.PlaySound(Dsharp5, volumeStream);
                    break;
                case 29:
                    repo.PlaySound(E5, volumeStream);
                    break;
                case 30:
                    repo.PlaySound(F5, volumeStream);
                    break;
                case 31:
                    repo.PlaySound(Fsharp5, volumeStream);
                    break;
                case 32:
                    repo.PlaySound(G5, volumeStream);
                    break;
                case 33:
                    repo.PlaySound(Gsharp5, volumeStream);
                    break;
                case 34:
                    repo.PlaySound(A5, volumeStream);
                    break;
                case 35:
                    repo.PlaySound(Asharp5, volumeStream);
                    break;
                case 36:
                    repo.PlaySound(B5, volumeStream);
                    break;

                // 5 Octave
                case 37:
                    repo.PlaySound(C6, volumeStream);
                    break;
            }
            await Task.Delay(350);
        }

        private async Task PlayChordAsync(int noteId, Button button)
        {


            switch (noteId)
            {
                // 3 Octave
                case 1:
                    repo.PlaySound(C3, volumeStream);
                    break;
                case 2:
                    repo.PlaySound(Csharp3, volumeStream);
                    break;
                case 3:
                    repo.PlaySound(D3, volumeStream);
                    break;
                case 4:
                    repo.PlaySound(Dsharp3, volumeStream);
                    break;
                case 5:
                    repo.PlaySound(E3, volumeStream);
                    break;
                case 6:
                    repo.PlaySound(F3, volumeStream);
                    break;
                case 7:
                    repo.PlaySound(Fsharp3, volumeStream);
                    break;
                case 8:
                    repo.PlaySound(G3, volumeStream);
                    break;
                case 9:
                    repo.PlaySound(Gsharp3, volumeStream);
                    break;
                case 10:
                    repo.PlaySound(A3, volumeStream);
                    break;
                case 11:
                    repo.PlaySound(Asharp3, volumeStream);
                    break;
                case 12:
                    repo.PlaySound(B3, volumeStream);
                    break;


                // 4 Octave
                case 13:
                    repo.PlaySound(C, volumeStream);
                    break;
                case 14:
                    repo.PlaySound(Csharp, volumeStream);
                    break;
                case 15:
                    repo.PlaySound(D, volumeStream);
                    break;
                case 16:
                    repo.PlaySound(Dsharp, volumeStream);
                    break;
                case 17:
                    repo.PlaySound(E, volumeStream);
                    break;
                case 18:
                    repo.PlaySound(F, volumeStream);
                    break;
                case 19:
                    repo.PlaySound(Fsharp, volumeStream);
                    break;
                case 20:
                    repo.PlaySound(G, volumeStream);
                    break;
                case 21:
                    repo.PlaySound(Gsharp, volumeStream);
                    break;
                case 22:
                    repo.PlaySound(A, volumeStream);
                    break;
                case 23:
                    repo.PlaySound(Asharp, volumeStream);
                    break;
                case 24:
                    repo.PlaySound(B, volumeStream);
                    break;

                // 5 Octave
                case 25:
                    repo.PlaySound(C5, volumeStream);
                    break;
                case 26:
                    repo.PlaySound(Csharp5, volumeStream);
                    break;
                case 27:
                    repo.PlaySound(D5, volumeStream);
                    break;
                case 28:
                    repo.PlaySound(Dsharp5, volumeStream);
                    break;
                case 29:
                    repo.PlaySound(E5, volumeStream);
                    break;
                case 30:
                    repo.PlaySound(F5, volumeStream);
                    break;
                case 31:
                    repo.PlaySound(Fsharp5, volumeStream);
                    break;
                case 32:
                    repo.PlaySound(G5, volumeStream);
                    break;
                case 33:
                    repo.PlaySound(Gsharp5, volumeStream);
                    break;
                case 34:
                    repo.PlaySound(A5, volumeStream);
                    break;
                case 35:
                    repo.PlaySound(Asharp5, volumeStream);
                    break;
                case 36:
                    repo.PlaySound(B5, volumeStream);
                    break;

                // 5 Octave
                case 37:
                    repo.PlaySound(C6, volumeStream);
                    break;
            }
            await Task.Delay(50);
        }

        // Volume Slider
        private void VolumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            float newVolume = (float)e.NewValue;
            if (newVolume > 1.0f)
            {
                volumeStream = 1.0f;

            }
            else if (newVolume < 0.0f)
            {
                volumeStream = 0.0f;


            }
            else
            {
                volumeStream = newVolume;
            }

        }

        // Transpose
        private void btnTranpose_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int transpose = Convert.ToInt32(tbTranspose.Text);
            ResetNotes();

            if (lbChords.SelectedValue != null)
            {
                if ((string)button.Content == "+")
                {
                    if (transpose < 12)
                    {
                        transpose += 1;
                        tbTranspose.Text = Convert.ToString(transpose);
                        if (selectedChord != null)
                        {
                            selectedChord.Transpose = transpose;
                            selectedChord = repo.CheckTranspose(selectedChord, button);
                            SelectedNotes(selectedChord);

                        }

                    }
                    else
                    {
                        tbTranspose.Text = Convert.ToString(transpose);
                        if (selectedChord != null)
                        {
                            selectedChord.Transpose = transpose + 1;
                            selectedChord = repo.CheckTranspose(selectedChord, button);
                            SelectedNotes(selectedChord);
                        }

                    }

                }
                else if ((string)button.Content == "-")
                {
                    if (transpose > 0)
                    {
                        transpose -= 1;
                        tbTranspose.Text = Convert.ToString(transpose);
                        if (selectedChord != null)
                        {
                            selectedChord.Transpose = transpose;
                            selectedChord = repo.CheckTranspose(selectedChord, button);
                            SelectedNotes(selectedChord);

                        }

                    }
                    else
                    {
                        tbTranspose.Text = Convert.ToString(transpose);
                        if (selectedChord != null)
                        {
                            selectedChord.Transpose = transpose - 1;
                            selectedChord = repo.CheckTranspose(selectedChord, button);
                            SelectedNotes(selectedChord);
                        }
                    }
                }
            }
            if (lbScales.SelectedValue != null)
            {
                if ((string)button.Content == "+")
                {
                    if (transpose < 12)
                    {
                        transpose += 1;
                        tbTranspose.Text = Convert.ToString(transpose);
                        if (selectedScale != null)
                        {
                            selectedScale.Transpose = transpose;
                            selectedScale = repo.CheckTranspose(selectedScale, button);
                            SelectedNotes(selectedScale);

                        }

                    }
                    else
                    {
                        tbTranspose.Text = Convert.ToString(transpose);
                        if (selectedScale != null)
                        {
                            selectedScale.Transpose = transpose + 1;
                            selectedScale = repo.CheckTranspose(selectedScale, button);
                            SelectedNotes(selectedScale);
                        }

                    }

                }
                else if ((string)button.Content == "-")
                {
                    if (transpose > 0)
                    {
                        transpose -= 1;
                        tbTranspose.Text = Convert.ToString(transpose);
                        if (selectedScale != null)
                        {
                            selectedScale.Transpose = transpose;
                            selectedScale = repo.CheckTranspose(selectedScale, button);
                            SelectedNotes(selectedScale);

                        }

                    }
                    else
                    {
                        tbTranspose.Text = Convert.ToString(transpose);
                        if (selectedScale != null)
                        {
                            selectedScale.Transpose = transpose - 1;
                            selectedScale = repo.CheckTranspose(selectedScale, button);
                            SelectedNotes(selectedScale);
                        }
                    }
                }
            }
        }

        private void lbChords_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Chord newChord = lbChords.SelectedValue as Chord;
            selectedChord = repo.GetChord(newChord);
            selectedChord.RootNote = (string)lbNotes.SelectedValue;
            selectedChord.Name = newChord.Name;

            if (btnBeat1.Content == string.Empty)
            {
                btnBeat1.Content = selectedChord.RootNote + " " + selectedChord.Name;
                repo.CheckNote(selectedChord);
                btnBeat1.DataContext = selectedChord;
                chordProgression.Add(selectedChord);
                return;
            }
            if (btnBeat2.Content == string.Empty)
            {
                btnBeat2.Content = selectedChord.RootNote + " " + selectedChord.Name;
                repo.CheckNote(selectedChord);
                btnBeat2.DataContext = selectedChord;
                chordProgression.Add(selectedChord);
                return;
            }
            if (btnBeat3.Content == string.Empty)
            {
                btnBeat3.Content = selectedChord.RootNote + " " + selectedChord.Name;
                repo.CheckNote(selectedChord);
                btnBeat3.DataContext = selectedChord;
                chordProgression.Add(selectedChord);
                return;
            }
            if (btnBeat4.Content == string.Empty)
            {
                btnBeat4.Content = selectedChord.RootNote + " " + selectedChord.Name;
                repo.CheckNote(selectedChord);
                btnBeat4.DataContext = selectedChord;
                chordProgression.Add(selectedChord);
                return;
            }

        }

        // Midi export
        private void btnExportMidi_Click(object sender, RoutedEventArgs e)
        {
            repo.CreateMidiFile(chordProgression);
        }
        private void btnResetMidi_Click(object sender, RoutedEventArgs e)
        {
            btnBeat1.DataContext = null;
            btnBeat2.DataContext = null;
            btnBeat3.DataContext = null;
            btnBeat4.DataContext = null;

            btnBeat1.Content = string.Empty;
            btnBeat2.Content = string.Empty;
            btnBeat3.Content = string.Empty;
            btnBeat4.Content = string.Empty;

        }
    }
}
