using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    Vector3 direction;
    MovementDirection _movementDirection;
    float speed;

    public MovementDirection movementDirection {
        set {
            _movementDirection = value;
            if (value == MovementDirection.left) {
                direction = new Vector3(-1, 0, 0);
            } else {
                direction = new Vector3(1, 0, 0);
            }
        }
        get {
            return _movementDirection;
        }
    }

	// Update is called once per frame
	void Update () {
        transform.position += direction * speed * Time.deltaTime;
        SceneUtils scene = SceneUtils.instance;
        if (_movementDirection == MovementDirection.left) {
            if (transform.position.x + 1 < scene.minX) {
                transform.position = new Vector3(scene.maxX + 1, transform.position.y, 0);
            }
        } else {
            if (transform.position.x - 1 > scene.maxX) {
                transform.position = new Vector3(scene.minX - 1, transform.position.y, 0);
            }
        }
	}

    public void init (TargetOptions options, MovementDirection direction, int path) {
        const float widthShift = 0.5f;
        var spriteRenderer = GetComponent<SpriteRenderer>();

        speed = options.speed;

        var sprites = Resources.LoadAll<Sprite>(options.path);
        spriteRenderer.sprite = sprites[options.spriteNumber];
        if (direction == MovementDirection.left) {
            spriteRenderer.flipX = true;
        }

        var boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = spriteRenderer.size;

        SceneUtils scene = SceneUtils.instance;
        const float startPath = -1;
        var y = startPath + path;
        var x = 0f;
        if (direction == MovementDirection.left) {
            x = scene.maxX + widthShift;
        } else {
            x = scene.minX - widthShift;
        }
        transform.position = new Vector3(x, y, 0);

        movementDirection = direction;
    }

	public void OnCollisionEnter2D (Collision2D collision) {
        var projectilePrefab = Resources.Load("Prefabs/Death");
        var projectileObj = (GameObject)Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );
        Destroy(gameObject);
	}
}
