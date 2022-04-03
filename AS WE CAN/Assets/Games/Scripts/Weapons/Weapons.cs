using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    #region ����
    protected Vector3 mousePos;
    protected Vector2 direction;
    private float fire_time = 0.0f;
    public float fire_interval;
    public float fire_current = 0.5f;
    #endregion

    #region �ӵ�����
    public GameObject bulletPrefab;
    public int atk;    
    public float bulletSpeed;
    #endregion

    #region ���
    private Animation animator;
    #endregion

    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        GunMovement();
        GunAttack();
    }

    protected void GunMovement()
    {
        //�õ��������λ��
        mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        //�������λ�õ�����������
        float flipY = mousePos.x < playerPos.x ? -1 : 1 ;
        transform.localScale = new Vector3(1, flipY, 1);

        //���������ת��
        direction = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        transform.right = direction;

    }

    protected virtual void GunAttack()
    {
        if (fire_time != 0)
        {
            fire_time -= Time.deltaTime;
            if (fire_time <= 0)
                fire_time = 0;
        }

        if (Input.GetMouseButton(0))
        {
            if (fire_time <= 0)
            {
                fire_time = fire_interval;
                Attack();
            }
        }
    }

    protected virtual void Attack()
    {
        //�����ӵ�
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = transform.position;
        //���λ�ã������ӵ�����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Wall")))
        {
            //Debug.Log(hit.collider.gameObject.layer);
            Vector3 dir = (hit.point - transform.position).normalized;
            dir = new Vector3(dir.x, 0, dir.z);
            bullet.GetComponent<Bullets>().ShootBullet(dir, bulletSpeed, atk);
        }
    }

    public void ChangeInterval(float x)
    {
        fire_interval = 0.5f * x;
    }

}
