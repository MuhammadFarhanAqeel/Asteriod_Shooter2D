﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {


	public enum TYPE{ RepairtKit,ExtraLife,Weapon }
	public TYPE type;
	AudioSource _audioSrc;
	public Weapon weapon; 

	Renderer _renderer;
	Collider2D _collider2D;


	void Awake(){
		_audioSrc = GetComponent<AudioSource>();
		_renderer = GetComponent<SpriteRenderer>();
		_collider2D = GetComponent<Collider2D>();
	}



	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag != "Player")
			return;
	
		switch (type)
		{
			case TYPE.ExtraLife:
				GameManager.Lives++;
				break;

			case TYPE.RepairtKit:
				GameManager.Damage = 0;
				break;
			case TYPE.Weapon:
				other.GetComponent<ShipControllerBase>().SwitchWeapon(weapon);
				break;
				 
			default:
				
				break;
		}
		StartCoroutine(PlaySoundAndRelease());
	}


	IEnumerator PlaySoundAndRelease(){
		_renderer.enabled = _collider2D.enabled = false;
		_audioSrc.Play();

		yield return new WaitForSeconds(_audioSrc.clip.length);

		//Destroy(gameObject);
		ObjectPool.Release(gameObject);
	}
}
