using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	[Header("Set in Inspector")] 

	public GameObject buildCube;
	public int numCubeMin = 6;
	public int numCubeMax = 10;
	public Vector3 sphereOffsetScale = new Vector3(5,2,1);
	public Vector2 cubecaleRangeX = new Vector2(4,8);
	public Vector2 cubecaleRangeY = new Vector2(3,4);
	public Vector2 cubecaleRangeZ = new Vector2(2,4);
	public float scaleYMin = 2f;
	private List<GameObject> cube;

	void Start () {
		cube = new List<GameObject>();
		int num = Random.Range(numCubeMin, numCubeMax);
		for (int i = 0; i < num; i++) {
			GameObject sp = Instantiate<GameObject>(buildCube); 
			cube.Add( sp );
			Transform spTrans = sp.transform;
			spTrans.SetParent( this.transform );

			// Randomly assign a position
			Vector3 offset = Random.insideUnitSphere; 
			offset.x *= sphereOffsetScale.x;
			//offset.y *= sphereOffsetScale.y;
			offset.z *= sphereOffsetScale.z;
			spTrans.localPosition = offset;
			// Randomly assign scale
			Vector3 scale = Vector3.one;
			scale.x = Random.Range(cubecaleRangeX.x, cubecaleRangeX.y);
			//scale.y = Random.Range(cubecaleRangeY.x, cubecaleRangeY.y);
			scale.z = Random.Range(cubecaleRangeZ.x, cubecaleRangeZ.y);
			// Adjust y scale by x distance from core
			scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
			scale.y = Mathf.Max( scale.y, scaleYMin );
			spTrans.localScale = scale; 
		}
	}

	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown(KeyCode.Space)) { 
			Restart();
		}*/
	}

	void Restart()
	{
		// Clear out old cube
		foreach (GameObject sp in cube) {
			Destroy(sp);
		}
		Start();
	}
}

