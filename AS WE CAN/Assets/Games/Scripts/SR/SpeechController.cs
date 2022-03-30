using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : Singleton<SpeechController>
{
    public void RecognizeResult(string recognize)
    {
        if (recognize.IndexOf("啊") != -1)
        {
            Debug.Log("识别成功");
            gameObject.transform.GetChild(0).GetComponent<VolumeGet>().SetTime();
        }
        /*for(int i=0;i<recognize.Length;i++)
        {
            if (recognize[i] == '啊')
            {
                Debug.Log("hhh");
            }
        }
        if (recognize == "啊?")
        //{
        //    Debug.Log("hhh");
        //}*/
    }
}
