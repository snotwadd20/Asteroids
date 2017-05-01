using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour 
{
	public float turnSpeed = 100.0f;

	public Bullet bulletPrefab = null;
	// Use this for initialization
	void Awake () 
	{
		
	}//Awake
	
	// Update is called once per frame
	void Update () 
	{
		float h = Input.GetAxisRaw("Horizontal");
		transform.eulerAngles += new Vector3(0,0,turnSpeed * Time.deltaTime * h);
	}//Update
}//
