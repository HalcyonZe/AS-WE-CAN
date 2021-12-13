using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Speech : MonoBehaviour
{
    private PhraseRecognizer m_phraseRecognizer;
    public string[] keywords = { };
    public ConfidenceLevel m_confidenceLevel = ConfidenceLevel.Medium;

    private float speechTime = -1.0f;
    private bool canSpeech = false;

    public float volume;
    AudioClip micRecord;
    string device;

    // Start is called before the first frame update
    void Start()
    {
        keywords = new string[] {"we can" };
        if(m_phraseRecognizer == null)
        {
            m_phraseRecognizer = new KeywordRecognizer(keywords, m_confidenceLevel);
            m_phraseRecognizer.OnPhraseRecognized += M_PhraseRecognizer;
            //m_phraseRecognizer.Start();
        }
        device = Microphone.devices[0];
        micRecord = Microphone.Start(device, true, 999, 44100);
    }

    private void FixedUpdate()
    {           
        //StartSpeech();
        if (!canSpeech)
        {
            StartSpeech();
        }
        else
        {            
            speechTime -= Time.fixedDeltaTime;
            if (speechTime <= 0)
            {
                StopRecognizer();
                canSpeech = false;
                Debug.Log("时间到!");
            }
            volume = GetVolume();
        }
        
    }

    private void StartSpeech()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartRecognizer();
            speechTime = 50.0f;
            canSpeech = true;
            
        }
    }

    private void StartRecognizer()
    {
        m_phraseRecognizer.Start();
        Debug.Log("开始识别!");
    }

    private void StopRecognizer()
    {
        m_phraseRecognizer.Stop();
        Debug.Log("停止识别!");
    }

    private void M_PhraseRecognizer(PhraseRecognizedEventArgs args)
    {
        print("识别成功!");
        StopRecognizer();
        canSpeech = false;

    }

    private float GetVolume()
    {
        float maxVolume = 0f;
        float[] volumeData = new float[128];
        int offset = Microphone.GetPosition(device) - 128 + 1;
        if (offset < 0)
        {
            return 0;
        }

        micRecord.GetData(volumeData, offset);

        for(int i = 0; i < 128; i++)
        {
            float tempMax = volumeData[i];
            if(maxVolume < tempMax)
            {
                maxVolume = tempMax;
            }
        }
        return maxVolume;
    }

}
