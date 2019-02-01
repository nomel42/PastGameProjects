using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	static public Spawner S;
	static public List<BirdO> Bird;

	[Header("Set in Inspector: Spawning")]
	public GameObject birdoPrefab;
	public Transform  birdAnchor;

	public int number = 100;
	public float spawnRadius = 100f;
	public float spawnDelay = 0.1f;

	[Header("Set in Inspector Birdo")]
	public float velocity = 30f;
	public float neighborDist = 30f;
	public float collDist = 4f;
	public float velMatching = 0.25f;
	public float flockCentering = 0.2f;
	public float collAvoid = 2f;
	public float attractPull = 2f;
	public float attractPush = 2f;
	public float attractPushDist = 5f;

	void Awake(){
		S = this;
		Bird = new List<BirdO> ();
		InstantiateBirdO();
	}

	public void InstantiateBirdO(){
		GameObject go = Instantiate (birdoPrefab);
		BirdO b = go.GetComponent<BirdO> ();
		b.transform.SetParent (birdAnchor);
		Bird.Add (b);
		if (Bird.Count < number) {
			Invoke ("InstantiateBirdO", spawnDelay);
		}

	}

}
