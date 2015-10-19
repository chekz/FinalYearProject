using UnityEngine;
using System.Collections;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * PlayerCarStopTrigger primary function is to stop 
 * automotive cars from crashing into the player car.
 * 	
 * Unless otherwise stated in the comments below I decalre that all 
 * code in this script is entirely my own work.
 */

public class PlayerCarStopTrigger : MonoBehaviour 
{
	GameObject automotiveCar;
	bool triggered;

	// Update is called once per frame
	void Update () 
	{
		if( triggered == true )
		{
			Vector3 toAutomotiveCar = ( gameObject.transform.parent.position - automotiveCar.transform.position ).normalized;

			// Automotive car will only stop if it is behind player car
			if( Vector3.Dot( toAutomotiveCar, automotiveCar.transform.forward ) > 0 )
			{
				automotiveCar.GetComponent< AutomotiveCar >().carSpeed = 0.0f;
			}
		}
	}

	// OnTriggerEnter
	void OnTriggerEnter( Collider other )
	{
		if( other.tag == "AICarCollider" )
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
			//Wait for 2 seconds before allowing automotive car to proceed.
			Invoke ( "DrivingOn", 2 );
		}
	}
	
	void DrivingOn()
	{
		triggered = false;
		automotiveCar.GetComponent< AutomotiveCar >().carSpeed = 1;
	}
}