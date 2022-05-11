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
        else if(recognize.IndexOf("幻想乡")!= -1)
        {
            Debug.Log("你无敌了");
            PlayerSFM.Instance.parameter.beInvincible = true;
            PlayerSFM.Instance.parameter.invincible_time = 6.0f;
            PlayerSFM.Instance.parameter.m_animator.SetBool("IsRolling", true);
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
