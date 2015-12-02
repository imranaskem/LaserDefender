using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width;
	public float height;
	public float enemySpeed;
	
	private bool movingRight = true;
	private float xMin, xMax;
	
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distance));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distance));
		xMax = rightEdge.x;
		xMin = leftEdge.x;
		
		foreach(Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	public void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, 0f));
	}
	
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * enemySpeed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * enemySpeed * Time.deltaTime;
		}
		
		float rightEdgeFormation = transform.position.x + (0.5f * width);
		float leftEdgeFormation = transform.position.x - (0.5f * width);
		if (leftEdgeFormation < xMin) {
			movingRight = true;
		} else if (rightEdgeFormation > xMax) {
			movingRight = false;
		}
		
	}
}
