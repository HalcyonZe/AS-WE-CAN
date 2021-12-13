using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell
{
    public class PlayerMovement : MonoBehaviour
    {
        public GameObject[] guns;
        public float speed;
        private Vector3 input;
        private Vector3 mousePos;
        private Animator animator;
        private Rigidbody rigidbody;
        private int gunNum;
        void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
            guns[0].SetActive(true);
        }

        void Update()
        {
            SwitchGun();
            input.x = Input.GetAxisRaw("Horizontal");
            input.z = Input.GetAxisRaw("Vertical");

            rigidbody.velocity = input.normalized * speed;
            //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = Input.mousePosition;
            Vector3 playerPos= Camera.main.WorldToScreenPoint(transform.position);

            if (mousePos.x > playerPos.x /*transform.position.x*/)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            if (input != Vector3.zero)
                animator.SetBool("isMoving", true);
            else
                animator.SetBool("isMoving", false);
        }

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
    }
}