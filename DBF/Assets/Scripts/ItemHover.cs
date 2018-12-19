using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHover : MonoBehaviour {

    private Material objectCurrentMaterialHolder;
    [SerializeField]
    private Material hoverMaterial;
    private GameObject groundPlacementController;
    private bool hoveredOver;

    private void Start()
    {
        groundPlacementController = GameObject.Find("GroundPlacementController");
    }

    private void OnMouseEnter()
    {
        if (groundPlacementController.GetComponent<GroundPlacementController>().editModeOn == true)
        {
            hoveredOver = true;
            objectCurrentMaterialHolder = GetComponent<Renderer>().material;
            GetComponent<Renderer>().material = hoverMaterial;
        }
    } 
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && groundPlacementController.GetComponent<GroundPlacementController>().editModeOn == true)
        {
            GetComponent<Renderer>().material = objectCurrentMaterialHolder;
        }
    }
	
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
