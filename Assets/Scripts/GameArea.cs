using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// defines a rectangular area
/// Look again!
/// </summary>
/// 
/// 


[AddComponentMenu("Farhan/GameArea")]
[RequireComponent(typeof(RectTransform))]
public class GameArea : MonoBehaviour {

	[SerializeField]
	[HideInInspector]
	Rect _area;


	RectTransform _rectTransform;
	public RectTransform rectTransform
	{
		get
		{ 
			if (!_rectTransform)
				_rectTransform = GetComponent<RectTransform>();
			return _rectTransform;
		}
	}


	static GameArea _main;
	static public GameArea Main
	{
		get{ 
			if (_main == null)
			{
				List<GameArea> areas = new List<GameArea>(FindObjectsOfType<GameArea>());
				if (areas.Count > 0)
				{
					areas.Sort((f1, f2) => f2.Area.size.magnitude.CompareTo(f1.Area.size.magnitude));
					_main = areas[0];
				}

				if (_main == null)
				{
					GameObject go = new GameObject("Game Area: Main");
					_main = go.AddComponent<GameArea>();
					Canvas canvas = go.AddComponent<Canvas>();
					canvas.renderMode = RenderMode.ScreenSpaceCamera;
					canvas.planeDistance = Camera.main.transform.position.z;
					canvas.worldCamera = Camera.main;
				
				}
			}
			return _main;
		}
		set{
			_main = value;
		}
	}

	public Rect Area
	{
		get{ return rectTransform.rect; }
		set
		{
			rectTransform.sizeDelta = new Vector2(value.width, value.height);
		}
	}

	public	Color gizmoColor = new Color(0, 0, 1, .2f);
	Color gizmoWireColor;

	/*
	public Vector2 size;
	public Vector2 Size{
		get{ 
			return Area.size;
		}
		set{ 
			size = value;
			Area = new Rect(size.x * -0.5f,size.y*-0.5f,size.x,size.y);
		}
	}

*/

	void OnDrawGizmos(){
		Gizmos.matrix = transform.localToWorldMatrix;

		Gizmos.color = gizmoColor;
		Gizmos.DrawCube(Area.center, new Vector3(Area.width, Area.height, 0));
		Gizmos.color = gizmoWireColor;
		Gizmos.DrawWireCube(Area.center, new Vector3(Area.width, Area.height, 0));

	}

	void OnValidate(){
	//	Size = size;
		gizmoWireColor = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1.0f);
	}


	public Vector3 GetRandomPosition(){
		Vector3 randomPos = Vector3.zero;

		randomPos.x = Random.Range(Area.xMin,Area.xMax);
		randomPos.y = Random.Range(Area.yMin,Area.yMax);
		randomPos = transform.TransformPoint(randomPos);

		return randomPos;
	}

}
