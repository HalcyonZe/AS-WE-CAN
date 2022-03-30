using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    public DeathState(PlayerSFM sfm)
    {
        ctrl = sfm;
        para = ctrl.parameter;
    }

    public override void OnEnter()
    {
        Debug.Log("ƒ„À¿¡À");
        para.m_animator.SetTrigger("Die");
    }

}
