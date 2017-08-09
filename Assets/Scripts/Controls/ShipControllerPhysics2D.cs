using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipControllerPhysics2D : ShipControllerBase {



	Rigidbody2D _rigidBody2D;
	float steeringMultiplyer = 10;
	public Vector2 thrustMultiplyer=new Vector2(10f,10f);

	Vector3 thrustDirection;

	protected override void Awake(){
		base.Awake();
		_rigidBody2D = GetComponent<Rigidbody2D>();

	}

	protected void FixedUpdate(){

		_rigidBody2D.AddTorque(-Steering * steeringMultiplyer,ForceMode2D.Force);

		thrustDirection = (Vector3)Thrust;
		thrustDirection.Scale(thrustMultiplyer);

		_rigidBody2D.AddRelativeForce(thrustDirection,ForceMode2D.Force);

	}
}
