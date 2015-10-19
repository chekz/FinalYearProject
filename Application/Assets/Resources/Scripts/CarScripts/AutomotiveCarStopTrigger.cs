using UnityEngine;
using System.Collections;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * AutomotiveCarStopTrigger ensures that automotive cars do 
 * not crash into eachother or static cars. 
 * 		
 * Unless otherwise stated in the comments below I decalre that all 
 * code in this script is entirely my own work.
 */

public class AutomotiveCarStopTrigger : MonoBehaviour
{
	GameObject automotiveCar;
	bool triggered;
	
	// Update is called once per frame
	void Update () 
	{
		if( triggered == true )
		{
			Vector3 toAutomotiveCar = ( gameObject.transform.parent.position - automotiveCar.transform.position ).normalized;

			// Checks to see which automotive car is in front and which one is behind and stops the relevant car accordingly
			if( Vector3.Dot( toAutomotiveCar, automotiveCar.transform.forward ) > 0 )
			{
				automotiveCar.GetComponent< AutomotiveCar >().carSpeed = 0.0f;
			}
			else
			{
				gameObject.transform.parent.transform.parent.GetComponent< AutomotiveCar >().carSpeed = 0.0f;
			}
		}
	}
	
	// OnTriggerEnter
	void OnTriggerEnter( Collider other )
	{
		// Sets automotiveCar
		if( other.tag == "AICarCollider" || other.tag == "StaticCarCollider" )
		{
			automotiveCar = other.transform.parent.transform.parent.gameObject;
			triggered = true;
		}
	}
	
	// OnTriggerExit
	void OnTriggerExit( Collider other )
	{
		if( other.tag == "AICarCollider"  )
		{
			//Wait for 2 seconds before allowing the car to drive off
			Invoke ( "DrivingOn", 2 );
		}
	}

	// Automotive car resumes driving
	void DrivingOn()
	{
		triggered = false;
		automotiveCar.GetComponent< AutomotiveCar >().carSpeed = 1;
	}	
}