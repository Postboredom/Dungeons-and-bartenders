using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {


    /// <summary>
    /// this script rotates the camera around a point for the main menu
    /// it is attached to the camera in the main menu
    /// </summary>

    //this object is the point the camera rotates around
    public Transform cameraRotationPoint;
	
	
	void Update () {

        //look at the rotation point and rotate around it
        transform.LookAt(cameraRotationPoint);
        transform.Translate(Vector3.right * Time.deltaTime);
	}
}
