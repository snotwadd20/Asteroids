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

		ScreenWrap();
	}//Update

	bool isWrappingX = false;
	bool isWrappingY = false;

	void ScreenWrap()
	{
		var isVisible = CheckRenderers();

		if(isVisible)
		{
			isWrappingX = false;
			isWrappingY = false;
			return;
		}

		if(isWrappingX && isWrappingY) {
			return;
		}

		var cam = Camera.main;
		var viewportPosition = cam.WorldToViewportPoint(transform.position);
		var newPosition = transform.position;

		if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
		{
			newPosition.x = -newPosition.x;

			isWrappingX = true;
		}

		if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
		{
			newPosition.y = -newPosition.y;

			isWrappingY = true;
		}

		transform.position = newPosition;
	}

	bool CheckRenderers()
	{
		Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

		foreach(var renderer in GetComponents<Renderer>())
		{
			
			// If at least one render is visible, return true
			if(GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds))
			{
				return true;
			}
		}

		// Otherwise, the object is invisible
		return false;
	}

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
		asteroid.GetComponent<Rigidbody2D>().AddForce(forceDir * scatterForce /20);

	}//Spawn
}//
