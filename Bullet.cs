using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    private float speed = 500.0f;
    private float maxLifetime = 9.0f;

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, maxLifetime);
    }

    public void OnCollisionEnter2D (Collision2D collision) 
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(this.gameObject);
        }  
    }

}
