using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {

	public float damage;
	
	public void Hit () {
		Destroy (gameObject);
	}
	
	public float GetDamage () {
		return damage;
	}
}
