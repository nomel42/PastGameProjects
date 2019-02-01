using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour {

	//Can be changed to level that increases in difficulty
	public Transform enemyPreFab;
	public Transform enemyFasterPreFab;

	public Transform spawnPoint;

	public float timer = 0f;
	public float waitTimer = 5f; 
	private float marchDistance = 0.5f;

	int enemyCreated = 0;
	int totalEnemy = 10;
	int totalWaves = 10;
	public int waveNumber = 0;
	public bool levelOver = false;

	GameObject g;
	theRaycast scpt;
	SceneMangerScript sceneM;

	Text levelT;


	void Start()
	{
		GameObject levelTextTemp = GameObject.Find ("levelText");
		levelT =  levelTextTemp.GetComponent<Text> ();

		GameObject scManager = GameObject.Find ("SceneManager");
		sceneM = scManager.GetComponent<SceneMangerScript> ();

		g = GameObject.Find ("Raycast");
		scpt = g.GetComponent<theRaycast> ();
		StartCoroutine ("waveTimerFunction");
	}

	void Update () {
		levelT.text = " Wave: "+ waveNumber +"/"+ (totalWaves+1);

		if (levelOver == true) {
			sceneM.updateLevel ();
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}

	IEnumerator waveTimerFunction ()
	{
		while (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {
			while (waveNumber <= totalWaves) {	
				yield return new WaitForSeconds (waitTimer);
				while (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {

					waveNumber++;

					if (waveNumber != 1) {
						Debug.Log ("update point number");
						scpt.setPoints (15);
					}

					for (int i = 0; i < totalEnemy; i++) {
						SpawnEnemy ();
						yield return new WaitForSeconds (marchDistance);
					}

					if (waveNumber >= totalWaves / 1.5) {
						totalEnemy += 40;
						marchDistance = .10f;
					} else if (waveNumber >= totalWaves / 4) {
						totalEnemy += 30;
						marchDistance = .25f;
					} else {
						totalEnemy += 10;
					}
						
				}
			}
		}
		///
		Debug.Log("reach");
		yield return new WaitForSeconds (10f);
		levelOver = true;
	}

	void SpawnEnemy ()
	{
			enemyCreated += 1;
			Vector3 addedPosition = new Vector3 (0, 0.2f, 0); //Compensate for Barrack's origin anchor
			Vector3 addedRotation = new Vector3 (0, -90, 0);
			addedPosition = spawnPoint.position + addedPosition;
			addedRotation = spawnPoint.rotation.eulerAngles + addedRotation;
			Quaternion temp = Quaternion.Euler (addedRotation);
		if(waveNumber > totalWaves/2)
			Instantiate (enemyFasterPreFab, addedPosition, temp);
		else
			Instantiate (enemyPreFab, addedPosition, temp);
	}
		
}
