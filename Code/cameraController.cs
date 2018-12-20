using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

	public GameObject target;
	private Vector3 targetPos; 
	public float cameraSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float xPos = target.transform.position.x;
		float yPos = target.transform.position.y;
		float zPos = transform.position.z;
		targetPos = new Vector3(xPos, yPos, zPos);
		transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
		
	}
}
