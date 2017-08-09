using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {


	public int score = 5;
	public GameObject explosion;

	int _explosionID;



	void Awake(){
		_explosionID = explosion.GetInstanceID();
		ObjectPool.InitPool(explosion);
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag != "Projectile")
			return;
		
		GameManager.Score += score;

		ObjectPool.GetInstance(_explosionID,collision.contacts[0].point);

		//Destroy(gameObject);
		ObjectPool.Release(gameObject);
	}

}