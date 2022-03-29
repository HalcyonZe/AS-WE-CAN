using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    enum EnemyState
    {
        idle, movement, death
    }

    #region 属性
    public int atk;
    public float bulletSpeed;
    private float fire_time = 0.0f;
    public float fire_interval;

    [SerializeField] private EnemyState curState;

    public float move_interval;
    private float move_time = 2.0f;
    public float idle_interval;
    private float idle_time = 2.0f;
    public Vector3 moveDir = new Vector3(1,0,0);
    public float moveSpeed;
    #endregion

    #region 组件
    public GameObject bulletPrefab;
    [HideInInspector] public Animator m_animator;
    [HideInInspector] public Rigidbody m_rigidbody;
    #endregion

    private void Start()
    {
        //组件
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();

        //状态机
        curState = EnemyState.idle;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch(curState)
        {
            case EnemyState.idle:
                EnemyIdle();
                EnemyAttack();
                break;
            case EnemyState.movement:
                EnemyMovement();
                EnemyAttack();
                break;
            case EnemyState.death:
                break;
        }
        
    }

    private void EnemyIdle()
    {
        m_animator.SetBool("IsWalk", false);
        m_rigidbody.velocity = Vector3.zero;
        if (idle_time > 0)
        {
            idle_time -= Time.deltaTime;
        }
        else
        {
            idle_time = idle_interval;
            curState = EnemyState.movement;
        }
    }

    protected virtual void EnemyMovement()
    {
        m_animator.SetBool("IsWalk", true);
        if (move_time > 0)
        {
            move_time -= Time.deltaTime;
        }
        else
        {
            move_time = move_interval;
            moveDir *= -1;
            curState = EnemyState.idle;
        }

        bool isRight = moveDir.x > 0 ? true : false;
        m_animator.SetBool("IsRight", isRight);

        m_rigidbody.velocity = moveDir.normalized * moveSpeed;
    }

    protected virtual void EnemyAttack()
    {
        if (fire_time > 0)
        {
            fire_time -= Time.deltaTime;
        }
        else
        {
            fire_time = fire_interval;
            Attack();
        }


    }

    protected virtual void Attack()
    {

        
    }

}
