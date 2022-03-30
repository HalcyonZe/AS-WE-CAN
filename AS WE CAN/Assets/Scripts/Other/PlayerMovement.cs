using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerMovement : MonoBehaviour
    {
        public GameObject[] guns;
        public float speed;
        private Vector3 input;
        private Vector3 mousePos;
        private Animator animator;
        private Rigidbody rigidbody;
        private int gunNum;

        public int maxLife = 10;
        public int life;
        //survival part

        public int water = 10;
        public int food = 10;
        public float hungerInterval=10;
        public float hungerClock;
        public float thirstyInterval = 10;
        public float thirstyClock;



        void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
            guns[0].SetActive(true);

            life = maxLife;
            //survival part
            hungerClock = hungerInterval;
            thirstyClock = thirstyInterval;
        }

        void Update()
        {
            
            input.x = Input.GetAxisRaw("Horizontal");
            input.z = Input.GetAxisRaw("Vertical");

            rigidbody.velocity = input.normalized * speed;
            //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = Input.mousePosition;
            Vector3 playerPos= Camera.main.WorldToScreenPoint(transform.position);

            if (mousePos.x > playerPos.x /*transform.position.x*/)
            {
                //***ToDo*** 把转向改rotation改为切换动画状态机的状态播放另一个方向的动画
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                //***ToDo*** 把转向改rotation改为切换动画状态机的状态播放另一个方向的动画
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            if (input != Vector3.zero)
                animator.SetBool("isMoving", true);
            else
                animator.SetBool("isMoving", false);

            //survival part
            survival();

            //Dead
            Dead();

            //当前生命值增益限制
            LifeLimit();
        }
    /*
        void SwitchGun()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                guns[gunNum].SetActive(false);
                if (--gunNum < 0)
                {
                    gunNum = guns.Length - 1;
                }
                guns[gunNum].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                guns[gunNum].SetActive(false);
                if (++gunNum > guns.Length - 1)
                {
                    gunNum = 0;
                }
                guns[gunNum].SetActive(true);
            }
        }
    */

        void LifeLimit()
    {
        if (life >= maxLife)
        {
            life = maxLife;
        }
    }
        void survival()
        {
            //Hunger
            Hunger();

            //Thirsty
            Thisty();
        }

        void Hunger()
        {
            hungerClock -= Time.deltaTime;
            if (hungerClock <= 0)
            {
                hungerClock =hungerInterval; 
                food--;
                //Debug.Log("Food:"+food);
                if(food<=0)
                {
                    food = 0;
                    life--;
                    Debug.Log("你饿坏了！生命减一！" + "生命：" + life);
                }
               
            }
        }

        void Thisty()
        {
            thirstyClock -= Time.deltaTime;
            if (thirstyClock <= 0)
            {
                thirstyClock = thirstyInterval;
                water--;
           
                //Debug.Log("Water:"+water);
                if(water<=0)
                {
                    water = 0;
                    life--;
                    Debug.Log("你太渴了！生命减一！" + "生命：" + life);
                }
               
            }
        }

        void Dead()
        {
            if(life<=0)
            {
                Debug.Log("死了啦，都你害的啦！");
            }
        }
        
    }

