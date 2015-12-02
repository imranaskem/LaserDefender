using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float health;
	
	void OnTriggerEnter2D (Collider2D col) {
		Projectile missile = col.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0){
				Destroy(gameObject);
			}
		}
	}
}
