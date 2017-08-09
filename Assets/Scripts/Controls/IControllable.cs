using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable 
{
	bool Attacking{ get; set;}
	bool Protecting { get; set;}
	void Move(Vector2 Movement);

}
