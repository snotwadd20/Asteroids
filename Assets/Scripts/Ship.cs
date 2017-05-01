using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour 
{
	public float turnSpeed = 100.0f;
	public float shotSpeed = 10;
	public Bullet bulletPrefab = null;
	// Use this for initialization
	void Awake () 
	{
		
	}//Awake
	
	// Update is called once per frame
	void Update () 
	{
		float h = Input.GetAxisRaw("Horizontal");
		transform.eulerAngles += new Vector3(0,0,turnSpeed * Time.deltaTime * h * -1);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Shoot();
		}//if
	}//Update

	void Shoot()
	{
		Vector3 shotVelocity = transform.up;
		shotVelocity = shotVelocity.normalized * shotSpeed;
		Bullet bullet = Instantiate<Bullet>(bulletPrefab);
		bullet.gameObject.transform.position = transform.position;
		bullet.Shoot(shotVelocity);
	}//Shoot
}//
