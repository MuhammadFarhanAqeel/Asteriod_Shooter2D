using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControllerTransforms : ShipControllerBase 
{


	float steeringMultiplyer = 5;
	public Vector2 thrustMultiplyer=new Vector2(0.2f,.22f);

	Vector3 thrustDirection;

	protected void Update(){
		thrustDirection = (Vector3)Thrust;
		thrustDirection.Scale(thrustMultiplyer);

		transform.Translate(thrustDirection, Space.Self);
		transform.Rotate(0,0,-Steering*steeringMultiplyer ,Space.Self);
		 
	}


}
