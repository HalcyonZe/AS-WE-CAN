using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject explosionPrefab;
    new private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void SetSpeed(Vector3 direction)
    {
        rigidbody.velocity = direction * speed;
    }

    void Update()
    {

    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;

        // Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }*/
}
