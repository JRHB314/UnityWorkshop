using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour {

	public float moveSpeed;

	private float holdX;
	private float holdY;
	private bool moving;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		moving = false;

		float horiz = Input.GetAxisRaw("Horizontal");
		float vert = Input.GetAxisRaw("Vertical");

		if(horiz > 0f || horiz < 0f) {
			float movement = horiz * moveSpeed * Time.deltaTime;
			transform.Translate(new Vector3(movement, 0f, 0f));
			moving = true;
			holdX = horiz;
			holdY= 0;
		}
		if(vert > 0f || vert < 0f) {
			float movement = vert * moveSpeed * Time.deltaTime;
			transform.Translate(new Vector3(0f, movement, 0f));
			moving = true;
			holdY = vert;
			holdX = 0;
		}
		
		anim.SetFloat("xMvmt", horiz);
		anim.SetFloat("yMvmt", vert); 
		anim.SetFloat("lastX", holdX);
		anim.SetFloat("lastY", holdY);
		anim.SetBool("isMoving", moving);
	}
}
