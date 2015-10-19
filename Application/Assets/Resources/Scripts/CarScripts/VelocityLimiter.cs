using UnityEngine;
using System.Collections;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * VelocityLimiter's function is to set a cap on
 * how fast the player's car can travel.
 * 	
 * Unless otherwise stated in the comments below I decalre that all 
 * code in this script is entirely my own work.
 */

public class VelocityLimiter : MonoBehaviour 
{
	float maxSpeed;

	void Start () 
	{
		maxSpeed = 148;
	}

	void FixedUpdate ()
	{
		// Caps Velocity at the maxSpeed value, KM/H
		if( ( rigidbody.velocity.magnitude * 3.6f ) > maxSpeed  )
		{
			rigidbody.velocity =  ( rigidbody.velocity.normalized  * maxSpeed ) / 3.6f;
		}
	}
}