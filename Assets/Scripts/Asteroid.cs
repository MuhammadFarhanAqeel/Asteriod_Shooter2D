using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {


	public int score = 5;

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag != "Projectile")
			return;
		
		GameManager.Score += score;

		//TODO : Use Object Pooling
		Destroy(gameObject);
	}

}