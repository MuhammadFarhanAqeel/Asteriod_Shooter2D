using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class AudioManager : MonoBehaviour {


	public AudioMixer mixer;



	void Start(){
		mixer.SetFloat("MusicVol", PlayerSettings.MusicVolume.LinearToDecibal());
		mixer.SetFloat("SfxVol", PlayerSettings.sfxVolume.LinearToDecibal());

		PlayerSettings.MusicVolumeChanged += delegate(float Volume)
		{
				mixer.SetFloat("MusicVol", Volume.LinearToDecibal());
		};

		PlayerSettings.SfxVolumeChanged += delegate(float Volume)
		{
				mixer.SetFloat("SfxVol", Volume.LinearToDecibal());

		};

	}



}
