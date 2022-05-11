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
        else if(recognize.IndexOf("������")!= -1)
        {
            Debug.Log("���޵���");
            PlayerSFM.Instance.parameter.beInvincible = true;
            PlayerSFM.Instance.parameter.invincible_time = 6.0f;
            PlayerSFM.Instance.parameter.m_animator.SetBool("IsRolling", true);
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
