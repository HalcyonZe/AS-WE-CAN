using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float interval;
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    protected Transform muzzlePos;
    protected Transform shellPos;
    protected Vector3 mousePos;
    protected Vector2 direction;
    protected float timer;
    protected float flipY;
    protected Animator animator;

    protected float interval_MouseButton = 0.5f;
    protected float time_MouseButton;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");
        shellPos = transform.Find("BulletShell");
        flipY = transform.localScale.y;
    }

    protected virtual void Update()
    {

        GunMove();

        MouseButtonShoot();
        //Shoot();
    }

    protected void GunMove()
    {
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerPos.x /*transform.position.x*/)
            transform.localScale = new Vector3(flipY, -flipY, 1);
        else
            transform.localScale = new Vector3(flipY, flipY, 1);

        direction = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        //direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;

    }

    protected virtual void Shoot()
    {      
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
        }

        if (Input.GetButton("Fire1"))
        {
            if (timer == 0)
            {
                timer = interval;
                Fire();
            }
        }
    }

    protected virtual void MouseButtonShoot()
    {
        if (time_MouseButton != 0)
        {
            time_MouseButton -= Time.deltaTime;
            if (time_MouseButton <= 0)
                time_MouseButton = 0;
        }

        if (Input.GetMouseButton(0))
        {
            if(time_MouseButton == 0)
            {
                time_MouseButton = interval_MouseButton;
                Fire();
            }
        }
    }

    protected virtual void Fire()
    {
        //animator.SetTrigger("Shoot");

        // GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = transform.position;
        //bullet.transform.position = muzzlePos.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            //Debug.Log(hit.point);
            Vector3 dir = (hit.point - transform.position).normalized;
            dir = new Vector3(dir.x, 0, dir.z);
            bullet.GetComponent<Bullet>().SetSpeed(dir);
        }

        /*float angel = Random.Range(-5f, 5f);
        Vector3 dir = (hit.point - transform.position).normalized;
        dir = new Vector3(dir.x, 0, dir.z);
        bullet.GetComponent<Bullet>().SetSpeed(dir);*/

        // Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
        /*GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;*/
    }

    public void ChangeInterval(float x)
    {
        interval_MouseButton = 0.5f * x;
    }

}
