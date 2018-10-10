using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{

	private Quaternion _eulerAngle;
//	function Start(){
//		iniRot = transform.rotation;
//	}
// 
//	function LateUpdate(){
//		transform.rotation = iniRot;
//	}
	// Use this for initialization
	void Start ()
	{
		_eulerAngle = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		transform.rotation = _eulerAngle;
	}
}
