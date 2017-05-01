using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour 
{
	public float turnSpeed = 100.0f;
	public float moveSpeed = 100f;
	public float shotSpeed = 10;
	public Bullet bulletPrefab = null;

	private Rigidbody2D rb2d = null;

	// Use this for initialization
	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D>();
	}//Awake
	
	// Update is called once per frame
	void Update () 
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		transform.eulerAngles += new Vector3(0,0,turnSpeed * Time.deltaTime * h * -1);

		if(v != 0)
			rb2d.AddForce(transform.up * v * moveSpeed * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Shoot();
		}//if

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

	void OnCollisionEnter2D(Collision2D coll)
	{
		Asteroid asteroid = coll.gameObject.GetComponent<Asteroid>();
		if (asteroid != null)
		{
			Die();
		}
	}//

	void Die()
	{
		Invoke("Reset", 2.0f);
		Destroy(rb2d);
		this.enabled = false;
		GetComponent<Renderer>().enabled = false;
	}//

	void Reset()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}//Reset

	void Shoot()
	{
		Vector3 shotVelocity = transform.up;
		shotVelocity = shotVelocity.normalized * shotSpeed;
		Bullet bullet = Instantiate<Bullet>(bulletPrefab);
		bullet.gameObject.transform.position = transform.position;
		bullet.Shoot(shotVelocity);
	}//Shoot
}//
