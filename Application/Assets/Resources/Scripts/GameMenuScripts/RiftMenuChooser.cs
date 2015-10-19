using UnityEngine;
using System.Collections;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * RiftMenuChooser is responsible for loading various scenes offered within the application.
 * The Oculus Rift acts as a mouse. The user can choose what to do by looking at
 * a button and waiting for the appropriate responce. 
 * 
 * Unless otherwise stated in the comments below, I decalre that all 
 *  code in this script is entirely my own work.
 */

public class RiftMenuChooser : MonoBehaviour
{
	RaycastHit hit;
	Ray ray;

	GameObject lastHitObject;
	AudioClip buttonSound;
	float timer;
	bool playSound;

	// Use this for initialization
	void Start ()
	{
		hit = new RaycastHit();
		lastHitObject = new GameObject();
		lastHitObject.name = "LastHitObject";
		gameObject.AddComponent( typeof( AudioSource ) );
		gameObject.AddComponent( typeof( AudioListener ) );

		// Audio source from http://www.soundjay.com/button/sounds/button-16.mp3
		gameObject.audio.clip = ( AudioClip ) Resources.Load( "Sounds/MenuSounds/ButtonSound" );
		timer = 0;
		playSound = false;
	}

	void Update ()
	{
		// Creates a new ray from the forward look vector of the cameras right eye.
		ray = new Ray(transform.position, transform.forward );
	
		if( Physics.Raycast( ray, out hit ) )
		{
			//Debug.DrawLine(transform.position,hit.transform.position, Color.cyan);

			// Depending on what the ray hits, it performs the appropriate action.
			switch( hit.collider.gameObject.name )
			{
			case "txt_ReverseAroundCorner":
				PlaySound( timer );
				UpdateRayCast( hit.collider.gameObject, ref timer );

				if(timer > 2.0f )
				{
					Application.LoadLevel( "ReverseAroundACorner" );	
				}
				break;
				
			case "txt_ParrallelParking":
				PlaySound( timer );
				UpdateRayCast( hit.collider.gameObject, ref timer );

				if( timer > 2.0f )
				{
					Application.LoadLevel( "ParrallelParking" );	
				}
				break;
				
			case "txt_FreeDriving":
				PlaySound( timer );
				UpdateRayCast( hit.collider.gameObject, ref timer );
				
				if( timer > 2.0f )
				{
					Application.LoadLevel( "FreeDrive" );	
				}
				break;

			case "txt_HowToPlay":
				PlaySound( timer );
				UpdateRayCast( hit.collider.gameObject, ref timer );
				
				if( timer > 2.0f )
				{
					Application.LoadLevel( "HowToPlay" );	
				}
				break;
				
			case "txt_QuitGame":
				PlaySound( timer );
				UpdateRayCast( hit.collider.gameObject, ref timer );

				if(timer > 2.0f )
				{
					Application.Quit();
				}
				break;
				
			default:
				break;
			}
			ChangeToWhiteChecker( hit.collider.gameObject.name );
		}
	}

	// Play Button Tone
	void PlaySound( float timer )
	{
		if( timer == 0 )
		{
			audio.Play();
		}
	}

	// Updates Ray
	void UpdateRayCast(GameObject hitGameObject, ref float timer)
	{
		hitGameObject.renderer.material.color = Color.red;
		timer += Time.deltaTime;
		lastHitObject = hitGameObject;
	}

	// Sets a highlighted object back to white if the user stops pointing the ray at it.
	void ChangeToWhiteChecker( string hitName )
	{
		//Changes color back to white if object not highlighted
		if ( hitName != lastHitObject.name )
		{	
			timer = 0;
			
			if( lastHitObject.name == "txt_ReverseAroundCorner" ||
			    lastHitObject.name == "txt_ParrallelParking" ||
			    lastHitObject.name == "txt_FreeDriving" ||
			    lastHitObject.name == "txt_HowToPlay" ||
			    lastHitObject.name == "txt_QuitGame"
			   )
			{
				lastHitObject.renderer.material.color = Color.white;
			}
		}
	}
}

