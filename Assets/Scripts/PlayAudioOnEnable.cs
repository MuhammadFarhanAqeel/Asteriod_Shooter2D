using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioOnEnable : MonoBehaviour {

	AudioSource _audio;

	void Awake () {
		_audio = GetComponent<AudioSource>();
	}
	
	void OnEnable(){
		_audio.Play();
	}

}
