using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ObjectPool : MonoBehaviour {

	List<GameObject> _objectList;
	GameObject objectToRecycle;
	int totalObjectsAtStart;

	static Dictionary<int,ObjectPool> pools = new Dictionary<int, ObjectPool>();


	void Init(){
		_objectList = new List<GameObject>(totalObjectsAtStart);


		for (int i = 0; i < totalObjectsAtStart; i++)
		{
			GameObject newObject = Instantiate(objectToRecycle);
			newObject.transform.SetParent(transform);
			newObject.SetActive(false);

			_objectList.Add(newObject);
		}

		pools.Add(objectToRecycle.GetInstanceID(),this);
	}


	GameObject PoolObject(Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
	{
		var freeObject = (from item in _objectList
			where item.activeSelf == false
			select item).FirstOrDefault();

		if (freeObject == null)
		{
			freeObject = Instantiate(objectToRecycle, position, rotation);
			freeObject.transform.SetParent(transform);
			_objectList.Add(freeObject);
		}
		else
		{
			freeObject.transform.position = position;
			freeObject.transform.rotation = rotation;
			freeObject.SetActive(true);
		}
		return freeObject;
	}


	public void Clear()
	{
		foreach (GameObject obj in _objectList)
			obj.SetActive(false);


	}


	public static void ClearPool(int orignalID){
		pools[orignalID].Clear();
	}

	public static void InitPool(GameObject orignal,int poolSize = 200){
		if (!pools.ContainsKey(orignal.GetInstanceID()))
		{
			GameObject go = new GameObject("ObjectPool:" + orignal.name);
			ObjectPool pool = go.AddComponent<ObjectPool>();
			pool.objectToRecycle = orignal;
			pool.totalObjectsAtStart = poolSize;
			pool.Init();
		}
	}


	public static GameObject GetInstance(int orignalID,Vector3 position = default(Vector3),Quaternion rotation = default(Quaternion), int poolSize = 200){
	
		return pools[orignalID].PoolObject(position, rotation);

	}



	public static GameObject GetInstance(GameObject orignal,Vector3 position = default(Vector3),Quaternion rotation = default(Quaternion), int poolSize = 200){

		//	ObjectPool pool = FindObjectOfType<ObjectPool>();
		//	return pool.PoolObject(position, rotation);
		//	return Instantiate(orignal, position, rotation);

		int id = orignal.GetInstanceID();
		ObjectPool.InitPool(orignal);
		return pools[id].PoolObject(position, rotation);
	}


	public static void Release(GameObject obj){
		//Destroy(obj);
		obj.SetActive(false);
	
	}
}
