using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
//{

//	// Use this for initialization
//	public Component target;
//	public Transform targetT;
//	private Vector3 dis;
//	void Awake()
//	{
//		targetT = target.GetComponent<Transform>();
//		dis = targetT.position - this.transform.position;
//	}
//	// Update is called once per frame
//	void Update()
//	{
//		Vector3 t = targetT.position - this.transform.position;

//		this.transform.Translate(t-dis);
//	}
//}
{
	public Transform target;            // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.

	Vector3 offset;                     // The initial offset from the target.

	void Start()
	{
		// Calculate the initial offset.
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = target.position + offset;

		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
