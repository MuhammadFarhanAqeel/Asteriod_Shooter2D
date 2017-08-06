using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public GameObject projectile;

	public Transform[] emitters;
	int _current;
	public float firingRange = 5;
	Collider2D _shipCollider2D;
	[Range(0.01f,100f)] public float firingRate = 1f;




	void Awake(){
		_shipCollider2D = transform.parent.GetComponent<Collider2D>();
	}


	void Start(){
	}




	void Fire(){

		_current = (_current >= emitters.Length - 1) ? 0 : _current + 1; 

		Vector3 position = emitters[_current].TransformPoint(Vector3.up * 0.5f);
		GameObject projetileInstance = (GameObject)Instantiate(projectile, emitters[_current].position, emitters[_current].rotation);

		projetileInstance.GetComponent<Projectile>().range = firingRange;
		Physics2D.IgnoreCollision(_shipCollider2D, projetileInstance.GetComponent<Collider2D>());
	}



}
