using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRelease : MonoBehaviour {


	public float duration;



	void Awake () {
		if (duration == 0)
		{
			Animator animator = GetComponent<Animator>();
			float animatorLength = animator ? animator.GetCurrentAnimatorClipInfo(0)[0].clip.length : 0;
			AudioSource audio = GetComponent<AudioSource>();
			float audioLength = audio ? audio.clip.length : 0;

			duration = Mathf.Max(animatorLength, audioLength);
		}
	}
	

	void OnEnable () {
		StartCoroutine(ReleaseAuto());
	}


	IEnumerator ReleaseAuto()
	{
		yield return new WaitForSeconds(duration);	
		gameObject.SetActive(false);
	}
}
