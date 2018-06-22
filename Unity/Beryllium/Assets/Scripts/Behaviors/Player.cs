using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    enum ShootingDirection { normal, up, down }
    ShootingDirection shootingDirection;
    const float shootingCoolDownRate = 3f;
    float shootingCoolDown;
    Controls controls;
    int playerSideMultiplier;
    float leftBound;
    float rightBound;

    public void setUp (PlayerIndex playerIndex) {
        shootingDirection = ShootingDirection.up;
        updateShootingDirectionSprite();
        if (playerIndex == PlayerIndex.first) {
            setUpAsFirst();
            controls = new Controls(PlayerIndex.first);
        } else if (playerIndex == PlayerIndex.second) {
            setUpAsSecond();
            controls = new Controls(PlayerIndex.second);
        } else {
            setUpAsSecond();
            controls = new Controls(PlayerIndex.auto);
        }
    }

    public void setUpAsFirst () {
        playerSideMultiplier = 1;
        placePlayer();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = false;

        SceneUtils scene = SceneUtils.instance;
        var width = GetComponent<SpriteRenderer>().bounds.size.x;
        leftBound = 0 + width / 2;
        rightBound = scene.maxX - width / 2;
    }

    public void setUpAsSecond () {
        playerSideMultiplier = -1;
        placePlayer();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;

        SceneUtils scene = SceneUtils.instance;
        var width = GetComponent<SpriteRenderer>().bounds.size.x;
        rightBound = 0 - width / 2;
        leftBound = scene.minX + width / 2;
    }

    public void placePlayer () {
        SceneUtils scene = SceneUtils.instance;
        var height = GetComponent<SpriteRenderer>().bounds.size.y;
        var x = playerSideMultiplier * scene.maxX / 2;// shift 1/4 from side
        var y = scene.minY + height / 2;
        var position = new Vector3(x, y, 0);
        transform.position = position;
    }
	
	void Update () {
        if (
            Global.gameType == GameType.antiAircraft || 
            Global.gameType == GameType.shootingGallary
        ) {
            updateShootingDirection();
        }
        if (
            Global.gameType == GameType.torpedo ||
            Global.gameType == GameType.shootingGallary
        ) {
            updatePosition();
        }
        updateShoot();
	}

    void updateShootingDirection () {
        var oldShootingDirection = shootingDirection;
        if (controls.up) {
            shootingDirection = ShootingDirection.up;
        } else if (controls.down) {
            shootingDirection = ShootingDirection.down;
        } else {
            shootingDirection = ShootingDirection.normal;
        }

        if (oldShootingDirection != shootingDirection) {
            updateShootingDirectionSprite();
        }
    }

    void updateShootingDirectionSprite () {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var sprites = Resources.LoadAll<Sprite>("Sprites/Player");
        spriteRenderer.sprite = sprites[(int)shootingDirection];
    }

    void updateShoot () {
        shootingCoolDown -= Time.deltaTime;
        if (shootingCoolDown <= 0 && controls.fire) {
            shootingCoolDown = shootingCoolDownRate;
            shoot();
        }
    }

    void shoot () {
        //TODO: need some refactor
        var projectilePrefab = Resources.Load("Prefabs/Projectile");
        Vector3 position = transform.position;
        switch (shootingDirection) {
            case ShootingDirection.normal:
                position.y += 0.2f;
                break;
            case ShootingDirection.up:
                position.x += 0.1f * playerSideMultiplier;
                position.y += 0.2f;
                break;
            case ShootingDirection.down:
                position.x -= 0.2f * playerSideMultiplier;
                position.y += 0.15f;
                break;
        }

        var projectileObj = (GameObject)Instantiate(projectilePrefab, position, Quaternion.identity);
        var projectile = projectileObj.GetComponent<Projectile>();
        switch (shootingDirection) {
            case ShootingDirection.normal:
                projectile.direction = new Vector3(-0.5f * playerSideMultiplier, 1f, 0f);
                break;
            case ShootingDirection.up:
                projectile.direction = new Vector3(0, 1, 0);
                break;
            case ShootingDirection.down:
                projectile.direction = new Vector3(-1f * playerSideMultiplier, 0.5f, 0f);
                break;
        }
    }

    void updatePosition () {
        const float speed = 3;
        var newPosition = transform.position;
        if (controls.left) {
            newPosition += Vector3.left * speed * Time.deltaTime;
        } else if (controls.right) {
            newPosition += Vector3.right * speed * Time.deltaTime;
        } else {
            return;
        }

        if(newPosition.x < leftBound) {
            transform.position = new Vector3(leftBound, newPosition.y, 0);
        } else if (newPosition.x > rightBound) {
            transform.position = new Vector3(rightBound, newPosition.y, 0);
        } else {
            transform.position = newPosition;
        }
    }
}
