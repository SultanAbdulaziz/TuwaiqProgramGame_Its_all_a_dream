using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed; 
    }
}
