using UnityEngine;

public class WavConverter : MonoBehaviour
{
    // Converts byte data to an AudioClip
    public static AudioClip ToAudioClip(byte[] data, string clipName = "GeneratedAudioClip")
    {
        WAV wav = new WAV(data);    // Parse WAV data
        AudioClip audioClip = AudioClip.Create(clipName, wav.SampleCount, 1, wav.Frequency, false);
        audioClip.SetData(wav.LeftChannel, 0);  // Set audio data
        return audioClip;
    }

    // Internal class to parse WAV file data
    private class WAV
    {
        public float[] LeftChannel { get; }
        public int SampleCount { get; }
        public int Frequency { get; }

        public WAV(byte[] wav)
        {
            int channels = wav[22];
            Frequency = wav[24] | (wav[25] << 8);

            int pos = 12;
            while (!(wav[pos] == 100 && wav[pos + 1] == 97 && wav[pos + 2] == 116 && wav[pos + 3] == 97))
            {
                pos += 4;
                int chunkSize = wav[pos] + (wav[pos + 1] << 8);
                pos += 4 + chunkSize;
            }
            pos += 8;

            SampleCount = (wav.Length - pos) / 2;
            LeftChannel = new float[SampleCount];

            for (int i = 0; i < SampleCount; i++)
            {
                LeftChannel[i] = (short)(wav[pos + i * 2] | (wav[pos + i * 2 + 1] << 8)) / 32768.0f;
            }
        }
    }
}