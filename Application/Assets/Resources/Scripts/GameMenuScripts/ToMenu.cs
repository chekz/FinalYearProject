using UnityEngine;
using System.Collections;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 
 * ToMenu sends the user back to the main menu if 
 * escape has been pressed
 * 	
 * Unless otherwise stated in the comments below I decalre that all 
 * code in this script is entirely my own work.
 */

public class ToMenu : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
	{
		if ( Input.GetKey( KeyCode.Escape ) )
		{
    		Application.LoadLevel( "MainMenu" );
		}
	}
}
