using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechState : BaseState
{
    //private float speech_timer = 0;
    //private float speech_interval = 1.5f;    

    public SpeechState(PlayerSFM sfm)
    {
        ctrl = sfm;
        para = ctrl.parameter;
    }

    public override void OnEnter()
    {
        Debug.Log("¿ªÊ¼Â¼Òô");
        para.speech_timer = para.speech_interval;
        SpeechRe.Instance.StartRecord();

    }

    public override void OnUpdate()
    {
        if (para.speech_timer != 0)
        {
            para.speech_timer -= Time.deltaTime;
            if (para.speech_timer < 0)
            {
                Debug.Log("½áÊøÂ¼Òô");
                para.speech_timer = 0;
                SpeechRe.Instance.EndRecord();
                ctrl.SwitchState(PlayerState.Movement);
            }

        }
    }

}
