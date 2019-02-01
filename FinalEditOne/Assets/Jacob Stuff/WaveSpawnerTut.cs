using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawnerTut : MonoBehaviour {

	//Can be changed to level that increases in difficulty
	public Transform enemyPreFab;

	public Transform spawnPoint;
	public float waitTimer = 5f;
	private float marchDistance = 0.5f;

	GameObject g;
	theRaycast scpt;

	Text levelT;


	void Start()
	{
		GameObject levelTextTemp = GameObject.Find ("levelText");
		levelT =  levelTextTemp.GetComponent<Text> ();

		g = GameObject.Find ("Raycast");
		scpt = g.GetComponent<theRaycast> ();
		StartCoroutine ("waveTimerFunction");
	}

	void FixedUpdate () {
		levelT.text = " Level: INF";
	}

	IEnumerator waveTimerFunction ()
	{
		while (true) {	
			yield return new WaitForSeconds (waitTimer);
			while (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
				scpt.setPoints (10);

				for (int i = 0; i < 10; i++) {
					SpawnEnemy ();
					yield return new WaitForSeconds (marchDistance);
				}
				yield return new WaitForSeconds (waitTimer);
			}
		}
	}

	void SpawnEnemy ()
	{
			Vector3 addedPosition = new Vector3 (0, 0.2f, 0); //Compensate for Barrack's origin anchor
			Vector3 addedRotation = new Vector3 (0, -90, 0);
			addedPosition = spawnPoint.position + addedPosition;
			addedRotation = spawnPoint.rotation.eulerAngles + addedRotation;
			Quaternion temp = Quaternion.Euler (addedRotation);
			Instantiate (enemyPreFab, addedPosition, temp);
	}
		
}
