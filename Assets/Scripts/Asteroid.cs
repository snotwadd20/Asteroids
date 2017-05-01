using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour 
{
	public Asteroid mediumPrefab = null;
	public Asteroid smallPrefab = null;

	public float speed = 10;

	public int numMed = 3;
	public int numSmall = 3;

	public float scatterForce = 0.1f;

	public enum Type {Large, Medium, Small}
	public Type type = Type.Large;


	public bool explode = false;

	// Use this for initialization
	void Start () 
	{
		//Give force to scatter
		Vector2 forceDir = new Vector2(Random.Range(-10,11), Random.Range(-10,11)).normalized;
		GetComponent<Rigidbody2D>().velocity = forceDir * scatterForce / 2;
	}//Awake
	
	// Update is called once per frame
	void Update () 
	{
		if (explode)
		{
			Explode();
			explode = false;
		}//
	}//Update

	public void Explode()
	{
		if (type != Type.Small)
		{
			int num = 0;
			Asteroid prefab = mediumPrefab;
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
		}//if

		Destroy(gameObject);
	}//Explode


	void Spawn(Asteroid prefab)
	{
		//Spawn at pos
		Asteroid asteroid = Instantiate<Asteroid>(prefab, transform.position, Quaternion.identity);

		if (type == Type.Large)
		{
			asteroid.type = Type.Medium;
		}//if
		else if (type == Type.Medium)
		{
			asteroid.type = Type.Small;
		}//else if

		asteroid.mediumPrefab = mediumPrefab;
		asteroid.smallPrefab = smallPrefab;

		asteroid.numMed = numMed;
		asteroid.numSmall = numSmall;

		asteroid.scatterForce = scatterForce;
		asteroid.speed = speed;

		//Give force to scatter
		Vector2 forceDir = new Vector2(Random.Range(-10,11), Random.Range(-10,11)).normalized;
		asteroid.GetComponent<Rigidbody2D>().velocity = forceDir * scatterForce;

	}//Spawn
}//
