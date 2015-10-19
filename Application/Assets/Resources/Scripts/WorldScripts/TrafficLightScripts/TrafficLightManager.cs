using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * TrafficLightManager is responsible for updating traffic lights within 
 * the applications enviorment. It is attached to the TrafficLightParent 
 * GameObject in the application. 
 * 	
 * Unless otherwise stated in the comments below I decalre that all 
 * code in this script is entirely my own work.
 */

public class TrafficLightManager : MonoBehaviour
{
	GameObject straightTrafficLights;
	GameObject reverseTrafficLights;
	GameObject rightTrafficLights;
	GameObject leftTrafficLights;

	List< Light > straightGreenLights;
	List< Light > straightOrangeLights;
	List< Light > straightRedLights;

	List< Light > reverseGreenLights;
	List< Light > reverseOrangeLights;
	List< Light > reverseRedLights;

	List< Light > leftGreenLights;
	List< Light > leftOrangeLights;
	List< Light > leftRedLights;

	List<Light> rightGreenLights;
	List<Light> rightOrangeLights;
	List<Light> rightRedLights;

	Vector3 trafficLightTimes;
	float straightTrafficLightTimer;
	float reverseTrafficLightTimer;
	float leftTrafficLightTimer;
	float rightTrafficLightTimer;

	// Use this for initialization
	void Start ()
	{
		straightGreenLights = new List< Light >();
		straightOrangeLights = new List< Light >();
		straightRedLights = new List< Light >();

		reverseGreenLights = new List< Light >();
		reverseOrangeLights = new List< Light >();
		reverseRedLights = new List< Light >();

		leftGreenLights = new List< Light >();
		leftOrangeLights = new List< Light >();
		leftRedLights = new List< Light >();

		rightGreenLights = new List< Light >();
		rightOrangeLights = new List< Light >();
		rightRedLights = new List< Light >();

		straightTrafficLights = GameObject.Find( "StraightTrafficLights" );
		reverseTrafficLights = GameObject.Find( "ReverseTrafficLights" );
		leftTrafficLights = GameObject.Find( "LeftTrafficLights" );
		rightTrafficLights = GameObject.Find( "RightTrafficLights" );

		trafficLightTimes = new Vector3( 16 , 2 , 20 );
		straightTrafficLightTimer = 0.0f;
		reverseTrafficLightTimer = 0.0f;
		leftTrafficLightTimer = 0.0f;
		rightTrafficLightTimer = 0.0f;

		SetupTrafficLights();
	}

	// Updates once per frame
	void Update()
	{
		straightTrafficLightTimer += Time.deltaTime;
		reverseTrafficLightTimer += Time.deltaTime;
		leftTrafficLightTimer += Time.deltaTime;
		rightTrafficLightTimer += Time.deltaTime;

		UpdateTrafficLights( straightGreenLights, straightOrangeLights, straightRedLights, ref straightTrafficLightTimer );
		UpdateTrafficLights( reverseGreenLights, reverseOrangeLights, reverseRedLights, ref reverseTrafficLightTimer );
		UpdateTrafficLights( leftGreenLights, leftOrangeLights, leftRedLights, ref leftTrafficLightTimer );
		UpdateTrafficLights( rightGreenLights, rightOrangeLights, rightRedLights, ref rightTrafficLightTimer );
	}

	// Sets up Traffic Lights
	void SetupTrafficLights()
	{
		// Straight Traffic Lights
		for( int i = 0; i < straightTrafficLights.transform.childCount ; i++ )
		{
			Transform straightTrafficLight = straightTrafficLights.transform.GetChild(i);
			AddTrafficLights( straightTrafficLight, straightGreenLights, straightOrangeLights, straightRedLights );
			EnableLights ( straightGreenLights[i], straightOrangeLights[i], straightRedLights[i], false, false, true );
		}

		// Reverse Traffic Lights
		for( int i = 0; i < reverseTrafficLights.transform.childCount ; i++ )
		{
			Transform reverseTrafficLight = reverseTrafficLights.transform.GetChild(i);
			AddTrafficLights( reverseTrafficLight, reverseGreenLights, reverseOrangeLights, reverseRedLights );
			EnableLights( reverseGreenLights[i], reverseOrangeLights[i], reverseRedLights[i], false, false, true );
		}

		// Left Traffic Lights
		for( int i = 0; i < leftTrafficLights.transform.childCount ; i++ )
		{
			Transform leftTrafficLight = leftTrafficLights.transform.GetChild(i);
			AddTrafficLights( leftTrafficLight, leftGreenLights, leftOrangeLights, leftRedLights );
			EnableLights( leftGreenLights[i], leftOrangeLights[i], leftRedLights[i], true, false, false );
		}

		// Right Traffic Lights
		for(int i = 0; i < rightTrafficLights.transform.childCount ; i++)
		{
			Transform rightTrafficLight = rightTrafficLights.transform.GetChild(i);
			AddTrafficLights( rightTrafficLight, rightGreenLights, rightOrangeLights, rightRedLights );
			EnableLights( rightGreenLights[i], rightOrangeLights[i], rightRedLights[i], true, false, false );
		}
	}

	// Adds Traffic Lights to List of Lights
	void AddTrafficLights(Transform lightParent , List< Light > greenLights, List< Light > orangeLights, List< Light > redLights  )
	{
		greenLights.Add( AddLight( lightParent, "Support/LightBox/GreenLight" ) );
		orangeLights.Add( AddLight( lightParent, "Support/LightBox/OrangeLight" ) );
		redLights.Add( AddLight( lightParent, "Support/LightBox/RedLight" ) );
	}

	// Returns Light Component
	Light AddLight( Transform lightParent, string location )
	{
		Light light = ( Light )lightParent.Find( location ).GetComponent( typeof( Light ) );
		return light;
	}

	// Turns Traffic Lights Off And On
	void EnableLights( Light greenlight,Light orangelight,Light redlight,bool greenLightSwitch,bool orangeLightSwitch, bool redLightSwitch )
	{
		greenlight.enabled = greenLightSwitch;
		orangelight.enabled = orangeLightSwitch;
		redlight.enabled = redLightSwitch;
	}

	// Updates Traffic Light function
	void UpdateTrafficLights( List< Light > greenLights, List< Light > orangeLights, List< Light > redLights, ref float timer )
	{
		//Change From Red To Green
		if( timer >= trafficLightTimes.z  && redLights[0].enabled == true)
		{
			for( int i = 0 ; i < greenLights.Count ; i++ )
			{
				EnableLights( greenLights[i], orangeLights[i], redLights[i], true, false, false );
			}

			timer = trafficLightTimes.y;
		}

		//Change From Green To Orange 
		if( timer  >= trafficLightTimes.x && greenLights[0].enabled == true)
		{
			for( int i = 0 ; i < greenLights.Count ; i++ )
			{
				EnableLights( greenLights[i], orangeLights[i], redLights[i], false, true, false );
			}

			timer = 0.0f;
		}

		//Change From Orange To Red
		if( timer >= trafficLightTimes.y  && orangeLights[0].enabled == true)
		{
			for( int i = 0 ; i < greenLights.Count ; i++ )
			{
				EnableLights( greenLights[i], orangeLights[i], redLights[i], false, false, true );
			}
			
			timer = 0.0f;
		}
	}
}