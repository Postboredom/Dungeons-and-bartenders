using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs; //array of objects that could be placed

    private GameObject currentPlaceableObject; //current object that the player is trying to place
    public GameObject currentPlaceableObjectNameHolder;

    public bool editModeOn;

    private float mouseWheelRotation;

    private void Update()
    {
        if(currentPlaceableObject == null)
        {
            HandleNewObjectHotkey();
        }
        

        if (currentPlaceableObject != null) //if the player is trying to place an object
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            StartCoroutine (ReleaseIfClicked());
        }
    }

    //this function handles the changing of the current placeable object
    private void HandleNewObjectHotkey()
    {
        for(int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if(placeableObjectPrefabs[i].name == currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().currentPlaceableObjectName && !currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced)
            {
                Debug.Log("I'd like to spawn a " + currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().currentPlaceableObjectName);
                currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                
            }
           
        }
        if(Input.GetMouseButtonDown(0) && editModeOn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                currentPlaceableObject = hitInfo.transform.gameObject;
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
            }
        }
    }

    public void EditMode()
    {
        editModeOn = !editModeOn;
    }

    //this function moves the object to where the mouse is and rotates it to fit the orientation of the environment
    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    //this function allows the player to rotate objects using the mouse wheel
    private void RotateFromMouseWheel()
    {
        Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    //this function handles placing the object when the player clicks
    IEnumerator ReleaseIfClicked()
    {
        yield return new WaitForSeconds(1);
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject.layer = 0;
            currentPlaceableObject = null;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced = true;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();
            editModeOn = false;
        }
    }
}
