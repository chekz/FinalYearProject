using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * 	AutomotiveCar is responsible for updating path points for automotive
 * 	cars in the scene. This script also moves the automotive cars by
 *  implementing a seek steering behaviour in between path points.
 * 		
 * 
 * Unless otherwise stated in the comments below I decalre that all 
 * code in this script is entirely my own work.
 */

public class AutomotiveCar : MonoBehaviour 
{
	GameObject playerCar;
	GameObject path;
	string state;

	List< Transform > pathPoints;
	List< BoxCollider > trafficLightTriggers;

	public bool driving;
	public float carSpeed;
	public int currentPoint;

	float maxCarSpeed;
	float epsilon = 5.0f;
	int pathPointCount;
	
	Vector3 targetPos;

	// Use this for initialization
	void Start ()
	{	
		driving = true;
		path = GameObject.Find( "PathParent" );
		pathPoints = new List< Transform >();

		GetPathPoints();
		pathPointCount = pathPoints.Count;

		carSpeed = 1;
		maxCarSpeed = 4.5f;
	}

	// Update is called once per frame
	void Update()
	{
		// Sets the target position of the car to the current path point
		targetPos = pathPoints [ currentPoint ].transform.position;

		// Gets distance between the target position and the automotive car
		float distance = ( targetPos - transform.position ).magnitude;

		if ( distance < epsilon ) 
		{
			currentPoint++;
		}

		// Resets the path loop once it reaches the end
		if( driving == true )
		{
			if( currentPoint >= path.transform.childCount )
			{
				currentPoint = 0;
			}

			transform.position += Seek ( pathPoints [ currentPoint ].transform.position );
		}

		// Simulates Acceleration
		if( carSpeed <= maxCarSpeed )
		{
			carSpeed += carSpeed * Time.deltaTime;
		}
	}

	// Gets all path points in the scene
	void GetPathPoints()
	{
		for( int i = 0; i < path.transform.childCount; i++ )
		{
			pathPoints.Add( path.transform.GetChild( i ) );
		}
	}

	// Seek steering behaviour
	Vector3 Seek( Vector3 targetPos )
	{
		Vector3 desiredVelocity = targetPos - transform.position;
		desiredVelocity.y = 0;
		transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( desiredVelocity ),  2.5f * Time.deltaTime);
		Vector3 moveVector = desiredVelocity.normalized * carSpeed * Time.deltaTime;
	
		return moveVector;
	}	
}