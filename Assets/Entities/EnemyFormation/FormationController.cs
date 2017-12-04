using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;

	private bool movingRight = true;
	private float xmin;
	private float xmax;

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		xmin = leftBoundary.x;
		xmax = rightBoundary.x;

		SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float leftEdgeOfFormation = transform.position.x - 0.5f * width;
		float rightEdgeOfFormation = transform.position.x + 0.5f * width;

		if (leftEdgeOfFormation < xmin || rightEdgeOfFormation > xmax) {
			movingRight = !movingRight;
		}

		if (AllMembersDead()) {
			Debug.Log("Empty Formation");
			SpawnEnemies();
		}
	}

	void SpawnEnemies() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	bool AllMembersDead() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
