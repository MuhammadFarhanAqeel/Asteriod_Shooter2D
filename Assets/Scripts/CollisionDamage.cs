using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour {


	public float damageAmount = 1;





	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag != "Player")
		return;


		collision.gameObject.SendMessage("ApplyDamage",damageAmount,SendMessageOptions.DontRequireReceiver);
	}


}
