using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float health = 150f;
	public float projectileSpeed = -10f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;

	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update() {
		float probability = shotsPerSecond * Time.deltaTime;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void Fire() {
		Vector3 offset = new Vector3(0, -1.0f, 0);
		Vector3 firePos = transform.position + offset;
		GameObject laser = Instantiate(projectile, firePos, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
}
