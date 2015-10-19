using UnityEngine;
using System.Collections;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * TrafficLightStopTrigger is responsible for stopping automotive cars
 * breaking a red light. 
 * 	
 * Unless otherwise stated in the comments below I decalre that all 
 * code in this script is entirely my own work.
 */

public class TrafficLightStopTrigger : MonoBehaviour 
{
	GameObject automotiveCar;

	Light redLight;
	Light orangeLight;

	bool triggered;

	// Use this for initialization
	void Start()
	{
		redLight = ( Light )gameObject.transform.parent.FindChild( "LightBox/RedLight" ).GetComponent( typeof( Light ) );
		orangeLight = ( Light )gameObject.transform.parent.FindChild( "LightBox/OrangeLight" ).GetComponent( typeof( Light ) );

		triggered = false;
	}

	// OnTriggerEnter
	void OnTriggerEnter( Collider other )
	{   
		// Checks For TrafficLightCollider and the light color
		if( other.tag == "AICarCollider" && ( redLight.enabled == true || orangeLight.enabled == true ) )
		{
			automotiveCar = other.transform.parent.transform.parent.gameObject;
			automotiveCar.GetComponent< AutomotiveCar >().carSpeed = 0.0f;
			triggered = true;
		}
	}
	
	// Updates once per frame
	void Update()
	{
		// Checks to see if the trafficlight has turned green
		if( triggered == true && redLight.enabled == false && orangeLight.enabled == false )
		{
			automotiveCar.GetComponent< AutomotiveCar >().carSpeed = 4.5f;
			triggered = false;
		}
	}
}