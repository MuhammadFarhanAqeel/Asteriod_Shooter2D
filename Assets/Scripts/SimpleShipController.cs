//#define REMOTE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShipController : MonoBehaviour {



	Vector2 delta = Vector2.zero;

	Rigidbody2D _rigidBody2D;
	Vector2 _force = Vector2.zero;
	float _torque;
	public float thrustMultiplyer;
	public float SteerMultiplyer;

	 Weapon _weapon;


	bool _firing;
	public bool Firing
	{
		get
		{ return _firing; }
		set
		{ 
			if (_firing != value)
			{
				_firing = value;

				if (_firing)
					_weapon.InvokeRepeating("Fire", 0.1f, 1/_weapon.firingRate);
				else
					_weapon.CancelInvoke();
			}
		}
	}


	void Awake(){
		_rigidBody2D = GetComponent<Rigidbody2D>();
		_weapon = GetComponentInChildren<Weapon>();
	}



	void FixedUpdate () {

		#if(UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR || REMOTE

		if(Input.touchCount >0){
			Touch t = Input.touches[0];

			if(t.phase == TouchPhase.Moved){
				delta.x = t.deltaPosition.x;
				delta.y = t.deltaPosition.y;
			}
			if(t.tapCount > 1){
				Firing = true;
			}
		}else{
			Firing = false;	
		}


		#else

		delta.x = Input.GetAxis("Horizontal");
		delta.y = Input.GetAxis("Vertical");

		Firing = Input.GetButton("Fire2");


		#endif
	


		_force.y = delta.y * thrustMultiplyer;
		_torque = -delta.x * SteerMultiplyer;



		_rigidBody2D.AddRelativeForce(_force);
		_rigidBody2D.AddTorque(_torque);


		//	transform.Translate(0, delta.y, 0) ;
	
		//	transform.Rotate(0,0,-delta.x) ;
		//transformComponent.Translate(0.01f, 0, 00);	
	}
}
