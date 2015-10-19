using UnityEngine;
using System.Collections;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * 	PlayerCarController's function is to take input from the user
 *  from the game controller and apply appropriate forces to the 
 *  player car game object. PlayerCarController is also responsible for
 *  playing the cars audio and shifting through the different gears.
 * 	
 * Unless otherwise stated in the comments below I decalre that all 
 * code in this script is entirely my own work.
 */

public class PlayerCarController : MonoBehaviour 
{
	//Car Wheels
	WheelCollider backLeftWheelCollider;
	WheelCollider backRightWheelCollider;
	WheelCollider frontLeftWheelCollider;
	WheelCollider frontRightWheelCollider;

	float engineTorque;
	float brakeTorque;
	float engineDirection;
	float brakeDirection;
	float[] gearRatios;
	float speed;
	float maxEngineRPM;
	float minEngineRPM;
	float engineRPM;
	float travellingForward;
	float travellingBackward;

	int currentGear;

	bool reversing;

	// Use this for initialization
	void Start () 
	{
		gameObject.AddComponent( typeof( AudioSource ) );
		gameObject.AddComponent( typeof( AudioListener ) );

		//Car sound clip from https://www.assetstore.unity3d.com/#/content/10
		audio.clip = ( AudioClip )Resources.Load( "Sounds/CarSounds/CanEngine_F_midhigh-register" );
		audio.loop = true;
		audio.Play();

		backLeftWheelCollider = ( WheelCollider )gameObject.transform.Find( "Wheels/BackLeftWheel/BackLeftWheelCollider" ).GetComponent( typeof( WheelCollider ) );
		backRightWheelCollider = ( WheelCollider )gameObject.transform.Find( "Wheels/BackRightWheel/BackRightWheelCollider" ).GetComponent( typeof( WheelCollider ) );
		frontLeftWheelCollider = ( WheelCollider )gameObject.transform.Find( "Wheels/FrontLeftWheel/FrontLeftWheelCollider" ).GetComponent( typeof( WheelCollider ) );
		frontRightWheelCollider = ( WheelCollider )gameObject.transform.Find( "Wheels/FrontRightWheel/FrontRightWheelCollider" ).GetComponent( typeof(WheelCollider) );

		rigidbody.centerOfMass = new Vector3(0,-1,0);

		engineTorque = 100;
		brakeTorque = 25;
		engineDirection = 0.0f;
		brakeDirection = 0.0f;
		gearRatios = new float[6];
		gearRatios = setGearRatios( gearRatios );
		currentGear = 0;
		speed = 0;
		maxEngineRPM = 350;
		minEngineRPM = 100;
		engineRPM = 0;
	 	travellingForward = 0;
		travellingBackward = 0;

		reversing = false;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		speed = rigidbody.velocity.magnitude * 3.6f;

		travellingForward = Mathf.Clamp( Input.GetAxis( "Vertical" ), 0, 1);
		travellingBackward =  -1 * Mathf.Clamp( Input.GetAxis( "Vertical" ), -1, 0 );

		// Caclulate EngineRPM
		engineRPM = ( backLeftWheelCollider.rpm + backRightWheelCollider.rpm )/ 2
			* gearRatios[ currentGear ];

		// Moves car forward or backward based on what gear the user is in
		if(reversing == false)
		{
			backLeftWheelCollider.motorTorque = ( engineTorque / gearRatios[ currentGear ] ) * Input.GetAxis( "Vertical" );

			backRightWheelCollider.motorTorque = ( engineTorque / gearRatios[ currentGear ] ) * Input.GetAxis( "Vertical" );
		}
		else
		{
			backLeftWheelCollider.motorTorque = ( engineTorque / gearRatios[ currentGear ] ) * Input.GetAxis( "Vertical" ) * -1;
			backRightWheelCollider.motorTorque = ( engineTorque / gearRatios[ currentGear ] ) * Input.GetAxis( "Vertical" ) * -1;
		}

		if( Input.GetAxis( "Vertical" ) < 0 )
		{
			backLeftWheelCollider.brakeTorque = brakeTorque;
			backRightWheelCollider.brakeTorque = brakeTorque;
		}
		else
		{
			backLeftWheelCollider.brakeTorque = 0;
			backRightWheelCollider.brakeTorque = 0;
		}

		if( Input.GetKeyDown( KeyCode.JoystickButton5 ) )
		{
			reversing = switchReverse( reversing );
		}

		// Car Steering
		frontLeftWheelCollider.steerAngle = 11 * Input.GetAxis( "Horizontal" );
		frontRightWheelCollider.steerAngle = 11 * Input.GetAxis( "Horizontal" );

		audio.pitch = Mathf.Abs( engineRPM / maxEngineRPM ) + 0.75f ;

		// Pitch Capper
		if ( audio.pitch > 2.0 )
		{
			audio.pitch = 2.0f;
		}

		ShiftGears();
	}

	// Sets gear ratios for the player car object
	float[] setGearRatios( float[] gearRatios )
	{
		gearRatios[0] = 3.0f;
		gearRatios[1] = 4.0f;
		gearRatios[2] = 4.0f;
		gearRatios[3] = 4.0f;
		gearRatios[4] = 5.0f;
		gearRatios[5] = 5.0f;

		return gearRatios;
	}

	// The following function ShiftGears() was obtained from http://answers.unity3d.com/questions/137386/car-can-someone-help-me-convert-this-to-c.html
	// It is unclear who the author was.

	// Shifts Gears
	void ShiftGears()
	{
		int appropriateGear;

		if( engineRPM > maxEngineRPM )
		{
			appropriateGear = currentGear;

			for( int i = 0; i < gearRatios.Length; i++ )
			{
				if( backLeftWheelCollider.rpm * gearRatios[i] < maxEngineRPM )
				{
					appropriateGear = i;
					break;
				}
			}

			currentGear = appropriateGear;
		}

		if( engineRPM < minEngineRPM )
		{
			appropriateGear = currentGear;
			
			for( int i = gearRatios.Length - 1; i >= 0; i-- )
			{
				if( backLeftWheelCollider.rpm * gearRatios[i] > minEngineRPM )
				{
					appropriateGear = i;
					break;
				}
			}
			
			currentGear = appropriateGear;
		}
	}

	bool switchReverse( bool reversing )
	{
		if( reversing == true )
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}