using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    public GameObject gameManager;
    private GameManager gameManagerScript;

    public float size = 1.0f;
    public float speed = 50.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    private float maxLifeTime = 30.0f;

    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 365.0f);
        this.transform.localScale = Vector3.one * this.size;    // Vector3.one is the same as Vector3(1,1,1)
        
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (this.size * 0.5 >= this.minSize)
            {
                SpawnSmallAsteroids();
            }
            gameManagerScript.AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void SpawnSmallAsteroids() 
    {
        int randomInt = Random.Range(0,4);
        for (int i = 0; i <= randomInt; i++) 
        {
            Vector2 position = this.transform.position;
            position += Random.insideUnitCircle * 0.5f;

            Asteroid smallAsteroid = Instantiate(this, position, this.transform.rotation);
            smallAsteroid.size = this.size * 0.5f;
            smallAsteroid.speed = this.speed * 1.5f;
            smallAsteroid.SetTrajectory(Random.insideUnitCircle.normalized);
        }
        
    }
  
}
