using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeGet : MonoBehaviour
{
    AudioClip micRecord;
    string device;
    public float volume;

    private float interval_volume = 10.0f;
    private float time_volume = 0;

    // Start is called before the first frame update
    void Start()
    {
        device = Microphone.devices[0];
        micRecord = Microphone.Start(device, true, 999, 44100);
    }

    // Update is called once per frame
    void Update()
    {
        VolumeController();
        //volume = GetVolume();
    }

    private void VolumeController()
    {
        if (time_volume != 0)
        {
            volume = GetVolume();
            SetGunInterval();
            time_volume -= Time.deltaTime;
            if (time_volume <= 0)
            { time_volume = 0; }
            
        }
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

        for (int i = 0; i < 128; i++)
        {
            float tempMax = volumeData[i];
            if (maxVolume < tempMax)
            {
                maxVolume = tempMax;
            }
        }
        return maxVolume * 10;
    }

    public void SetTime()
    {
        time_volume = interval_volume;
        micRecord = Microphone.Start(device, true, 999, 44100);
    }

    private void SetGunInterval()
    {
        Weapons m_weapon = GameObject.FindGameObjectWithTag("Weapons").GetComponent<Weapons>();

        if (volume < 0.3)
        {
            m_weapon.ChangeInterval(1);
        }
        else if (volume >= 0.3f && volume <= 0.5f)
        {
            m_weapon.ChangeInterval(0.8f);
        }
        else if (volume > 0.5f && volume <= 0.75f)
        {
            m_weapon.ChangeInterval(0.6f);
        }
        else if (volume > 0.75f && volume <= 1.0f)
        {
            m_weapon.ChangeInterval(0.45f);
        }
        else if (volume > 1.0f && volume <= 1.5f)
        {
            m_weapon.ChangeInterval(0.3f);
        }
        else if (volume > 1.5f && volume <= 3.0f)
        {
            m_weapon.ChangeInterval(0.2f);
        }
        else if (volume > 3.0f && volume <= 5f)
        {
            m_weapon.ChangeInterval(0.1f);
        }
        else if (volume > 5f && volume <= 7f)
        {
            m_weapon.ChangeInterval(0.05f);
        }
        else if (volume > 7f && volume <= 9f)
        {
            m_weapon.ChangeInterval(0.025f);
        }
        else if (volume > 9f)
        {
            m_weapon.ChangeInterval(0.01f);
        }
    }

}
