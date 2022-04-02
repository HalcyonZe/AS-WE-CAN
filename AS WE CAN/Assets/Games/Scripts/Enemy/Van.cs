using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Van : Enemys
{
    

    private Vector3 dir = new Vector3(1,0,0);

    protected override void Attack()
    {
        //生成方向
        dir = Quaternion.AngleAxis(30,Vector3.up) * dir;
        dir.Normalize();
        //Debug.Log(dir);

        //生成子弹
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Bullets>().ShootBullet(dir, bulletSpeed, atk);
        
    }

    protected override void EnemyDied()
    {
        base.EnemyDied();
        AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime >= 1 && (info.IsName("Death_Left_Van")|| info.IsName("Death_Right_Van")))
        {
            //
            Player player=FindObjectOfType<Player>();
            player.VanNum++;
            //
            Destroy(gameObject);
        }
    }

}
