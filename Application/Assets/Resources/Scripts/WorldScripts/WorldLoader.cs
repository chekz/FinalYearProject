using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Author: Cian Gardiner
 * Student Number: C10734285
 * 
 * Script Description:
 * 		This script reads in values from a text file, and builds items based on the values provided
 * 
 * Unless otherwise stated in the comments below, I decalre that all 
 *  code in this script is entirely my own work.
 * 
 */


public class WorldLoader : MonoBehaviour 
{
	public TextAsset sceneLoader;

	GameObject roadParent;
	GameObject straightRoadParent;
	GameObject sidewaysRoadParent;
	GameObject junctionParent;
	GameObject buildingsParent;
	GameObject staticCarParent;
	GameObject automotiveCarParent;
	GameObject trafficLightParent;
	GameObject straightTrafficLightParent;
	GameObject rightTrafficLightParent;
	GameObject leftTrafficLightParent;
	GameObject reverseTrafficLightParent;
	GameObject pathParent;
	GameObject mainMenu;
	GameObject mainCamera;
	
	int straightRoadCounter;
	int sidewaysRoadCounter;
	int junctionCounter;
	int buildingCounter;
	int staticCarCounter;
	int automotiveCarCounter;
	int straightTrafficLightCounter;
	int rightTrafficLightCounter;
	int leftTrafficLightCounter;
	int reverseTrafficLightCounter;
	int pathPointCounter;
	int currentPathPoint;
	int lineCounter;

	Vector3 objectPosition;
	Vector3 objectScale;
	Vector3 objectRotation;
	Vector3 menuItemTextScale;
	Vector3 textRotation;

	string line;
	bool playerCarIntialized;
	bool groundIntialized;
	bool trafficLightsOn;

	// Use this for initialization
	void Start ()
	{
		objectPosition = new Vector3( 0, 0, 0 );
		objectScale = new Vector3( 0, 0, 0 );
		objectRotation = new Vector3( 0, 0, 0 );

		lineCounter = 0;

		playerCarIntialized = false;
		groundIntialized = false;
		
		//Setting the texture
		Load( sceneLoader );
		TurnOnTrafficLights();
	}
	
	//Loads in the contents of a file
	private void Load(TextAsset fileName)
	{
    	//Splits text file into individual lines
		string[] lines = fileName.text.Split("\n"[0]);

		//Reads through every text line
		foreach(string line in lines)
		{
			lineCounter++;

			//Comma delimited values
			string[] entries = line.Split( ',' );

				if ( entries.Length == 10 )
				{
					BuildObject( entries );
				}
				else if( entries.Length == 11 )
				{
					currentPathPoint = int.Parse( entries[10] );
					BuildObject( entries );
				}
				else
				{
					Debug.Log( "Error at Line: " + lineCounter );
				}
		}
	}
    
	//Builds object based off words entered from the text file
	private void BuildObject( string[] entries )
	{	
		string objectName = entries[0];
		objectPosition = new Vector3( float.Parse( entries[1] ),
		                              float.Parse( entries[2] ),
		                              float.Parse( entries[3] ) );
		objectScale = new Vector3( float.Parse( entries[4] ),
		                           float.Parse( entries[5] ),
		                           float.Parse( entries[6] ) ); 
		objectRotation = new Vector3( float.Parse( entries[7] ),
		                              float.Parse( entries[8] ),
		                              float.Parse( entries[9] ) ); 
		
		//Different objects to build
		switch ( objectName.ToLower() )
		{
		//Skybox
		case "skybox":
			BuildSkybox();
			break;

		//Main Menu
		case "main_menu":
			BuildMainMenu();
			break;

		//How To Play:
		case "how_to_play":
			BuildHowToPlayMenu();
			break;
			
		//Road parent
		case "road_parent":
			roadParent = new GameObject();
			SetParentDetails( roadParent, "RoadParent", 0, null );
			break;

		//Straight road parent
		case "straight_road_parent":
			straightRoadParent = new GameObject();
			SetParentDetails ( straightRoadParent, "StraightRoads",
			                   straightRoadCounter, roadParent );
			break;

		//Sideways road parent
		case "sideways_road_parent":
			sidewaysRoadParent = new GameObject();
			SetParentDetails ( sidewaysRoadParent, "SidewaysRoads",
			                   sidewaysRoadCounter, roadParent );
			break;

		//Junction parent
		case "junction_parent":
			junctionParent = new GameObject();
			SetParentDetails ( junctionParent, "Junctions",
			                   junctionCounter, roadParent );
			break;

		//Buildings parent
		case "buildings_parent":
			buildingsParent = new GameObject();
			SetParentDetails ( buildingsParent, "Buildings",
			                   buildingCounter, null );
			break;

		//Static car parent
		case "static_car_parent":
			staticCarParent = new GameObject();
			SetParentDetails ( staticCarParent, "StaticCars",
			                   staticCarCounter, null );
			break;

		//Automotive car parent
		case "automotive_car_parent":
			automotiveCarParent = new GameObject();
			SetParentDetails ( automotiveCarParent, "AutomotiveCars",
			                   automotiveCarCounter, null );
			break;

		//Trafficlight parent
		case "traffic_light_parent":
			trafficLightParent = new GameObject();
			SetParentDetails ( trafficLightParent, "TrafficLights",
			                   0, null );
			break;

		//Straight trafficlight parent
		case "straight_traffic_light_parent":
			straightTrafficLightParent = new GameObject();
			SetParentDetails ( straightTrafficLightParent, "StraightTrafficLights",
			                   straightTrafficLightCounter, trafficLightParent );
			break;

		//Reverse trafficlight parent
		case "reverse_traffic_light_parent":
			reverseTrafficLightParent = new GameObject();
			SetParentDetails ( reverseTrafficLightParent, "ReverseTrafficLights",
			                   reverseTrafficLightCounter, trafficLightParent );
			break;

		//Left trafficlight parent
		case "left_traffic_light_parent":
			leftTrafficLightParent = new GameObject();
			SetParentDetails ( leftTrafficLightParent, "LeftTrafficLights",
			                   leftTrafficLightCounter, trafficLightParent );
			break;

		//Right trafficlight parent
		case "right_traffic_light_parent":
			rightTrafficLightParent = new GameObject();
			SetParentDetails ( rightTrafficLightParent, "RightTrafficLights",
			                   rightTrafficLightCounter, trafficLightParent );
			break;

		//Right trafficlight parent
		case "path_parent":
			pathParent = new GameObject();
			SetParentDetails ( pathParent, "PathParent", pathPointCounter, null );
			break;
		
		//Plane
		case "plane":
			BuildPlane();
			break;
			
		//Straight Road	
		case "straight_road":
			BuildStraightRoad();
			break;
			
		//Sideways Road
		case "sideways_road":
			BuildSidewaysRoad();
			break;

		//Junction
		case "junction":
			BuildJunction();
			break;

		//Building 1
		case "building":
			BuildBuilding();
			break;

		//Player Car
		case "player_car":
			BuildPlayerCar();
			break;
		
		//Static Car
		case "static_car":
			BuildStaticCar();
			break;

		//Automotive Car
		case "automotive_car":
			BuildAutomotiveCar();
			break;

		//Straight Traffic Light
		case "traffic_light_straight":
			BuildTrafficLightStraight();
			break;

		//Left Traffic Light
		case "traffic_light_left":
			BuildTrafficLightLeft();
			break;

		//Right Traffic Light
		case "traffic_light_right":
			BuildTrafficLightRight();
			break;

		//Reverse Traffic Light
		case "traffic_light_reverse":
			BuildTrafficLightReverse();
			break;

		//Path
		case "path":
			BuildPath();
			break;
			
		//Default Value	
		default:
			break;
		}
	}

	void BuildMainMenu()
	{	
		BuildMainMenuTemplate(); 

		GameObject txt_GameTitle = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_GameTitle, "txt_GameTitle", null,
							  new Vector3( -5.66f, 6, 8.4f ),
						      new Vector3( 0.042f, 0.084f, 0.84f ),
		                     textRotation, mainMenu );	
		AddTextMeshDetails( txt_GameTitle, "Virtual Driving", 225 );
		
		GameObject txt_ReverseAroundCorner = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_ReverseAroundCorner, "txt_ReverseAroundCorner", null , new Vector3( -4.26f, 6, 4.19f ),
		                      menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_ReverseAroundCorner, "Reverse Around A Corner", 150 );
		
		GameObject txt_ParrallelParking = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_ParrallelParking, "txt_ParrallelParking", null , new Vector3( -2.94f, 6, 2.29f ),
		                      menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_ParrallelParking, "Parrallel Parking", 150 );
		
		GameObject txt_FreeDriving = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_FreeDriving, "txt_FreeDriving", null , new Vector3( -1.92f, 6, 0.48f ),
		                      menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_FreeDriving, "Free Driving", 150 );

		GameObject txt_HowToPlay = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_HowToPlay, "txt_HowToPlay", null , new Vector3( -1.92f, 6, -1.46f ),
		                      menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_HowToPlay, "How To Play", 150 );
		
		GameObject txt_QuitGame = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_QuitGame, "txt_QuitGame", null , new Vector3( -1.67f, 6, -3.32f ),
		                      menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_QuitGame, "Quit Game", 150 );

		AddGameObjectDetails( mainMenu, "MainMenu", null, objectPosition, objectScale, objectRotation, null );
	}

	void BuildHowToPlayMenu()
	{	
		BuildMainMenuTemplate();

		GameObject mainMenuPanel = GameObject.CreatePrimitive( PrimitiveType.Plane );
		
		//Texture comes from 
		AddGameObjectDetails( mainMenuPanel, "MainMenuPanel", ( Texture2D )Resources.Load( "Textures/Menu/MenuTexture", typeof( Texture2D ) ),
		                     Vector3.zero, new Vector3( 2, 2, 2 ), Vector3.zero, mainMenu );
		
		GameObject txt_GameTitle = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_GameTitle, "txt_GameTitle", null,
		                     new Vector3( -5.66f, 6, 8.4f ),
		                     new Vector3( 0.042f, 0.084f, 0.84f ),
		                     textRotation, mainMenu );	
		AddTextMeshDetails( txt_GameTitle, "Virtual Driving", 225 );

		GameObject txt_Line1 = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_Line1, "txt_Line1", null , new Vector3( -4.74f, 6, 4.37f ),
		                     menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_Line1, "Hold the rift on the selected", 150 );

		GameObject txt_Line2 = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_Line2, "txt_Line2", null , new Vector3( -4.74f, 6, 3 ),
		                     menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_Line2, "button for 2 seconds to", 150 );

		GameObject txt_Line3 = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_Line3, "txt_Line3", null , new Vector3( -4.74f, 6, 1.51f ),
		                      menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_Line3, "change the scene", 150 );

		GameObject txt_Line4 = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_Line4, "txt_Line4", null , new Vector3( -4.74f, 6, -1.35f ),
		                      menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_Line4, "Press escape to return to", 150 );

		GameObject txt_Line5 = ( GameObject )Instantiate( Resources.Load( "Fonts/FontText" ) );
		AddGameObjectDetails( txt_Line5, "txt_Line5", null , new Vector3( -4.74f, 6, -2.79f ),
		                      menuItemTextScale, textRotation, mainMenu );
		AddTextMeshDetails( txt_Line5, "the main menu", 150 );
		
		AddGameObjectDetails( mainMenu, "MainMenu", null, objectPosition, objectScale, objectRotation, null );
	}

	void BuildMainMenuTemplate()
	{
		menuItemTextScale = new Vector3( 0.027f, 0.067f, 0.67f );
		textRotation = new Vector3( 90, 0, 0 );

		mainMenu = new GameObject();
		
		GameObject mainMenuPanel = GameObject.CreatePrimitive( PrimitiveType.Plane );
		
		//Texture comes from 
		AddGameObjectDetails( mainMenuPanel, "MainMenuPanel", ( Texture2D )Resources.Load( "Textures/Menu/MenuTexture", typeof( Texture2D ) ),
		                     Vector3.zero, new Vector3( 2, 2, 2 ), Vector3.zero, mainMenu );
		
		mainCamera = new GameObject();
		AddGameObjectDetails( mainCamera, "MainCamera", null , new Vector3( 0, 0, -400 ), Vector3.zero, Vector3.zero, null );
		BuildRiftCamera( mainCamera );

		//Add RiftMenu Chooser Script
		mainCamera.transform.FindChild( "OVRCameraController/CameraRight" ).gameObject.AddComponent( "RiftMenuChooser" );
	}

	void BuildSkybox()
	{
		GameObject skybox = ( GameObject )Resources.Load( "Textures/Skyboxes/Skybox" );
		AddGameObjectDetails( skybox, "Skybox", null, objectPosition, objectScale, objectRotation, null );
		Instantiate( skybox );
	}

	void BuildPlane()
	{
		if(groundIntialized == false)
		{
			GameObject plane = GameObject.CreatePrimitive( PrimitiveType.Plane );
			AddGameObjectDetails( plane, "Ground",
			                     ( Texture2D )Resources.Load( "Textures/Ground/GrassGreenTexture", typeof( Texture2D ) ), 
			                     objectPosition, objectScale, objectRotation, plane );
		}
		else
		{
			Debug.Log("The ground has already been intialized!");
		}
	}

	//Build Straight Road
	void BuildStraightRoad()
	{
		straightRoadCounter++;
		
		GameObject straightRoad = GameObject.CreatePrimitive( PrimitiveType.Cube );
		AddGameObjectDetails( straightRoad, "StraightRoad" + straightRoadCounter,
		                     ( Texture2D )Resources.Load( "Textures/Roads/RoadTexture", typeof( Texture2D ) ),
		                     objectPosition, objectScale, objectRotation, straightRoadParent );
	}

	//Build Sideways Road
	void BuildSidewaysRoad()
	{
		sidewaysRoadCounter++;
		
		GameObject sidewaysRoad = GameObject.CreatePrimitive( PrimitiveType.Cube );
		AddGameObjectDetails( sidewaysRoad, "SidewaysRoad" + sidewaysRoadCounter,
		                     ( Texture2D )Resources.Load( "Textures/Roads/RoadTexture", typeof( Texture2D ) ),
		                     objectPosition, objectScale, objectRotation, sidewaysRoadParent );
	}

	//Build Junction
	void BuildJunction()
	{
		junctionCounter++;
		
		GameObject junction = GameObject.CreatePrimitive( PrimitiveType.Cube );
		AddGameObjectDetails( junction, "Junction" + junctionCounter,
		                     ( Texture2D )Resources.Load( "Textures/Roads/JunctionTexture", typeof( Texture2D ) ),
		                     objectPosition, objectScale, objectRotation, junctionParent );
	}

	void BuildBuilding()
	{
		buildingCounter++;
		
		GameObject building1 = GameObject.CreatePrimitive( PrimitiveType.Cube );
		AddGameObjectDetails( building1, "Building" + buildingCounter,
		                     ( Texture2D )Resources.Load( "Textures/Buildings/BuildingTexture", typeof( Texture2D ) ),
		                     objectPosition, objectScale, objectRotation ,buildingsParent );
	}

	//Builds a car
	void BuildCar( GameObject car )
	{
		//Adding details to car
		car.AddComponent( typeof( Rigidbody ) );
		car.rigidbody.mass = 1650;
		car.rigidbody.angularDrag = 2.25f;
		car.transform.position = objectPosition;
		car.rigidbody.centerOfMass =  new Vector3 ( 0, - 2, 0 );

		//Create Car base
		GameObject carBase = GameObject.CreatePrimitive( PrimitiveType.Cube );
		AddGameObjectDetails( carBase, "CarBase", null, 
		                      objectPosition, objectScale, objectRotation, car);

		//Set scale and rotation
		Vector3 wheelScale =  new Vector3( objectScale.y, 0.1f, objectScale.y );
		Vector3 wheelRoatation = new Vector3( 0, 0, 90 );

		//Wheel Parent
		GameObject wheels = new GameObject();
		wheels.name = "Wheels";
		wheels.transform.parent = car.transform;

		//Front Right Wheel
		GameObject frontRightWheel = GameObject.CreatePrimitive( PrimitiveType.Cylinder );
		frontRightWheel.collider.enabled = false;
		Vector3 wheelPosition = new Vector3( objectPosition.x + objectScale.x * 0.75f,
		                            objectPosition.y - objectScale.y/2,
		                            objectPosition.z + objectScale.z * 0.25F );
		AddGameObjectDetails( frontRightWheel, "FrontRightWheel" , null, wheelPosition, wheelScale, wheelRoatation, wheels );

		//Front Left Wheel
		GameObject frontLeftWheel = GameObject.CreatePrimitive( PrimitiveType.Cylinder );
		frontLeftWheel.collider.enabled = false;
		wheelPosition = new Vector3( objectPosition.x - objectScale.x * 0.75f,
		                            objectPosition.y - objectScale.y/2,
		                            objectPosition.z + objectScale.z * 0.25F );
		AddGameObjectDetails( frontLeftWheel, "FrontLeftWheel", null, wheelPosition, wheelScale, wheelRoatation, wheels );

		//Back Right Wheel
		GameObject backRightWheel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		backRightWheel.collider.enabled = false;
		wheelPosition = new Vector3( objectPosition.x + objectScale.x * 0.75f,
		                            objectPosition.y - objectScale.y/2,
		                            objectPosition.z - objectScale.z * 0.25F );
		AddGameObjectDetails( backRightWheel, "BackRightWheel", null, wheelPosition, wheelScale, wheelRoatation, wheels );

		//Back Left Wheel
		GameObject backLeftWheel = GameObject.CreatePrimitive( PrimitiveType.Cylinder );
		backLeftWheel.collider.enabled = false;
		wheelPosition = new Vector3( objectPosition.x - objectScale.x * 0.75f,
		                            objectPosition.y - objectScale.y/2,
		                            objectPosition.z - objectScale.z * 0.25F );
		AddGameObjectDetails(backLeftWheel, "BackLeftWheel", null, wheelPosition, wheelScale, wheelRoatation, wheels );

		float wheelColliderRadis = objectScale.y/2;

		GameObject frontRightWheelCollider = new GameObject();
		AddGameObjectDetails( frontRightWheelCollider, "FrontRightWheelCollider", null, frontRightWheel.transform.position, Vector3.zero, Vector3.zero, frontRightWheel );
		frontRightWheelCollider.AddComponent( typeof( WheelCollider ) );
		AddWheelColliderDetails( ( WheelCollider )frontRightWheelCollider.GetComponent( typeof( WheelCollider ) ), wheelColliderRadis );

		GameObject frontLeftWheelCollider = new GameObject();
		AddGameObjectDetails( frontLeftWheelCollider, "FrontLeftWheelCollider", null, frontLeftWheel.transform.position, Vector3.zero, Vector3.zero, frontLeftWheel );
		frontLeftWheelCollider.AddComponent( typeof( WheelCollider ) );
		AddWheelColliderDetails( ( WheelCollider )frontLeftWheelCollider.GetComponent( typeof( WheelCollider ) ), wheelColliderRadis );
		
		GameObject backRightWheelCollider = new GameObject();
		AddGameObjectDetails( backRightWheelCollider, "BackRightWheelCollider", null, backRightWheel.transform.position, Vector3.zero, Vector3.zero, backRightWheel );
		backRightWheelCollider.AddComponent( typeof( WheelCollider ) );
		AddWheelColliderDetails( ( WheelCollider )backRightWheelCollider.GetComponent( typeof( WheelCollider ) ), wheelColliderRadis );
	
		GameObject backLeftWheelCollider = new GameObject();
		AddGameObjectDetails( backLeftWheelCollider, "BackLeftWheelCollider", null, backLeftWheel.transform.position, Vector3.zero, Vector3.zero, backLeftWheel );
		backLeftWheelCollider.AddComponent( typeof( WheelCollider ) );
		AddWheelColliderDetails( ( WheelCollider )backLeftWheelCollider.GetComponent( typeof( WheelCollider ) ), wheelColliderRadis );

		AddCarColors( carBase, frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel );
	}


	//Builds a Player Car
	void BuildPlayerCar()
	{	
		//Ensures player car is only created once
		if( playerCarIntialized == false )
		{
			GameObject playerCar = new GameObject();
			playerCar.name = "PlayerCar";

			//Builds the car
			BuildCar( playerCar );

			//Adds car colors
			playerCar.transform.FindChild( "CarBase" ).
				renderer.material.color = Color.blue;
			playerCar.transform.FindChild( "Wheels/FrontRightWheel" ).
				renderer.material.color = Color.white;
			playerCar.transform.FindChild( "Wheels/FrontLeftWheel" ).
				renderer.material.color = Color.white;
			playerCar.transform.FindChild( "Wheels/BackRightWheel" ).
				renderer.material.color = Color.white;
			playerCar.transform.FindChild( "Wheels/BackLeftWheel" ).
				renderer.material.color = Color.white;

			//Builds and attaches Rift to playerCar
			BuildRiftCamera( playerCar );
			playerCar.transform.FindChild( "OVRCameraController" ).
				GetComponent< OVRCameraController >().
					FollowOrientation = playerCar.transform;

			//Adding scripts to playerCar
			playerCar.AddComponent( "PlayerCarController" );
			playerCar.AddComponent( "VelocityLimiter" );
			playerCar.AddComponent( "AntiRollBar" );

			//AddingCollider to playerCar
			AddCollider( objectPosition, objectScale,
			             "PlayerCarCollider", "PlayerCarStopTrigger", playerCar
			            );

			playerCarIntialized = true;
		}
		else
		{
			Debug.Log( "A player car has been intialzied already!" );	
		}
	}
	
	//Builds A Static Car
	void BuildStaticCar()
	{	
		staticCarCounter++;
		
		GameObject staticCar = new GameObject();
		AddGameObjectDetails( staticCar, "StaticCar" + staticCarCounter, null, Vector3.zero, Vector3.zero, Vector3.zero, staticCarParent );

		BuildCar( staticCar );
		staticCar.AddComponent( "HandBrake" );
	}
	
	//Builds A Automotive Car
	void BuildAutomotiveCar()
	{	
		automotiveCarCounter++;

		GameObject automotiveCar = new GameObject();
		AddGameObjectDetails( automotiveCar, "AutomotiveCar" + automotiveCarCounter, null, Vector3.zero, Vector3.zero, Vector3.zero, automotiveCarParent );
		BuildCar(automotiveCar);
		AddCollider( objectPosition, objectScale, "AICarCollider", "AutomotiveCarStopTrigger", automotiveCar );
		automotiveCar.rigidbody.isKinematic = true;
		automotiveCar.AddComponent( "AutomotiveCar" );
		automotiveCar.GetComponent< AutomotiveCar >().currentPoint = currentPathPoint;
	}

	//Builds rift camera
	void BuildRiftCamera(GameObject gameObject)
	{
		GameObject OVRCameraController = new GameObject();
		AddGameObjectDetails( OVRCameraController, "OVRCameraController",
		                      null, gameObject.transform.position,
		                      Vector3.zero, Vector3.zero, gameObject );
		OVRCameraController.AddComponent( "OVRDevice" );

		//Left Eye Camera
		GameObject cameraLeft = new GameObject();
		AddGameObjectDetails( cameraLeft, "CameraLeft", null,
		                      OVRCameraController.transform.position,
		                      Vector3.zero, Vector3.zero, OVRCameraController );
		AddRiftCamera( cameraLeft, 1, new Rect( 0.0f, 0.0f, 0.5f, 1.0f ) );

		//Right Eye Camera
		GameObject cameraRight = new GameObject();
		AddGameObjectDetails( cameraRight, "CameraRight", null,
		                      OVRCameraController.transform.position,
		                      Vector3.zero, Vector3.zero, OVRCameraController );
		AddRiftCamera( cameraRight, 0, new Rect( 0.5f,0.0f,0.499999f,1.0f ) );
	
		//Add script to OVRCameraController
		OVRCameraController.AddComponent( "OVRCameraController" );
	}	

	void AddRiftCamera( GameObject cameraObject, int depth, Rect cameraRect )
	{
		cameraObject.tag = "MainCamera";
		cameraObject.AddComponent( typeof( Camera ) );
		cameraObject.camera.depth = depth;
		cameraObject.AddComponent( "OVRCamera" );
		cameraObject.AddComponent( "OVRLensCorrection" );
		cameraObject.camera.rect = cameraRect;
	}

	//Car Collider
	void AddCollider( Vector3 carColliderPosition, Vector3 carColliderScale, string tagName , string scriptName , GameObject parent )
	{
		float colliderGrowth = 5.0f;
		carColliderScale = new Vector3( carColliderScale.x + 1.2f, carColliderScale.y, carColliderScale.z + colliderGrowth );
		GameObject carColliderBox = new GameObject();
		carColliderBox.tag = tagName;
		AddGameObjectDetails( carColliderBox, "CarColliderBox", null, carColliderPosition, carColliderScale, Vector3.zero, null );
		carColliderBox.transform.parent = parent.transform.FindChild( "CarBase" ).transform;
		AddBoxColliderDetails( carColliderBox, true, scriptName );
	}

	void BuildTrafficLight( GameObject trafficLight )
	{
		//Support Structure
		GameObject support = GameObject.CreatePrimitive( PrimitiveType.Cylinder );
		support.name = "Support";
		support.transform.position = objectPosition;
		support.transform.localScale = objectScale;
		support.transform.parent = trafficLight.transform;

		//Light Box
		GameObject lightBox = GameObject.CreatePrimitive( PrimitiveType.Cube );
		lightBox.name = "LightBox";

		Vector3 lightBoxScale = new Vector3( objectScale.x * 2, objectScale.y, objectScale.z );
		Vector3 lightBoxPosition = new Vector3( objectPosition.x , objectPosition.y +  objectScale.y  , objectPosition.z );

		lightBox.transform.position = lightBoxPosition;
		lightBox.transform.localScale = lightBoxScale;
		lightBox.transform.parent = support.transform;

		float lightRaidus = ( lightBoxScale.y / 3 ) / 2;
		Vector3 lightScale = new Vector3( lightRaidus, 0.01f, lightRaidus );

		//Green Light
		GameObject greenLight = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		greenLight.name = "GreenLight";
		greenLight.tag = "GreenLight";
		greenLight.transform.position = new Vector3(lightBoxPosition.x,lightBoxPosition.y - lightRaidus,lightBoxPosition.z - lightBoxScale.z);
		greenLight.transform.localScale = lightScale;
		greenLight.transform.Rotate(90,0,0);
		greenLight.renderer.material.color = Color.green;
		greenLight.AddComponent(typeof(Light));
		greenLight.light.color = Color.green;
		greenLight.transform.parent = lightBox.transform;

		//Orange Light
		GameObject orangeLight = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		orangeLight.name = "OrangeLight";
		orangeLight.tag = "OrangeLight";
		orangeLight.transform.position = new Vector3(lightBoxPosition.x,lightBoxPosition.y,lightBoxPosition.z - lightBoxScale.z);
		orangeLight.transform.localScale = lightScale;
		orangeLight.transform.Rotate(90,0,0);
		orangeLight.renderer.material.color = Color.yellow;
		orangeLight.AddComponent(typeof(Light));
		orangeLight.light.color = Color.yellow;
		orangeLight.transform.parent = lightBox.transform;

		//Red Light
		GameObject redLight = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		redLight.name = "RedLight";
		redLight.tag = "RedLight";
		redLight.transform.position = new Vector3(lightBoxPosition.x,lightBoxPosition.y + lightRaidus,lightBoxPosition.z - lightBoxScale.z);
		redLight.transform.localScale = lightScale;
		redLight.transform.Rotate(90,0,0);
		redLight.renderer.material.color = Color.red;
		redLight.AddComponent(typeof(Light));
		redLight.light.color = Color.red;
		redLight.transform.parent = lightBox.transform;

		AddTrafficLightCollider( new Vector3( objectPosition.x + 11.5f,4,objectPosition.z - 10 ), new Vector3(12,1,0), support );
		support.transform.eulerAngles = objectRotation;

		trafficLight.AddComponent( typeof( Rigidbody ) );
		trafficLight.rigidbody.isKinematic = true;
	}

	void BuildTrafficLightStraight()
	{
		straightTrafficLightCounter++;
		
		GameObject straightTrafficLight = new GameObject();
		AddGameObjectDetails( straightTrafficLight, "StraightTrafficLight" + straightTrafficLightCounter, null, Vector3.zero, Vector3.zero, Vector3.zero, straightTrafficLightParent );
		
		BuildTrafficLight(straightTrafficLight);
	}

	void BuildTrafficLightLeft()
	{
		leftTrafficLightCounter++;
		
		GameObject leftTrafficLight = new GameObject();
		AddGameObjectDetails( leftTrafficLight, "LeftTrafficLight" + leftTrafficLightCounter, null, Vector3.zero, Vector3.zero, Vector3.zero, leftTrafficLightParent );
		
		BuildTrafficLight(leftTrafficLight);
	}
	
	void BuildTrafficLightRight()
	{
		rightTrafficLightCounter++;
		
		GameObject rightTrafficLight = new GameObject();
		AddGameObjectDetails( rightTrafficLight, "RightTrafficLight" + rightTrafficLightCounter, null, Vector3.zero, Vector3.zero, Vector3.zero, rightTrafficLightParent );
		
		BuildTrafficLight(rightTrafficLight);
	}
	
	void BuildTrafficLightReverse()
	{
		reverseTrafficLightCounter++;
		
		GameObject reverseTrafficLight = new GameObject();
		AddGameObjectDetails( reverseTrafficLight, "RightTrafficLight" + reverseTrafficLightCounter, null, Vector3.zero, Vector3.zero, Vector3.zero, reverseTrafficLightParent );
		
		BuildTrafficLight(reverseTrafficLight);
	}	

	void AddTrafficLightCollider( Vector3 trafficColliderPosition, Vector3 trafficColliderScale, GameObject parent )
	{
		GameObject trafficCollider = new GameObject();
		AddGameObjectDetails( trafficCollider, "TrafficCollider", null, trafficColliderPosition, trafficColliderScale, Vector3.zero, parent );
		AddBoxColliderDetails( trafficCollider, true, "TrafficLightStopTrigger" );
	}

	void TurnOnTrafficLights()
	{
		if( GameObject.Find( "TrafficLights" ) )
		{
			trafficLightParent.AddComponent("TrafficLightManager");
		}

	}

	void BuildPath()
	{
		pathPointCounter++;

		GameObject pathPoint = new GameObject();
		AddGameObjectDetails( pathPoint, "PathPoint" + pathPointCounter, null, objectPosition, Vector3.zero, Vector3.zero, pathParent );
	}
	
	void AddGameObjectDetails(GameObject gameObject, string name, Texture2D texture , Vector3 position, Vector3 scale, Vector3 rotation, GameObject parent)
	{
		gameObject.name = name;

		if(texture != null)
		{
			gameObject.renderer.material.mainTexture = texture;
		}

		if(parent != null)
		{
			gameObject.transform.parent = parent.transform;
		}

		gameObject.transform.position = position;

		if(scale != Vector3.zero)
		{
			gameObject.transform.localScale = scale;
		}

		if( rotation != Vector3.zero )
		{
			gameObject.transform.localEulerAngles = rotation;
		}
	}

	void SetParentDetails( GameObject gameObject, string objectName, int objectCounter, GameObject objectParent )
	{
		gameObject.name = objectName;
		objectCounter = 0;

		if( objectParent != null )
		{
			gameObject.transform.parent = objectParent.transform; 
		}
	}

	void AddWheelColliderDetails(WheelCollider wheelCollider, float radius)
	{
		wheelCollider.radius = radius;
	}

	void AddBoxColliderDetails( GameObject colliderObject, bool trigger, string scriptName )
	{
		colliderObject.AddComponent( typeof( BoxCollider ) );
		colliderObject.collider.isTrigger = trigger;

		if ( scriptName != null )
		{
			colliderObject.AddComponent( scriptName );
		}
	}
	
	void AddTextMeshDetails( GameObject gameObject, string textMeshName, int textMeshFontSize )
	{
		TextMesh textMesh = ( TextMesh ) gameObject.GetComponent( typeof( TextMesh ) );
		textMesh.text = textMeshName;
		textMesh.fontSize = textMeshFontSize;
		gameObject.AddComponent( typeof( BoxCollider ) );
	}

	void AddCarColors( GameObject carBase, GameObject frontLeftWheel, GameObject frontRightWheel, GameObject backLeftWheel, GameObject backRightWheel )
	{
		carBase.renderer.material.color = getRandomColor();

		frontLeftWheel.renderer.material.color = Color.black;
		frontRightWheel.renderer.material.color = Color.black;
		backLeftWheel.renderer.material.color = Color.black;
		backRightWheel.renderer.material.color = Color.black;
	}

	private Color getRandomColor()
	{
		float r = Random.value;
		float g = Random.value;
		float b = Random.value;
		return new Color(r,g,b);
	}
}