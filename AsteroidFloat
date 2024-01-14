using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFloat : MonoBehaviour
{
    // Start is called before the first frame update
    public float hovspeed = 2.0f;
    private Rigidbody2D rigidBody2D;
    public Vector2 direction = new Vector2(2, 2);
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody2D.velocity = hovspeed * direction;
        //Debug.LogError("In Update:  " + direction);
    }

    void onBecameInvisible()
    {
        Vector2 direction = new Vector2(Random.Range(0,3), Random.Range(0,3));
        //Debug.LogError("If Invisible  " + direction);
    }
}
