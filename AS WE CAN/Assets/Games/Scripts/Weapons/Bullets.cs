using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    private int atk;
    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    //�ӵ���ʼ��
    public void ShootBullet(Vector3 dir, float speed, int atk)
    {
        this.rigidbody.velocity = dir * speed;
        this.atk = atk;
    }


}
