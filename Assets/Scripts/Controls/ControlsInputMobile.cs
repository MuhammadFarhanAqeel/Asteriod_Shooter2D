using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


	public class ControlsInputMobile : ControlsBase {

	Vector2 _movement = Vector2.zero;


	void Update(){
		
		_movement.x = CrossPlatformInputManager.GetAxis("Horizontal");
		_movement.y = CrossPlatformInputManager.GetAxis("Vertical");

			controllable.Move(_movement);
//		controllable.Attacking = Input.GetButton("Fire2");
		controllable.Attacking = _fire;

//		controllable.Protecting = Input.GetButton("Fire3");
		controllable.Protecting = _shield;
	}


	bool _fire = false;
	bool _shield = false;

	public void Fire(){
		_fire = !_fire;
	}

	public void Shield(){
		_shield = !_shield;
	}

}
