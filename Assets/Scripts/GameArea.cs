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
public class GameArea : MonoBehaviour {

	[SerializeField]
	[HideInInspector]
	Rect _area;

	public Rect Area{
		get{ return _area;}
		set{ _area = value;}
	}

	public	Color gizmoColor = new Color(0, 0, 1, .2f);
	Color gizmoWireColor;


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



	void OnDrawGizmos(){
		Gizmos.matrix = transform.localToWorldMatrix;

		Gizmos.color = gizmoColor;
		Gizmos.DrawCube(Vector2.zero, new Vector3(Area.width, Area.height, 0));
		Gizmos.color = gizmoWireColor;
		Gizmos.DrawWireCube(Vector2.zero, new Vector3(Area.width, Area.height, 0));

	}

	void OnValidate(){
		Size = size;
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
