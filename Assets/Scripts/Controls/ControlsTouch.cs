using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsTouch : ControlsBase 
{

	bool _released = true;
	Vector2 _lastPositionDelta = Vector2.zero;

	#if(UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR

	protected override void Awake(){
		base.Awake();
		enabled = true;
	}
	#endif
	void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch1 = Input.touches[0];
			switch (touch1.phase)
			{
				case TouchPhase.Began:
					_released = false;
					break;
				case TouchPhase.Moved:
					_lastPositionDelta = touch1.deltaPosition;
					controllable.Move(_lastPositionDelta);
	
					break;
				case TouchPhase.Ended:
					controllable.Move(Vector2.zero);
					_released = true;
					break;
				default:
					break;
			}

			controllable.Attacking = (touch1.tapCount == 2);

		}

		controllable.Protecting = (Input.touchCount == 2);

		if (_released && _lastPositionDelta.magnitude > 0)
		{
			_lastPositionDelta = Vector2.Lerp(_lastPositionDelta, Vector2.zero, Time.deltaTime * 5);
			if (_lastPositionDelta.magnitude < 0.05f)
				_lastPositionDelta = Vector2.zero;
			controllable.Move(_lastPositionDelta);
		}
			
	}
}