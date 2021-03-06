﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsInput : ControlsBase {

	Vector2 _movement;

	#if(UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR

	protected override void Awake(){
		Destroy(this);
	}

	#endif
	void Update(){
		_movement.x = Input.GetAxis("Horizontal");
		_movement.y = Input.GetAxis("Vertical");
		controllable.Move(_movement);
		controllable.Attacking = Input.GetButton("Fire2");
		controllable.Protecting = Input.GetButton("Fire3");
	}


}
