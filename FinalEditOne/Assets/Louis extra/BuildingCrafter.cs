using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCrafter : MonoBehaviour {
	[Header("Set in Inspector")]
	public int numbuilds = 40; // The # of builds to make
	public GameObject buildPrefab; // The prefab for the builds
	public Vector3 buildPosMin = new Vector3(-50,-5,10);
	public Vector3 buildPosMax = new Vector3(150,100,10);
	public float buildScaleMin = 1; // Min scale of each build
	public float buildScaleMax = 3; // Max scale of each build
	public float buildSpeedMult = 0.5f; // Adjusts speed of builds
	private GameObject[] buildInstances;

	void Awake() {
		// Make an array large enough to hold all the build_instances
		buildInstances = new GameObject[numbuilds];

		// Find the buildAnchor parent GameObject
		GameObject anchor = GameObject.Find("BuildAnchor");

		// Iterate through and make build_s
		GameObject build;

		for (int i = 0; i < numbuilds; i++) {
			// Make an instance of buildPrefab
			build = Instantiate<GameObject>( buildPrefab );

			// Position build
			Vector3 cPos = Vector3.zero;
			cPos.x = Random.Range( buildPosMin.x, buildPosMax.x );
			cPos.y = Random.Range( buildPosMin.y, buildPosMax.y );

			// Scale build
			float scaleU = Random.value;
			float scaleVal = Mathf.Lerp( buildScaleMin, buildScaleMax, scaleU );

			// Smaller builds (with smaller scaleU) should be nearer the ground
			cPos.y = Mathf.Lerp( buildPosMin.y, cPos.y, scaleU );

			// Smaller builds should be further away
			cPos.z = 100 - 90*scaleU;

			// Apply these transforms to the build
			build.transform.position = cPos;
			build.transform.localScale = Vector3.one * scaleVal;

			// Make build a child of the anchor
			build.transform.SetParent( anchor.transform );

			// Add the build to buildInstances
			buildInstances[i] = build;
		}
	}

	void Update() {
		// Iterate over each build that was created
		foreach (GameObject build in buildInstances) {
			// Get the build scale and position
			float scaleVal = build.transform.localScale.x;
			Vector3 cPos = build.transform.position;

			// Move larger builds faster
			cPos.x -= scaleVal * Time.deltaTime * buildSpeedMult;

			// If a build has moved too far to the left...
			if (cPos.x <= buildPosMin.x) {
				// Move it to the far right
				cPos.x = buildPosMax.x;
			}

			// Apply the new position to build
			build.transform.position = cPos;
		}
	}
}


