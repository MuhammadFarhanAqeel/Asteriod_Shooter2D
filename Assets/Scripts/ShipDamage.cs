using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamage : MonoBehaviour {


	public float vulnerability = 1;
	Rigidbody2D _ridigbody2D;
	Collider2D _collider2D;



	void Awake(){
	
		_ridigbody2D = GetComponent<Rigidbody2D>();
		_collider2D = GetComponent<Collider2D>();

	}



	float _damage;
	
	public float Damage
	{
		get{ return GameManager.Damage; }
		set{ GameManager.Damage = value; }
	}

	void OnCollisionEnter2D(Collision2D collision){
		float damage = collision.relativeVelocity.magnitude;
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
		Damage += damage;
	}

}
