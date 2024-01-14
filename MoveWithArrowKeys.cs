using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithArrowKeys : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 10.0f;
    public float rotationSpeed = 90.0f;
    private Rigidbody2D rigidBody2D;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] AudioSource shooting;
    [SerializeField] AudioSource death;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame

    void Update()
    {
        float rotation = transform.rotation.eulerAngles.z;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation += rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation -= rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector2 direction = new Vector2(-Mathf.Sin(rotation * Mathf.Deg2Rad), Mathf.Cos(rotation * Mathf.Deg2Rad));
            rigidBody2D.velocity = direction * speed;
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting.Play();
            Shoot();
        }

        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
    }

    private void Shoot() 
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            death.Play();
            rigidBody2D.velocity = Vector3.zero;
            rigidBody2D.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);
            gameManagerScript.PlayerDied();

        }
    }


}
