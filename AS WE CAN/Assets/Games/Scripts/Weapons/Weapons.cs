using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    #region 属性
    protected Vector3 mousePos;
    protected Vector2 direction;
    private float fire_time = 0.0f;
    public float fire_interval;
    public float fire_current = 0.5f;
    #endregion

    #region 子弹属性
    public GameObject bulletPrefab;
    public int atk;    
    public float bulletSpeed;
    #endregion

    #region 组件
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
        //得到鼠标和玩家位置
        mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        //根据鼠标位置调整武器朝向
        float flipY = mousePos.x < playerPos.x ? -1 : 1 ;
        transform.localScale = new Vector3(1, flipY, 1);

        //武器随鼠标转向
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
        //生成子弹
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = transform.position;
        //检测位置，给予子弹方向
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
