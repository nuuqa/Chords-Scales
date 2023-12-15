using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows.Controls;


namespace Chords
{
    class ChordsRepo
    {
        // Database connections
        private const string connectionString = "Data Source=ChordsAndScales.db;Version=3;";
        private IWavePlayer audioOut;
        private MixingSampleProvider mixOut;


        public ChordsRepo()
        {
            audioOut = new DirectSoundOut();
            mixOut = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2)); // 44.1khz, Stereo.
            mixOut.ReadFully = true;
            audioOut.Init(mixOut);
            audioOut.Play();
        }

        /// <summary>
        /// Get all the chords from database
        /// </summary>
        /// <returns>Chords</returns>
        public ObservableCollection<Chord> GetChords(string note)
        {
            ObservableCollection<Chord> chords = new ObservableCollection<Chord>();

            string sqlSelect = "SELECT * FROM Chords WHERE chordName LIKE @chordName";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(sqlSelect, connection))
                {
                    command.Parameters.AddWithValue("@chordName", note + "%");

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chords.Add(new Chord
                            {
                                Id = Convert.ToInt32(reader["chordID"]),
                                Name = Convert.ToString(reader["chordName"])
                            });
                        }
                    }
                }
            }

            return chords;
        }

        /// <summary>
        /// Get specific chord from the database
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public Chord GetChord(Chord chord)
        {
            Chord newChord = new Chord();
            string sqlSelect = "SELECT Notes.noteName, Notes.octave, Notes.noteID, Notes.midiValue, ChordNotes.chordPosition " +
                                "FROM Chords " +
                                "JOIN ChordNotes ON Chords.chordID = ChordNotes.ChordID " +
                                "JOIN Notes ON ChordNotes.NoteID = Notes.noteID " +
                                "WHERE Chords.chordName = @chordName;";

            if (chord != null)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(sqlSelect, connection))
                    {
                        command.Parameters.AddWithValue("@chordName", chord.Name);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                newChord.Notes.Add(new Note
                                {
                                    Id = Convert.ToInt32(reader["noteID"]),
                                    Name = Convert.ToString(reader["noteName"]),
                                    Octave = Convert.ToInt32(reader["octave"]),
                                    MidiValue = Convert.ToInt32(reader["midiValue"])
                                });
                            }
                        }
                    }
                }
            }

            return newChord;
        }


        /// <summary>
        /// Gets specific scale from the database
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public Scale GetScale(Scale scale)
        {
            Scale newScale = new Scale();
            string sqlSelect = "SELECT Notes.noteName, Notes.octave, Notes.noteID, ScaleNotes.scalePosition " +
                                "FROM Scales " +
                                "JOIN ScaleNotes ON Scales.scaleID = ScaleNotes.ScaleID " +
                                "JOIN Notes ON ScaleNotes.NoteID = Notes.noteID " +
                                "WHERE Scales.name = @scaleName;";

            if (scale != null)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(sqlSelect, connection))
                    {
                        command.Parameters.AddWithValue("@scaleName", scale.Name);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                newScale.Notes.Add(new Note
                                {
                                    Id = Convert.ToInt32(reader["noteID"]),
                                    Name = Convert.ToString(reader["noteName"]),
                                    Octave = Convert.ToInt32(reader["octave"])
                                });
                            }
                        }
                    }
                }
            }

            return newScale;
        }


        /// <summary>
        /// Gets all the chords from the database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Chord> GetAllChords()
        {
            ObservableCollection<Chord> chords = new ObservableCollection<Chord>();
            string sqlSelect = "SELECT chordName FROM Chords;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(sqlSelect, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chords.Add(new Chord
                            {
                                Name = Convert.ToString(reader["chordName"])
                            });
                        }
                    }
                }
            }

            return chords;
        }


        /// <summary>
        /// Gets all scales from the database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Scale> GetAllScales()
        {
            ObservableCollection<Scale> scales = new ObservableCollection<Scale>();
            string sqlSelect = "SELECT name FROM Scales;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(sqlSelect, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scales.Add(new Scale
                            {
                                Name = Convert.ToString(reader["name"])
                            });
                        }
                    }
                }
            }

            return scales;
        }


        /// <summary>
        /// Transpose for chords
        /// </summary>
        /// <param name="chord"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public Chord CheckTranspose(Chord chord, Button button)
        {
            foreach (var item in chord.Notes)
            {
                switch ((string)button.Content)
                {
                    case "+":
                        if (chord.Transpose > 12)
                        {
                            item.Id += 0;
                        }
                        else
                        {
                            item.Id += 1;
                        }

                        break;

                    case "-":
                        if (chord.Transpose < 0)
                        {
                            item.Id -= 0;
                        }
                        else
                        {
                            item.Id -= 1;
                        }
                        break;
                }
            }
            return chord;
        }

        /// <summary>
        /// Transpose for scales
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public Scale CheckTranspose(Scale scale, Button button)
        {
            foreach (var item in scale.Notes)
            {
                switch ((string)button.Content)
                {
                    case "+":
                        if (scale.Transpose > 12)
                        {
                            item.Id += 0;
                        }
                        else
                        {
                            item.Id += 1;
                        }

                        break;

                    case "-":
                        if (scale.Transpose < 0)
                        {
                            item.Id -= 0;
                        }
                        else
                        {
                            item.Id -= 1;
                        }
                        break;
                }
            }
            return scale;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chord"></param>
        /// <returns></returns>
        public Chord CheckNote(Chord chord)
        {

            if (chord.RootNote == "C#/Db")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 1;
                    item.MidiValue += 1;
                }
                return chord;
            }
            if (chord.RootNote == "D")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 2;
                    item.MidiValue += 2;
                }
                return chord;
            }
            if (chord.RootNote == "D#/Eb")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 3;
                    item.MidiValue += 3;
                }
                return chord;
            }
            if (chord.RootNote == "E")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 4;
                    item.MidiValue += 4;
                }
                return chord;
            }
            if (chord.RootNote == "F")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 5;
                    item.MidiValue += 5;
                }
                return chord;
            }
            if (chord.RootNote == "F#/Gb")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 6;
                    item.MidiValue += 6;
                }
                return chord;
            }
            if (chord.RootNote == "G")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 7;
                    item.MidiValue += 7;
                }
                return chord;
            }
            if (chord.RootNote == "G#/Ab")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 8;
                    item.MidiValue += 8;
                }
                return chord;
            }
            if (chord.RootNote == "A")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 9;
                    item.MidiValue += 9;
                }
                return chord;
            }
            if (chord.RootNote == "A#/Bb")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 10;
                    item.MidiValue += 10;
                }
                return chord;
            }
            if (chord.RootNote == "B")
            {
                foreach (var item in chord.Notes)
                {
                    item.Id += 11;
                    item.MidiValue += 11;
                }
                return chord;
            }
            return chord;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public Scale CheckNote(Scale scale)
        {

            if (scale.RootNote == "C#/Db")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 1;
                    item.MidiValue += 1;
                }
                return scale;
            }
            if (scale.RootNote == "D")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 2;
                    item.MidiValue += 2;
                }
                return scale;
            }
            if (scale.RootNote == "D#/Eb")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 3;
                    item.MidiValue += 3;
                }
                return scale;
            }
            if (scale.RootNote == "E")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 4;
                    item.MidiValue += 4;
                }
                return scale;
            }
            if (scale.RootNote == "F")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 5;
                    item.MidiValue += 5;
                }
                return scale;
            }
            if (scale.RootNote == "F#/Gb")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 6;
                    item.MidiValue += 6;
                }
                return scale;
            }
            if (scale.RootNote == "G")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 7;
                    item.MidiValue += 7;
                }
                return scale;
            }
            if (scale.RootNote == "G#/Ab")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 8;
                    item.MidiValue += 8;
                }
                return scale;
            }
            if (scale.RootNote == "A")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 9;
                    item.MidiValue += 9;
                }
                return scale;
            }
            if (scale.RootNote == "A#/Bb")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 10;
                    item.MidiValue += 10;
                }
                return scale;
            }
            if (scale.RootNote == "B")
            {
                foreach (var item in scale.Notes)
                {
                    item.Id += 11;
                    item.MidiValue += 11;
                }
                return scale;
            }
            return scale;
        }

        /// <summary>
        /// Creates a midi file from selected chords
        /// </summary>
        /// <param name="chords"></param>
        public void CreateMidiFile(ObservableCollection<Chord> chords)
        {
            var midiFile = new Melanchall.DryWetMidi.Core.MidiFile();
            var deltaTime = 0;
            MidiFileFormat format = MidiFileFormat.SingleTrack;
            string fileName = string.Empty;

            foreach (var chord in chords)
            {
                Chord newChord = CheckNote(chord);


                fileName += chord.RootNote + chord.Name;
                foreach (var note in newChord.Notes)
                {
                    var trackChunk = new TrackChunk();
                    var noteOn = new Melanchall.DryWetMidi.Core.NoteOnEvent((SevenBitNumber)note.MidiValue, (SevenBitNumber)100) { DeltaTime = deltaTime };
                    var noteOff = new NoteOffEvent((SevenBitNumber)note.MidiValue, (SevenBitNumber)0) { DeltaTime = 384 };
                    trackChunk.Events.Add(noteOn);
                    trackChunk.Events.Add(noteOff);
                    midiFile.Chunks.Add(trackChunk);
                }
                deltaTime += 384;
            }

            // Checks for illegal characters in filename.
            if (fileName.Contains("/"))
            {
                fileName = fileName.Replace("/", "_");
            }

            midiFile.Write(fileName + ".mid", true, format);

        }

        /// <summary>
        /// Adds the file to mixer.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="volume"></param>
        public void PlaySound(string filePath, float volume)
        {
            AudioFileReader audioFileReader = new AudioFileReader(filePath);
            mixOut.AddMixerInput(new DisposeAudioReader(audioFileReader, volume));

        }
    }
}
