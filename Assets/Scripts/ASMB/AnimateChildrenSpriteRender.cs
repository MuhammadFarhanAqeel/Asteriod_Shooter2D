using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateChildrenSpriteRender : StateMachineBehaviour {




	List<SpriteRenderer> childrenSpriteRendrer;
	List<Color> childrenColor;
	SpriteRenderer thisSpriteRenderer;


	bool byPass;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		thisSpriteRenderer = animator.GetComponent<SpriteRenderer>();

		if (!thisSpriteRenderer)
		{
			byPass = true;
			return;
		}
		childrenSpriteRendrer = new List<SpriteRenderer>(animator.GetComponentsInChildren<SpriteRenderer>());
		childrenSpriteRendrer.Remove(thisSpriteRenderer);

		if (childrenSpriteRendrer.Count == 0)
		{
			byPass = true;
			return;
		}
		childrenColor = new List<Color>(childrenSpriteRendrer.Count);
		foreach (SpriteRenderer r in childrenSpriteRendrer)
		{
			childrenColor.Add(r.color);
			r.color = thisSpriteRenderer.color;
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (byPass)
			return;

		foreach (SpriteRenderer r in childrenSpriteRendrer)
			r.color = thisSpriteRenderer.color;

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (byPass)
			return;
		
		for (int i = 0; i < childrenSpriteRendrer.Count; i++)
			childrenSpriteRendrer[i].color = childrenColor[i];
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
