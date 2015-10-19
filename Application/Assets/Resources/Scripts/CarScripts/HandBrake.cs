using UnityEngine;
using System.Collections;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * 	Handbrake is responsible for applying a brake torque
 *  to static cars to simulate a handbrake
 * 	
 * Unless otherwise stated in the comments below I decalre that all 
 * code in this script is entirely my own work.
 */

public class HandBrake : MonoBehaviour
{
	WheelCollider backLeftWheelCollider;
	WheelCollider backRightWheelCollider;

	// Use this for initialization
	void Start () 
	{
		// Setting Wheel Collider Variables
		backLeftWheelCollider = ( WheelCollider )gameObject.transform.Find( "Wheels/BackLeftWheel/BackLeftWheelCollider" ).GetComponent( typeof( WheelCollider ) );
		backRightWheelCollider = ( WheelCollider )gameObject.transform.Find( "Wheels/BackRightWheel/BackRightWheelCollider" ).GetComponent( typeof( WheelCollider ) );

		// braketorque acts as a handbrake
		backLeftWheelCollider.brakeTorque = 600.0f;
		backRightWheelCollider.brakeTorque = 600.0f;
	}
}