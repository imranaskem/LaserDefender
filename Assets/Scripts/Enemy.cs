using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float health, enemyProjectileSpeed, shotsPerSecond;
	public GameObject enemyProjectile;
	public AudioClip laserSound, deathSound;
	public int scoreValue = 150;
	
	private ScoreKeeper scoreKeeper;
	
	void Start () {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		
	}
	void OnTriggerEnter2D (Collider2D col) {
		Projectile missile = col.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0){
				AudioSource.PlayClipAtPoint (deathSound,transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
	
	void EnemyFire () {
		GameObject enemyBeam = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
		enemyBeam.rigidbody2D.velocity = new Vector3 (0, -enemyProjectileSpeed, 0);
		AudioSource.PlayClipAtPoint (laserSound, transform.position);
	}
	
	void Update() {
		float probablity = Time.deltaTime * shotsPerSecond;
		if(Random.value < probablity) {
		EnemyFire();
		}
	}
	
}
