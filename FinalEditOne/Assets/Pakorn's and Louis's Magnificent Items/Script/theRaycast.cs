using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theRaycast : MonoBehaviour {

	public float raycastLength = 5f;
	public GameObject turretBasic;
	public GameObject turretGat;
	public GameObject turretRocket;
	public GameObject turretFire;
	public GameObject factory;

	public GameObject minimap;

	private bool LookAtTile = false;
	private GameObject tileLookingAt;
	private GameObject tileLooked;
	private TurrentPlacement turretTile;

	//Yeong var
	public Image tOne;
	public Image tTwo;
	public Image tThree;
	public Image tFour;
	public Image tFive;
	private static int points;

	public int turretSelection = 0;
	Text scoreGT;
	Text costCO;

	void Start(){
		
		points = 160;
		//InvokeRepeating ("updateSeconds",0.0f, 1.0f);
		GameObject scoreGO = GameObject.Find ("ScoreCounter");
		scoreGT = scoreGO.GetComponent<Text> ();

		minimap = GameObject.Find ("MiniMapImage");

		GameObject costCOST = GameObject.Find ("Cost");
		costCO = costCOST.GetComponent<Text> ();
	}

	void FixedUpdate () {

		minimap.SetActive (Input.GetKey(KeyCode.Tab));


		if (Input.GetKey (KeyCode.Alpha1)) {
			costCO.text = "Building Cost: 10";
			turretSelection = 0;
			tOne.color = new Color (1, 1, 1, 1);
			tTwo.color = new Color (0, 0, 0, 1);
			tThree.color = new Color (0, 0, 0, 1);
			tFour.color = new Color (0, 0, 0, 1);
			tFive.color = new Color (0, 0, 0, 1);

		} else if (Input.GetKey (KeyCode.Alpha2)) {
			costCO.text = "Building Cost: 70";
			turretSelection = 1;
			tOne.color = new Color (0, 0, 0, 1);
			tTwo.color = new Color (0, 0, 0, 1);
			tThree.color = new Color (0, 0, 0, 1);
			tFour.color = new Color (1, 1, 1, 1);
			tFive.color = new Color (0,0, 0, 1);

		} else if (Input.GetKey (KeyCode.Alpha3)) {
			costCO.text = "Building Cost: 70";
			turretSelection = 2;
			tOne.color = new Color (0, 0, 0, 1);
			tTwo.color = new Color (0, 0, 0, 1);
			tThree.color = new Color (1, 1, 1, 1);
			tFour.color = new Color (0, 0, 0, 1);
			tFive.color = new Color (0, 0, 0, 1);
		} 
		else if (Input.GetKey (KeyCode.Alpha4)) {
			costCO.text = "Building Cost: 100";
			turretSelection = 3;
			tOne.color = new Color (0, 0, 0, 1);
			tTwo.color = new Color (1, 1, 1, 1);
			tThree.color = new Color (0, 0, 0, 1);
			tFour.color = new Color (0, 0, 0, 1);
			tFive.color = new Color (0, 0, 0, 1);
		}
		else if (Input.GetKey (KeyCode.Alpha5)) {
			costCO.text = "Building Cost: 140";
			turretSelection = 4;
			tOne.color = new Color (0, 0, 0, 1);
			tTwo.color = new Color (0, 0, 0, 1);
			tThree.color = new Color (0, 0, 0, 1);
			tFour.color = new Color (0, 0, 0, 1);
			tFive.color = new Color (1, 1, 1, 1);
		}
	}


	void Update () {

		RaycastHit hit;
		Vector3 diagonal = new Vector3 (0, -1, 3);
		Color color = Color.red;

		if (Physics.Raycast (transform.position, transform.TransformDirection(diagonal), out hit, raycastLength)) {
			Debug.DrawRay(transform.position, transform.TransformDirection(diagonal), color);
			if (hit.collider.tag == "Box") {
				tileLookingAt = hit.collider.gameObject;
				turretTile = tileLookingAt.GetComponent<TurrentPlacement> ();
				turretTile.ChangeColor ();	

				if (Input.GetMouseButtonDown (0) && turretTile.hasTurretGet () == false) { //When left click, place turret if tile does not have turrret
					if (turretSelection == 0 && points >= 10) {
						points -= 10;
						Instantiate (turretBasic, new Vector3 (tileLookingAt.transform.position.x, 0.5f, tileLookingAt.transform.position.z), tileLookingAt.transform.rotation);
						turretTile.hasTurretTrue ();
					} 
					else if (turretSelection == 1 && points >= 70) {
						points -= 70;
						Instantiate (turretFire, new Vector3 (tileLookingAt.transform.position.x, 0.5f, tileLookingAt.transform.position.z), tileLookingAt.transform.rotation);
						turretTile.hasTurretTrue ();
					}
					else if (turretSelection == 2 && points >= 70) {
						points -= 70;
						Instantiate (turretRocket, new Vector3 (tileLookingAt.transform.position.x, 0.5f, tileLookingAt.transform.position.z), tileLookingAt.transform.rotation);
						turretTile.hasTurretTrue ();
					}
					else if (turretSelection == 3 && points >= 100) {
						points -= 100;
						Instantiate (turretGat, new Vector3 (tileLookingAt.transform.position.x, 0.5f, tileLookingAt.transform.position.z), tileLookingAt.transform.rotation);
						turretTile.hasTurretTrue ();
					}
					else if (turretSelection == 4 && points >= 140) {
						points -= 140;
						Instantiate (factory, new Vector3 (tileLookingAt.transform.position.x, 0f, tileLookingAt.transform.position.z), tileLookingAt.transform.rotation);
						turretTile.hasTurretTrue ();
					}
				}

				if (tileLooked != null && tileLooked != tileLookingAt) { //Ensure that if tile you're not looking at will change.
					turretTile = tileLooked.GetComponent<TurrentPlacement> ();
					turretTile.ChangeColorBack ();
				}

				tileLooked = tileLookingAt; //Store tileLooked with what tile you just looked at

				LookAtTile = true;
			}
			if (hit.collider.tag == "Turret" && Input.GetMouseButtonDown (1)) {
				Destroy (hit.collider.gameObject);
				turretTile.hasTurretFalse ();
			}
		}
		else{
			turretTile.ChangeColorBack ();
			Debug.DrawRay(transform.position, transform.TransformDirection(diagonal), color);
			LookAtTile = false;
			//if (!LookAtTile)
			//	Debug.Log ("Not Looking At Tile");
		}

		SetCountText();

	}

	void updateSeconds(){
		points = points + 1;
	}

	public int getPoints(){
		return points;
	}

	public void setPoints(int value ){
		points = points + value;
	}

	void SetCountText(){
		scoreGT.text = "Resource Points: " + points.ToString();
	}
}
