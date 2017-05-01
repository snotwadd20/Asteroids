using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour 
{
	public GameObject mediumPrefab = null;
	public GameObject smallPrefab = null;

	public int numMed = 3;
	public int numSmall = 3;

	public enum Type {Large, Medium, Small}
	public Type type = Type.Large;
	// Use this for initialization
	void Awake () 
	{
		
	}//Awake
	
	// Update is called once per frame
	void Update () 
	{
		
	}//Update

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (type == Type.Small)
		{
			Destroy(gameObject);
			return;
		}//if

		int num = 0;
		GameObject prefab = mediumPrefab;
		if (type == Type.Large)
		{
			num = numMed;
			prefab = mediumPrefab;
		}//if
		else if (type == Type.Medium)
		{
			num = numSmall;
			prefab = smallPrefab;
		}//else if

		for(int i=0; i < num; i++)
		{
			Spawn(prefab);
		}//for
	}//OnCollisionEnter2D

	void Spawn(GameObject prefab)
	{
		//Spawn at pos
		Instantiate(prefab, transform.position, Quaternion.identity);

		//Give force to scatter

	}//
}//
