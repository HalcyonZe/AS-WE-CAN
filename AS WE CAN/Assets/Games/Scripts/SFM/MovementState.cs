using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : BaseState
{
    public MovementState(PlayerSFM sfm)
    {
        ctrl = sfm;
        para = ctrl.parameter;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        para.mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(ctrl.transform.position);
        bool isRight = para.mousePos.x > playerPos.x ? true : false;
        para.m_animator.SetBool("IsRight", isRight);     

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        para.input.Set(horizontal, 0.0f, vertical);
        para.input.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        para.m_animator.SetBool("IsWalking", isWalking);

        if (Input.GetKey(KeyCode.LeftShift) && isWalking)
        {
            para.m_animator.SetBool("IsRolling", true);
        }
        else
        {
            para.m_animator.SetBool("IsRolling", false);
        }

        para.m_rigidbody.velocity = para.input.normalized * para.speed;

        //控制语音输入
        SpeechStateSwitch();

    }

    public override void OnExit()
    {
        base.OnExit();
        para.input = Vector3.zero;
    }

    private void SpeechStateSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Z) && para.speech_timer == 0)
        {

            Debug.Log("开始");
            //para.speech_timer = para.speech_interval;

            ctrl.SwitchState(PlayerState.Speech);

        }
    }

}
