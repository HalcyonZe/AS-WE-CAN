using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : Singleton<SpeechController>
{
    public void RecognizeResult(string recognize)
    {
        if (recognize.IndexOf("��") != -1)
        {
            Debug.Log("ʶ��ɹ�");
            gameObject.transform.GetChild(0).GetComponent<VolumeGet>().SetTime();
        }
        /*for(int i=0;i<recognize.Length;i++)
        {
            if (recognize[i] == '��')
            {
                Debug.Log("hhh");
            }
        }
        if (recognize == "��?")
        //{
        //    Debug.Log("hhh");
        //}*/
    }
}
