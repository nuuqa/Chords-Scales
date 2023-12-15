using System.Data.SQLite;
using System.Diagnostics;


namespace Chords
{
    class ChordsDatabase
    {
        private const string connectionString = "Data Source=ChordsAndScales.db;Version=3;";

        /// <summary>
        /// Creates SQLITE database
        /// </summary>
        public void CreateDatabaseIfNotExists()
        {

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Notes Table
                string createTableNotes = "CREATE TABLE Notes (noteID INTEGER PRIMARY KEY AUTOINCREMENT, noteName TEXT NOT NULL, octave INTEGER NOT NULL, midiValue INTEGER NOT NULL);";
                using (SQLiteCommand command = new SQLiteCommand(createTableNotes, conn))
                {
                    command.ExecuteNonQuery();
                }

                // Chords Table
                string createTableChords = "CREATE TABLE Chords (chordID INTEGER PRIMARY KEY AUTOINCREMENT, chordName TEXT NOT NULL);";
                using (SQLiteCommand command = new SQLiteCommand(createTableChords, conn))
                {
                    command.ExecuteNonQuery();
                }

                // ChordNotes Table
                string createTableChordNotes = "CREATE TABLE ChordNotes (ChordID INTEGER NOT NULL, NoteID INTEGER NOT NULL, ChordPosition INTEGER, Octave INTEGER NOT NULL, PRIMARY KEY (ChordID, NoteID), FOREIGN KEY (ChordID) REFERENCES Chords(chordID), FOREIGN KEY (NoteID) REFERENCES Notes(noteID));";
                using (SQLiteCommand command = new SQLiteCommand(createTableChordNotes, conn))
                {
                    command.ExecuteNonQuery();
                }

                // Scales Table
                string createTableScales = "CREATE TABLE Scales (scaleID INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL);";
                using (SQLiteCommand command = new SQLiteCommand(createTableScales, conn))
                {
                    command.ExecuteNonQuery();
                }

                // ScaleNotes Table
                string createTableScaleNotes = "CREATE TABLE ScaleNotes (ScaleID INTEGER NOT NULL, NoteID INTEGER NOT NULL, ScalePosition INTEGER NOT NULL, PRIMARY KEY (ScaleID, NoteID), FOREIGN KEY (ScaleID) REFERENCES Scales(scaleID), FOREIGN KEY (NoteID) REFERENCES Notes(noteID));";
                using (SQLiteCommand command = new SQLiteCommand(createTableScaleNotes, conn))
                {
                    command.ExecuteNonQuery();
                }


            }

        }


        /// <summary>
        /// Inserts note data to database
        /// </summary>
        public void InsertNotesData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string insertData = @"
                INSERT INTO Notes (noteName, octave, midiValue) VALUES 
                ('C3', 3, 60),
                ('C#/Db3', 3, 61),
                ('D3', 3, 62),
                ('D#/Eb3', 3, 63),
                ('E3', 3, 64),
                ('F3', 3, 65),
                ('F#/Gb3', 3, 66),
                ('G3', 3, 67),
                ('G#/Ab3', 3, 68),
                ('A3', 3, 69),
                ('A#/Bb3', 3, 70),
                ('B3', 3, 71),
                ('C4', 4, 72),
                ('C#/Db4', 4, 73),
                ('D4', 4, 74),
                ('D#/Eb4', 4, 75),
                ('E4', 4, 76),
                ('F4', 4, 77),
                ('F#/Gb4', 4, 78),
                ('G4', 4, 79),
                ('G#/Ab4', 4, 80),
                ('A4', 4, 81),
                ('A#/Bb4', 4, 82),
                ('B4', 4, 83),
                ('C5', 5, 84);
                ";

                using (SQLiteCommand command = new SQLiteCommand(insertData, conn))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Debug.WriteLine("Lisäys onnistui" + rowsAffected);
                }
            }
        }

        /// <summary>
        /// Inserts chords data to database
        /// </summary>
        public void InsertChordsData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string insertData = @"
                INSERT INTO Chords (chordName) VALUES 
                ('Maj'),
                ('Min'),
                ('5'),
                ('7th'),
                ('Maj7'),
                ('Min7'),
                ('Min Maj7'),
                ('Sus4'),
                ('Sus2'),
                ('Dim'),
                ('Dim7'),
                ('Aug'),
                ('6'),
                ('Min6'),
                ('9'),
                ('Min9'),
                ('Maj9'),
                ('Min Maj9'),
                ('11'),
                ('Min11'),
                ('Maj11'),
                ('Min Maj11'),
                ('13'),
                ('Min13'),
                ('Maj13'),
                ('Min Maj13'),
                ('add9'),
                ('Min add9'),
                ('Min 6 add9'),
                ('7th add9'),
                ('6 sus4'),
                ('7th sus4'),
                ('Maj7th sus4'),
                ('9 sus4'),
                ('Maj9 sus4');";

                using (SQLiteCommand command = new SQLiteCommand(insertData, conn))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Debug.WriteLine("Lisäys onnistui" + rowsAffected);
                }
            }
        }

        /// <summary>
        /// Inserts scales data to database
        /// </summary>
        public void InsertScalesData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string insertData = @"
                    INSERT INTO Scales (scaleID, name) VALUES
                    (1, 'Major Scale'),
                    (2, 'Harmonic Minor Scale'),
                    (3, 'Melodic Minor (Ascending) Scale'),
                    (4, 'Melodic Minor (Descending) Scale'),
                    (5, 'Chromatic Scale'),
                    (6, 'Whole Tone Scale'),
                    (7, 'Pentatonic Major Scale'),
                    (8, 'Pentatonic Minor Scale'),
                    (9, 'Pentatonic Blues Scale'),
                    (10, 'Pentatonic Neutral Scale'),
                    (11, 'Octatonic (H-W) Scale'),
                    (12, 'Octatonic (W-H) Scale'),
                    (13, 'Ionian Scale'),
                    (14, 'Dorian Scale'),
                    (15, 'Phrygian Scale'),
                    (16, 'Lydian Scale'),
                    (17, 'Mixolydian Scale'),
                    (18, 'Aeolian Scale'),
                    (19, 'Locrian Scale');";

                using (SQLiteCommand command = new SQLiteCommand(insertData, conn))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Debug.WriteLine("Lisäys onnistui" + rowsAffected);
                }
            }
        }

        /// <summary>
        /// Inserts Chord notes data to database
        /// </summary>
        public void InsertChordNotesData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string insertData = @"
                    INSERT INTO ChordNotes (ChordID, NoteID, ChordPosition, Octave) VALUES
                    -- C Maj
                    (1, 1, 1, 3),
                    (1, 5, 2, 3),
                    (1, 8, 3, 3),
                    -- C Min
                    (2, 1, 1, 3),
                    (2, 4, 2, 3),
                    (2, 8, 3, 3),
                    -- C 5
                    (3, 1, 1, 3),
                    (3, 8, 2, 3),
                    -- C 7th
                    (4, 1, 1, 3),
                    (4, 5, 2, 3),
                    (4, 8, 3, 3),
                    (4, 11, 4, 3),
                    -- C Maj7
                    (5, 1, 1, 3),
                    (5, 5, 2, 3),
                    (5, 8, 3, 3),
                    (5, 12, 4, 3),
                    -- C Min7
                    (6, 1, 1, 3),
                    (6, 4, 2, 3),
                    (6, 8, 3, 3),
                    (6, 11, 4, 3),
                    -- C Min Maj7
                    (7, 1, 1, 3),
                    (7, 4, 2, 3),
                    (7, 8, 3, 3),
                    (7, 12, 4, 3),
                    -- C Sus4
                    (8, 1, 1, 3),
                    (8, 6, 2, 3),
                    (8, 8, 3, 3),
                    -- C Sus2
                    (9, 1, 1, 3),
                    (9, 3, 2, 3),
                    (9, 8, 3, 3),
                    -- C Dim
                    (10, 1, 1, 3),
                    (10, 4, 2, 3),
                    (10, 7, 3, 3),
                    -- C Dim7
                    (11, 1, 1, 3),
                    (11, 4, 2, 3),
                    (11, 7, 3, 3),
                    (11, 10, 4, 3),
                    -- C Aug
                    (12, 1, 1, 3),
                    (12, 5, 2, 3),
                    (12, 9, 3, 3),
                    -- C 6
                    (13, 1, 1, 3),
                    (13, 5, 2, 3),
                    (13, 8, 3, 3),
                    (13, 10, 4, 3),
                    -- C Min6
                    (14, 1, 1, 3),
                    (14, 4, 2, 3),
                    (14, 8, 3, 3),
                    (14, 10, 4, 3),
                    -- C 9
                    (15, 1, 1, 3),
                    (15, 5, 2, 3),
                    (15, 8, 3, 3),
                    (15, 11, 4, 3),
                    (15, 3, 6, 3),
                    
                    -- C Min9
                    (16, 1, 1, 3),
                    (16, 4, 2, 3),
                    (16, 8, 3, 3),
                    (16, 11, 4, 3),
                    (16, 3, 6, 3),
                    -- C Maj9
                    (17, 1, 1, 3),
                    (17, 5, 2, 3),
                    (17, 8, 3, 3),
                    (17, 12, 4, 3),
                    (17, 3, 6, 3),
                    -- C Min Maj9
                    (18, 1, 1, 3),
                    (18, 4, 2, 3),
                    (18, 8, 3, 3),
                    (18, 12, 4, 3),
                    (18, 3, 6, 3),
                    
                    -- C 11
                    (19, 1, 1, 3),
                    (19, 5, 2, 3),
                    (19, 8, 3, 3),
                    (19, 11, 4, 3),
                    (19, 3, 5, 3),
                    (19, 6, 6, 3),
                    (19, 13, 6, 3),
                    -- C Min11
                    (20, 1, 1, 3),
                    (20, 4, 2, 3),
                    (20, 8, 3, 3),
                    (20, 11, 4, 3),
                    (20, 3, 5, 3),
                    (20, 6, 6, 3),
                    (20, 13, 6, 3),
                    -- C Maj11
                    (21, 1, 1, 3),
                    (21, 5, 2, 3),
                    (21, 8, 3, 3),
                    (21, 12, 4, 3),
                    (21, 3, 5, 3),
                    (21, 6, 6, 3),
                    (21, 13, 6, 3),
                    -- C Min Maj11
                    (22, 1, 1, 3),
                    (22, 3, 1, 3),
                    (22, 4, 2, 3),
                    (22, 8, 3, 3),
                    (22, 12, 4, 3),
                    (22, 6, 6, 3),
                    (22, 13, 6, 3),
                    -- C 13
                    (23, 1, 1, 4),
                    (23, 3, 2, 4),
                    (23, 5, 3, 4),
                    (23, 8, 4, 4),
                    (23, 10, 5, 4),
                    (23, 11, 6, 4),
                    -- C Min13
                    (24, 1, 1, 4),
                    (24, 3, 2, 4),
                    (24, 4, 3, 4),
                    (24, 8, 4, 4),
                    (24, 10, 5, 4),
                    (24, 11, 6, 4),
                    -- C Maj13
                    (25, 1, 1, 4),
                    (25, 3, 2, 4),
                    (25, 5, 3, 4),
                    (25, 8, 4, 4),
                    (25, 10, 5, 4),
                    (25, 12, 6, 4),
                    -- C Min Maj13
                    (26, 1, 1, 4),
                    (26, 3, 2, 4),
                    (26, 4, 3, 4),
                    (26, 8, 4, 4),
                    (26, 10, 5, 4),
                    (26, 12, 6, 4),
                    -- C add9
                    (27, 1, 1, 4),
                    (27, 3, 2, 4),
                    (27, 5, 3, 4),
                    (27, 8, 4, 4),
                    -- C Min add9
                    (28, 1, 1, 4),
                    (28, 3, 2, 4),
                    (28, 4, 3, 4),
                    (28, 8, 4, 4),
                    -- C Min 6 add9
                    (29, 1, 1, 4),
                    (29, 3, 2, 4),
                    (29, 4, 3, 4),
                    (29, 8, 4, 4),
                    (29, 10, 5, 4),
                    -- C 7th add9
                    (30, 1, 1, 4),
                    (30, 5, 2, 4),
                    (30, 6, 3, 4),
                    (30, 8, 4, 4),
                    (30, 11, 5, 4),
                    -- C 6 sus4
                    (31, 1, 1, 4),
                    (31, 6, 2, 4),
                    (31, 8, 3, 4),
                    (31, 10, 4, 4),
                    -- C 7th sus4
                    (32, 1, 1, 4),
                    (32, 6, 2, 4),
                    (32, 8, 3, 4),
                    (32, 11, 4, 4),
                    -- C Maj7th sus4
                    (33, 1, 1, 4),
                    (33, 6, 2, 4),
                    (33, 8, 3, 4),
                    (33, 12, 4, 4),
                    -- C 9 sus4
                    (34, 1, 1, 4),
                    (34, 3, 2, 4),
                    (34, 6, 3, 4),
                    (34, 8, 4, 4),
                    (34, 11, 5, 4),
                    -- C Maj9 sus4
                    (35, 1, 1, 4),
                    (35, 3, 2, 4),
                    (35, 6, 3, 4),
                    (35, 8, 4, 4),
                    (35, 12, 5, 4);";

                using (SQLiteCommand command = new SQLiteCommand(insertData, conn))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Debug.WriteLine("Lisäys onnistui" + rowsAffected);
                }
            }
        }

        /// <summary>
        /// Inserts Scale notes data to database
        /// </summary>
        public void InsertScaleNotesData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string insertData = @"
                    INSERT INTO ScaleNotes (ScaleID, NoteID, ScalePosition) VALUES
                    -- Major Scale
                    (1, 1, 1),
                    (1, 3, 2),
                    (1, 5, 3),
                    (1, 6, 4),
                    (1, 8, 5),
                    (1, 10, 6),
                    (1, 12, 7),
                    -- Harmonic Minor Scale
                    (2, 1, 1),
                    (2, 3, 2),
                    (2, 4, 3),
                    (2, 6, 4),
                    (2, 8, 5),
                    (2, 9, 6),
                    (2, 12, 7),
                    -- Melodic Minor (Ascending) Scale
                    (3, 1, 1),
                    (3, 3, 2),
                    (3, 4, 3),
                    (3, 6, 4),
                    (3, 8, 5),
                    (3, 10, 6),
                    (3, 12, 7),
                    -- Melodic Minor (Descending) Scale
                    (4, 1, 1),
                    (4, 3, 2),
                    (4, 4, 3),
                    (4, 6, 4),
                    (4, 8, 5),
                    (4, 9, 6),
                    (4, 11, 7),
                    -- Chromatic Scale
                    (5, 1, 1),
                    (5, 2, 2),
                    (5, 3, 3),
                    (5, 4, 4),
                    (5, 5, 5),
                    (5, 6, 6),
                    (5, 7, 7),
                    (5, 8, 8),
                    (5, 9, 9),
                    (5, 10, 10),
                    (5, 11, 11),
                    (5, 12, 12),
                    -- Whole Tone Scale
                    (6, 1, 1),
                    (6, 3, 2),
                    (6, 5, 3),
                    (6, 7, 4),
                    (6, 9, 5),
                    (6, 11, 6),
                    -- Pentatonic Major Scale
                    (7, 1, 1),
                    (7, 3, 2),
                    (7, 5, 3),
                    (7, 8, 4),
                    (7, 10, 5),
                    -- Pentatonic Minor Scale
                    (8, 1, 1),
                    (8, 4, 2),
                    (8, 6, 3),
                    (8, 8, 4),
                    (8, 11, 5),
                    -- Pentatonic Blues Scale
                    (9, 1, 1),
                    (9, 4, 2),
                    (9, 6, 3),
                    (9, 7, 4),
                    (9, 8, 5),
                    (9, 11, 6),
                    -- Pentatonic Neutral Scale
                    (10, 1, 1),
                    (10, 3, 2),
                    (10, 6, 3),
                    (10, 8, 4),
                    (10, 11, 5),
                    -- Octatonic (H-W) Scale
                    (11, 1, 1),
                    (11, 2, 2),
                    (11, 4, 3),
                    (11, 5, 4),
                    (11, 7, 5),
                    (11, 8, 6),
                    (11, 10, 7),
                    (11, 11, 8),
                    -- Octatonic (W-H) Scale
                    (12, 1, 1),
                    (12, 3, 2),
                    (12, 4, 3),
                    (12, 6, 4),
                    (12, 7, 5),
                    (12, 9, 6),
                    (12, 10, 7),
                    (12, 12, 8),
                    -- Ionian Scale
                    (13, 1, 1),
                    (13, 3, 2),
                    (13, 5, 3),
                    (13, 6, 4),
                    (13, 8, 5),
                    (13, 10, 6),
                    (13, 12, 7),
                    -- Dorian Scale
                    (14, 1, 1),
                    (14, 3, 2),
                    (14, 4, 3),
                    (14, 6, 4),
                    (14, 8, 5),
                    (14, 10, 6),
                    (14, 11, 7),
                    -- Phrygian Scale
                    (15, 1, 1),
                    (15, 2, 2),
                    (15, 4, 3),
                    (15, 6, 4),
                    (15, 8, 5),
                    (15, 9, 6),
                    (15, 11, 7),
                    -- Lydian Scale
                    (16, 1, 1),
                    (16, 3, 2),
                    (16, 5, 3),
                    (16, 7, 4),
                    (16, 8, 5),
                    (16, 10, 6),
                    (16, 12, 7),
                    -- Mixolydian Scale
                    (17, 1, 1),
                    (17, 3, 2),
                    (17, 5, 3),
                    (17, 6, 4),
                    (17, 8, 5),
                    (17, 10, 6),
                    (17, 11, 7),
                    -- Aeolian Scale
                    (18, 1, 1),
                    (18, 3, 2),
                    (18, 4, 3),
                    (18, 6, 4),
                    (18, 8, 5),
                    (18, 9, 6),
                    (18, 11, 7),
                    -- Locrian Scale
                    (19, 1, 1),
                    (19, 2, 2),
                    (19, 4, 3),
                    (19, 6, 4),
                    (19, 7, 5),
                    (19, 9, 6),
                    (19, 11, 7);";

                using (SQLiteCommand command = new SQLiteCommand(insertData, conn))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Debug.WriteLine("Lisäys onnistui" + rowsAffected);
                }
            }
        }

        /// <summary>
        /// Runs all the data insert methods
        /// </summary>
        public void InsertData()
        {
            InsertNotesData();
            InsertChordsData();
            InsertScalesData();
            InsertChordNotesData();
            InsertScaleNotesData();

        }
    }
}