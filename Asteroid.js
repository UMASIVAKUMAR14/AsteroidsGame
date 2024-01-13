#pragma strict

public var sprites : Sprite[];
private var _spriteRenderer : SpriteRenderer;
private var _rigidbody : Rigidbody2D;
public var gameManager : GameObject;
private var gameManagerScript : GameManager;

public var size : float = 1.0;
public var speed : float = 50.0;
public var minSize : float = 0.5;
public var maxSize : float = 1.5;
private var maxLifeTime : float = 30.0;

function Awake() {
    _spriteRenderer = GetComponent.<SpriteRenderer>();
    _rigidbody = GetComponent.<Rigidbody2D>();
    gameManager = GameObject.FindGameObjectWithTag("GameManager");
    gameManagerScript = gameManager.GetComponent(GameManager);
}

function Start() {
    _spriteRenderer.sprite = sprites[Random.Range(0, sprites.length)];

    transform.eulerAngles = Vector3(0.0, 0.0, Random.value * 365.0);
    transform.localScale = Vector3.one * size;
}

function SetTrajectory(direction : Vector2) {
    _rigidbody.AddForce(direction * speed);
    Destroy(gameObject, maxLifeTime);
}

function OnCollisionEnter2D(collision : Collision2D) {
    if (collision.gameObject.tag == "Bullet") {
        if (size * 0.5 >= minSize) {
            SpawnSmallAsteroids();
        }
        gameManagerScript.AsteroidDestroyed(this);
        Destroy(gameObject);
    }
}

function SpawnSmallAsteroids() {
    var randomInt : int = Random.Range(0, 4);
    for (var i : int = 0; i <= randomInt; i++) {
        var position : Vector2 = transform.position;
        position += Random.insideUnitCircle * 0.5;

        var smallAsteroid : Asteroid = Instantiate(this, position, transform.rotation);
        smallAsteroid.size = size * 0.5;
        smallAsteroid.speed = speed * 1.5;
        smallAsteroid.SetTrajectory(Random.insideUnitCircle.normalized);
    }
}
