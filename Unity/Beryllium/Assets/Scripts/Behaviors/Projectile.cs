﻿using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector3 direction { get; set; }
    private const int speed = 2; 

	void Update () {
        transform.position += direction.normalized * speed * Time.deltaTime;
        SceneUtils scene = SceneUtils.instance;
        if (!scene.inBound(transform.position)) {
            Destroy(gameObject);
        }
	}

    public void OnCollisionEnter2D (Collision2D collision) {
        if (collision.collider.tag != "Target") {
            return;
        }
        Destroy(gameObject);
    }
}
