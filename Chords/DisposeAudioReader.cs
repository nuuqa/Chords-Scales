using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Chords
{
    /// <summary>
    /// Based on https://markheath.net/post/fire-and-forget-audio-playback-with
    /// </summary>
    public class DisposeAudioReader : ISampleProvider
    {
        private bool isDisposed;
        private readonly AudioFileReader reader;
        private readonly VolumeSampleProvider volumeProvider;

        public WaveFormat WaveFormat
        {
            get { return volumeProvider.WaveFormat; }
        }

        public DisposeAudioReader(AudioFileReader reader, float volume)
        {
            this.reader = reader;
            this.volumeProvider = new VolumeSampleProvider(reader.ToSampleProvider()) { Volume = volume };
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (isDisposed)
            {
                return 0;
            }
            int read = volumeProvider.Read(buffer, offset, count);
            if (read == 0)
            {
                reader.Dispose();
                isDisposed = true;
            }
            return read;
        }
    }
}
