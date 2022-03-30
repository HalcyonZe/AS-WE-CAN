using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class StateParameter
{
    #region 属性
    /*[HideInInspector]*/ public float speed;
    /*[HideInInspector]*/ public int playerLife;   
    [HideInInspector] public int MaxLife = 10;   
    [HideInInspector] public Vector3 input;
    [HideInInspector] public Vector3 mousePos;

    [HideInInspector] public float speech_timer = 0;
    [HideInInspector] public float speech_interval = 1.5f;

    #endregion

    #region 组件
    [HideInInspector] public Animator m_animator;
    [HideInInspector] public Rigidbody m_rigidbody;
    [HideInInspector] public int gunNum;
    #endregion

}

public class PlayerSFM : MonoBehaviour
{
    private Dictionary<PlayerState, BaseState> stateDic = new Dictionary<PlayerState, BaseState>();
    private BaseState curState;
    public StateParameter parameter = new StateParameter();

    // Start is called before the first frame update
    void Start()
    {
        //属性
        //parameter.speed = 6.0f;

        //组件
        parameter.m_animator = GetComponent<Animator>();
        parameter.m_rigidbody = GetComponent<Rigidbody>();

        //状态机
        stateDic.Add(PlayerState.Movement, new MovementState(this));
        stateDic.Add(PlayerState.Death, new DeathState(this));
        stateDic.Add(PlayerState.Speech, new SpeechState(this));

        curState = stateDic[PlayerState.Movement];
        curState.OnEnter();
    }

    // Update is called once per frame
    void Update()
    {
        if (curState != null)
        {
            curState.OnUpdate();
        }          
    }

    //切换状态
    public void SwitchState(PlayerState type)
    {
        curState.OnExit();
        curState = stateDic[type];
        curState.OnEnter();
    }

    public void LifeChange(int value)
    {
        parameter.playerLife += value;
        Debug.Log("玩家血量"+ parameter.playerLife);
        if(parameter.playerLife<=0)
        {
            SwitchState(PlayerState.Death);
        }
    }

}
