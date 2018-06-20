using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private enum ShootingDirection { normal, up, down}
    private ShootingDirection shootingDirection;
    const float shootingCoolDownRate = 2;
    private float shootingCoolDown = 0;

	// Use this for initialization
	void Start () {
        SceneUtils scene = SceneUtils.instance;
        var height = GetComponent<SpriteRenderer>().bounds.size.y;
        var x = scene.maxX / 2;// shift 1/4 from side
        var y = scene.minY + height / 2;
        var position = new Vector3(x, y, 0);
        transform.position = position;
	}
	
	// Update is called once per frame
	void Update () {
        updateShootingDirection();
        updateShoot();
	}

    void updateShootingDirection () {
        var oldShootingDirection = shootingDirection;
        if (Input.GetKey("up")) {
            shootingDirection = ShootingDirection.up;
        } else if (Input.GetKey("down")) {
            shootingDirection = ShootingDirection.down;
        } else {
            shootingDirection = ShootingDirection.normal;
        }

        if (oldShootingDirection != shootingDirection) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var sprites = Resources.LoadAll<Sprite>("Sprites/Player");
            spriteRenderer.sprite = sprites[(int)shootingDirection];
        }
    }

    void updateShoot () {
        shootingCoolDown -= Time.deltaTime;
        if (shootingCoolDown <= 0 && Input.GetKey("space")) {
            shootingCoolDown = shootingCoolDownRate;
            shoot();
        }
    }

    void shoot () {
        //TODO: need some refactor
        Object projectilePrefab = Resources.Load("Prefabs/Projectile");
        Vector3 position = transform.position;


        if (shootingDirection == ShootingDirection.normal) {
            position.y += 0.2f;
        } else if (shootingDirection == ShootingDirection.up) {
            position.x += 0.1f;
            position.y += 0.2f;
        } else if (shootingDirection == ShootingDirection.down) {
            position.x -= 0.2f;
            position.y += 0.15f;
        }

        GameObject projectileObj = (GameObject)Instantiate(projectilePrefab, position, Quaternion.identity);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        if (shootingDirection == ShootingDirection.normal) {
            projectile.direction = new Vector3(-0.5f, 1f, 0f);
        } else if (shootingDirection == ShootingDirection.up) {
            projectile.direction = new Vector3(0, 1, 0);
        } else if (shootingDirection == ShootingDirection.down) {
            projectile.direction = new Vector3(-1f, 0.5f, 0f);
        }


    }
}
