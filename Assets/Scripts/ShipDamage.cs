using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamage : MonoBehaviour {


	public float vulnerability = 1;
	Rigidbody2D _ridigbody2D;
	Collider2D _collider2D;
	public GameObject impact;
	int _impactID;

	Renderer[] _renderers;
	Collider2D[] _colliders;


	public Transform[] explosionLocations;
	public GameObject explosion;
	int _explosionID;



	void Awake(){
		_ridigbody2D = GetComponent<Rigidbody2D>();
		_collider2D = GetComponent<Collider2D>();
		_impactID = impact.GetInstanceID();
		ObjectPool.InitPool(impact);
	
		ObjectPool.InitPool(explosion);
		_explosionID = explosion.GetInstanceID();
	
	}





	float _damage;
	
	public float Damage
	{
		get{ return GameManager.Damage; }
		set{ GameManager.Damage = value; }
	}

	void OnCollisionEnter2D(Collision2D collision){
		float damage = collision.relativeVelocity.magnitude * vulnerability	;
		//if (collision.gameObject.tag == "Asteroid")
		if (collision.collider.sharedMaterial)
		{
			damage *= (_collider2D.sharedMaterial.friction) *
			collision.collider.sharedMaterial.friction *
			(1 / collision.collider.sharedMaterial.bounciness)*
			(1 / _collider2D.sharedMaterial.bounciness);

		}
		if (collision.rigidbody)
		{
			damage *= (collision.rigidbody.mass / _ridigbody2D.mass);
		}
		if (damage >= .1f)
		{
			ObjectPool.GetInstance(_impactID, collision.contacts[0].point);
		}
		Damage += damage;
	}


	public void Die(){
		StartCoroutine(Respawn());
	}



	IEnumerator RunExplosions(){

		foreach (Transform t in explosionLocations)
		{
			ObjectPool.GetInstance(_explosionID, t.position, t.rotation);
			yield return new WaitForSeconds(.2f);
		}
	}

	private IEnumerator Respawn(){
	

		_renderers = GetComponentsInChildren<Renderer>(false);
		_colliders = GetComponentsInChildren<Collider2D>(false);

		// DIsable all colliders
		foreach (Collider2D c in _colliders)
			c.enabled = false;

		// Disable all Renderers
		foreach (Renderer r in _renderers)
			r.enabled = false;
	
		// Disable COntrols
		GameManager.Controls = false;

		// Run explosions
		yield return StartCoroutine(RunExplosions());

		// Reset Ship
		GetComponent<ShipControllerBase>().ResetShip();

		// wait for 1 sec
		yield return new WaitForSeconds(1);


		// Enable all Renderers
		foreach (Renderer r in _renderers)
			r.enabled = true;

		// Enable Controls
		GameManager.Controls = true;

		// Set Animator "Respawn" bool parameter to true
		GetComponent<Animator>().SetBool("Respawn",true);

		// wait for 3 sec
		yield return new WaitForSeconds(3.0f);

		// enable all colliders
		foreach (Collider2D c in _colliders)
			c.enabled = true;
		// set Animator "Respawn" bool parameter to false
		GetComponent<Animator>().SetBool("Respawn",false);



	}

}
