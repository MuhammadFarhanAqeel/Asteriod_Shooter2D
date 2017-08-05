using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose: 
 * Looping the transform across the rectangle area
 * when the object leaves from the right, it comes from left.
 */


[AddComponentMenu("Farhan/ScreenLooper")]
public class TransformLooper : MonoBehaviour {



	//public Rect area;
	public GameArea gameArea;
	Vector3 areaSpacePosition;


	// Update is called once per frame
	void Update () {
		//position = transform.position;

		areaSpacePosition = gameArea.transform.InverseTransformPoint(transform.position);

		if (gameArea.Area.Contains(areaSpacePosition))
			return;


		if (areaSpacePosition.x < gameArea.Area.xMin)
			areaSpacePosition.x = gameArea.Area.xMax;
		else if (areaSpacePosition.x > gameArea.Area.xMax)
			areaSpacePosition.x = gameArea.Area.xMin;


		if (areaSpacePosition.y < gameArea.Area.yMin)
			areaSpacePosition.y = gameArea.Area.yMax;
		else if (areaSpacePosition.y > gameArea.Area.yMax)
			areaSpacePosition.y = gameArea.Area.yMin;
		
		//transform.position = position;
		transform.position = gameArea.transform.TransformPoint(areaSpacePosition);

	}



}
