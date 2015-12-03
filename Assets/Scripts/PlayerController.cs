using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public GameObject projectile;
	public AudioClip laserSound;
	public float speed, padding, projectileSpeed, fireRate, health;
	
	float xMin;
	float xMax;
	
	void Start (){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distance));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	}
	
	void Update () {
		MoveWithArrows ();
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, fireRate); 
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
	}
	
	void MoveWithArrows () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
			
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		
		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	
	}
	
	void Fire () {
		GameObject playerBeam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		playerBeam.rigidbody2D.velocity = new Vector3 (0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (laserSound, transform.position);
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		EnemyWeapon enemyMissile = col.GetComponent<EnemyWeapon>();
		if (enemyMissile) {
			health -= enemyMissile.GetDamage ();
			enemyMissile.Hit();
			if (health <= 0) {
				Die();
			}
		}
	}
	
	void Die() {
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		Destroy(gameObject);
		man.LoadLevel ("Win Screen");
	}
}