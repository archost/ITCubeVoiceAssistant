using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVisualizer : MonoBehaviour
{
    private AudioSource _audioSource;

    public static float[] _samples = new float[512];

    public static float[] _freqBand = new float[8];

    public static float[] _bandBuffer = new float[8];

    private float[] _bufferDecrease = new float[8];

    private float[] _highestFreqBand = new float[8];

    /// <summary>
    /// Normalized frequency volume values
    /// </summary>
    public static float[] _audioBand = new float[8];

    /// <summary>
    /// Buffered normalized frequency volume values
    /// </summary>
    public static float[] _audioBandBuffer = new float[8];

    public static float _amplitude, _amplitudeBuffer;

    private float _highestAmplitude;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    private void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    private void MakeFrequencyBands()
    {
        for (int i = 0; i < 8; i++)
        {
            float avg = 0f;
            int sampleCount = (int)Mathf.Pow(2, i + 1);

            if (i == 7)
                sampleCount += 2;

            for (int count = 0; count < sampleCount; count++)
                avg += _samples[count] * (count + 1);

            avg /= sampleCount;
            _freqBand[i] = avg * 10;
        }
    }

    private void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _bandBuffer[i])
            {
                _bandBuffer[i] = _freqBand[i];
                _bufferDecrease[i] = 0.005f;

            }
            if (_freqBand[i] < _bandBuffer[i])
            {
                _bandBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.4f;
            }
        }
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            _highestFreqBand[i] = Mathf.Max(_freqBand[i], _highestFreqBand[i]);
            _audioBand[i] = Mathf.Max(0, _freqBand[i] / _highestFreqBand[i]);
            _audioBandBuffer[i] = Mathf.Max(0, _bandBuffer[i] / _highestFreqBand[i]);
        }
    }

    void GetAmplitude()
    {
        float currentAmp = 0, currentAmpBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            currentAmp += _audioBand[i];
            currentAmpBuffer += _audioBandBuffer[i];
        }
        _highestAmplitude = Mathf.Max(_highestAmplitude, currentAmp);
        _amplitude = currentAmp / _highestAmplitude;
        _amplitudeBuffer = currentAmpBuffer / _highestAmplitude;
    }
}