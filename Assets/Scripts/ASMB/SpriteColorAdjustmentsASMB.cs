using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorAdjustmentsASMB : StateMachineBehaviour {


	[SerializeField]	Gradient _color;
	[SerializeField]	AnimationCurve _Luminosity;
	[SerializeField]	AnimationCurve _Saturation;

	List<SpriteRenderer> _spriteRendrer;

	List<Color> _colors;
	List<float> _satValues;
	List<float> _lumValues;


	bool byPass;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {



		_spriteRendrer = new List<SpriteRenderer>(animator.GetComponentsInChildren<SpriteRenderer>());

		if (_spriteRendrer.Count == 0)
		{
			byPass = true;
			return;
		}
		_colors = new List<Color>(_spriteRendrer.Count);
		_satValues = new List<float>(_spriteRendrer.Count);
		_lumValues= new List<float>(_spriteRendrer.Count);


		for (int i = 0 ; i < _spriteRendrer.Count;i++)
		{
			_colors.Add(_spriteRendrer[i].material.HasProperty("_Color2") ? _spriteRendrer[i].material.GetColor("_Color2") : Color.white);
			_satValues.Add(_spriteRendrer[i].material.HasProperty("_Saturation") ? _spriteRendrer[i].material.GetFloat("_Saturation") : 1f);
			_lumValues.Add(_spriteRendrer[i].material.HasProperty("_Saturation") ? _spriteRendrer[i].material.GetFloat("_Saturation") : 1f);

		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (byPass)
			return;
		float t = stateInfo.normalizedTime - Mathf.Floor(stateInfo.normalizedTime);
		Color c = _color.Evaluate(t);
		float s = _Saturation.Evaluate(t);
		float l = _Luminosity.Evaluate(t);




		for (int i = 0; i < _spriteRendrer.Count; i++)
		{
			_spriteRendrer[i].material.SetColor("_Color2",c);
			_spriteRendrer[i].material.SetFloat("_Saturation",s);
			_spriteRendrer[i].material.SetFloat("_Luminosity",l);

		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (byPass)
			return;
		
		for (int i = 0; i < _spriteRendrer.Count; i++)
		{
			_spriteRendrer[i].material.SetColor("_Color2", _colors[i]);
			_spriteRendrer[i].material.SetFloat("_Saturation", _satValues[i]);
			_spriteRendrer[i].material.SetFloat("_Luminosity", _lumValues[i]);

		}
	}
}
