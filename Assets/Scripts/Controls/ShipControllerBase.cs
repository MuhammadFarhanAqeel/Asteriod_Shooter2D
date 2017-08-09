using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class ShipControllerBase : MonoBehaviour,IControllable {



	public AnimationCurve steeringCurve;
	public AnimationCurve glideCurve;

	float rearPowerLimit = 0.2f;
	protected Animator _animator;
	protected int _steeringHashID;
	protected int _thrustXHashID;
	protected int _thrustYHashID;
	protected int _shieldHashID;

	Weapon _weapon;

	bool _firing;
	protected bool Firing
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

	Vector2 _thrust;

	protected Vector2 Thrust
	{
		get{ return _thrust; }
		set
		{ 
			if (_thrust != value)
			{
				_thrust = value;
				_animator.SetFloat(_thrustXHashID,_thrust.x);
				_animator.SetFloat(_thrustYHashID,_thrust.y);
			}
		}
	}

	float _steering;

	protected float Steering
	{
		get{ return _steering; }
		set
		{ 
			if (_steering != value)
			{
				_steering = value;
				_animator.SetFloat(_steeringHashID,_steering);
			}
		}
	}

	Vector2 _thrust_Tmp;


	public bool Attacking
	{
		get{ return Firing; }
		set{ Firing = value; }
	}
	bool _shield;

	public bool Shield
	{
		get{ return _shield; }
		set
		{ 
			if (_shield != value)
			{
				_shield = value;
				_animator.SetLayerWeight(_shieldHashID, _shield ? 1 : 0f);
			}
			if (_shield)
				Firing = false;
			
		}
	}

	public bool Protecting
	{
		get{ return Shield; }
		set{ Shield = value; }
	}


	protected virtual void Awake(){
		_animator = GetComponent<Animator>();
		_steeringHashID = Animator.StringToHash("Steering");
		_thrustXHashID = Animator.StringToHash("ThrustX");
		_thrustYHashID = Animator.StringToHash("ThrustY");
		_shieldHashID = _animator.GetLayerIndex("Shield");
		_weapon = GetComponentInChildren<Weapon>();

	}


	public virtual void Move(Vector2 movement)
	{
	
		movement = Vector2.ClampMagnitude(movement,1.0f);
		movement.y = Mathf.Clamp(movement.y,-rearPowerLimit,1);

		Steering = steeringCurve.Evaluate(movement.y) * movement.x;


		_thrust_Tmp.x = glideCurve.Evaluate(movement.y) * movement.x;
		_thrust_Tmp.y = movement.y;

		Thrust = _thrust_Tmp;

		enabled = movement != Vector2.zero;
	}


}
