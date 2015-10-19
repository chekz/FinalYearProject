using UnityEngine;
using System.Collections;

/*
 * Author: Edy
 * Source: http://forum.unity3d.com/threads/50643-How-to-make-a-physically-real-stable-car-with-WheelColliders
 *
 * The script Edy provided was converted from Unityscript to C#
 */

public class AntiRollBar : MonoBehaviour
{
	WheelCollider frontLeftWheelCollider;
	WheelCollider frontRightWheelCollider;
	float antiRoll;

	// Use this for initialization
	void Start () 
	{
		frontLeftWheelCollider = ( WheelCollider )gameObject.transform.Find( "Wheels/FrontLeftWheel/FrontLeftWheelCollider" ).GetComponent( typeof( WheelCollider ) );
		frontRightWheelCollider = ( WheelCollider )gameObject.transform.Find( "Wheels/FrontRightWheel/FrontRightWheelCollider" ).GetComponent( typeof(WheelCollider) );
		antiRoll = 5000.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		WheelHit hit; 
		float travelLeft = 1.0f;
		float travelRight = 1.0f;

		bool groundedLeft = frontLeftWheelCollider.GetGroundHit(out hit);

		if( groundedLeft )
		{
			travelLeft = ( -frontLeftWheelCollider.transform.InverseTransformPoint(hit.point).y - frontLeftWheelCollider.radius ) / frontLeftWheelCollider.suspensionDistance;
		}

		bool groundedRight = frontRightWheelCollider.GetGroundHit( out hit );

		if(groundedRight)
		{
			travelRight = ( -frontRightWheelCollider.transform.InverseTransformPoint( hit.point ).y - frontRightWheelCollider.radius) / frontRightWheelCollider.suspensionDistance;
		}

		float antiRollForce = ( travelLeft - travelRight ) * antiRoll;

		if( groundedLeft )
		{
			rigidbody.AddForceAtPosition( frontLeftWheelCollider.transform.up * -antiRollForce, frontLeftWheelCollider.transform.position );
		}

		if(groundedRight)
		{
			rigidbody.AddForceAtPosition( frontRightWheelCollider.transform.up * -antiRollForce, frontRightWheelCollider.transform.position );
		}
	}
}