using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    public Transform cameraRotationPoint;
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(cameraRotationPoint);
        transform.Translate(Vector3.right * Time.deltaTime);
	}
}
