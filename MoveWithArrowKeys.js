#pragma strict

public var speed : float = 10.0;
public var rotationSpeed : float = 90.0;
private var rigidBody2D : Rigidbody2D;
public var gameManager : GameObject;
private var gameManagerScript : GameManager;
@SerializeField private var bulletPrefab : Bullet;
@SerializeField private var shooting : AudioSource;
@SerializeField private var death : AudioSource;

function Start() {
    rigidBody2D = GetComponent.<Rigidbody2D>();
    gameManagerScript = gameManager.GetComponent(GameManager);
}

function Update() {
    var rotation : float = transform.rotation.eulerAngles.z;
    
    if (Input.GetKey(KeyCode.LeftArrow)) {
        rotation += rotationSpeed * Time.deltaTime;
    } else if (Input.GetKey(KeyCode.RightArrow)) {
        rotation -= rotationSpeed * Time.deltaTime;
    }

    if (Input.GetKey(KeyCode.UpArrow)) {
        var direction : Vector2 = new Vector2(-Mathf.Sin(rotation * Mathf.Deg2Rad), Mathf.Cos(rotation * Mathf.Deg2Rad));
        rigidBody2D.velocity = direction * speed;
    } else {
        rigidBody2D.velocity = Vector2.zero;
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
        shooting.Play();
        Shoot();
    }

    transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
}

function Shoot() {
    var bullet : Bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
    bullet.Project(transform.up);
}

function OnCollisionEnter2D(collision : Collision2D) {
    if (collision.gameObject.tag == "Asteroid") {
        death.Play();
        rigidBody2D.velocity = Vector3.zero;
        rigidBody2D.angularVelocity = 0.0;
        gameObject.SetActive(false);
        gameManagerScript.PlayerDied();
    }
}
