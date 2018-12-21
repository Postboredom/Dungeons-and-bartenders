using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHover : MonoBehaviour {

    /// <summary>
    /// This script changes the material of an object based on if you are hovering over it in edit mode
    /// </summary>

    //this material holds the original material of the object being hovered over
    private Material objectCurrentMaterialHolder;

    //this material is the material applied to the object when it is being hovered over
    [SerializeField]
    private Material hoverMaterial;

    private GameObject groundPlacementController;

    //checks if an object was hovered over
    private bool hoveredOver;

    private void Start()
    {
        groundPlacementController = GameObject.Find("GroundPlacementController");
    }

    //if you hover over an object in the scene
    private void OnMouseEnter()
    {
        //if you are in editing mode replace the material when hovering over an object for the first time
        if (groundPlacementController.GetComponent<GroundPlacementController>().editModeOn == true)
        {
            hoveredOver = true;
            objectCurrentMaterialHolder = GetComponent<Renderer>().material;
            GetComponent<Renderer>().material = hoverMaterial;
        }
    } 
    //if you continue to hover over an object in the scene
    private void OnMouseOver()
    {
        //if you click on the object and you are editing, give it back its original material before moving it
        if(Input.GetMouseButtonDown(0) && groundPlacementController.GetComponent<GroundPlacementController>().editModeOn == true)
        {
            GetComponent<Renderer>().material = objectCurrentMaterialHolder;
        }
    }
	//once you stop hovering over an object, give it back its original material
    private void OnMouseExit()
    {
        if(groundPlacementController.GetComponent<GroundPlacementController>().editModeOn == true)
        {
            if(hoveredOver == true)
            {
                GetComponent<Renderer>().material = objectCurrentMaterialHolder;
                hoveredOver = false;
            }
        }
            
    }
}
