using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs; //array of objects that could be placed

    private GameObject currentPlaceableObject; //current object that the player is trying to place
    public GameObject currentPlaceableObjectNameHolder;
    public GameObject BarStatsHandler;

    public bool editModeOn = false;
    public bool deleteModeOn = false;

    private float mouseWheelRotation;

    private void Update()
    {
        if(currentPlaceableObject == null)
        {
            HandleNewObjectHotkey();
        }
        

        if (currentPlaceableObject != null) //if the player is trying to place an object
        {
            StartCoroutine (MoveCurrentObjectToMouse());
            StartCoroutine (RotateFromMouseWheel());
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
                if(CostTooMuch())
                {
                    currentPlaceableObject = null;
                    Debug.Log("You are too broke");
                }
                else
                {
                    currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                }
               
            }
           
        }
        if(Input.GetMouseButtonDown(0) && editModeOn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                if(deleteModeOn)
                {
                    currentPlaceableObject = hitInfo.transform.gameObject;
                    currentPlaceableObject.layer = 2;
                    currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                    Destroy(currentPlaceableObject);
                }
                else
                {
                    currentPlaceableObject = hitInfo.transform.gameObject;
                    currentPlaceableObject.layer = 2;
                    currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                }
            }
        }
    }

    public void EditMode()
    {
        editModeOn = !editModeOn;
        
    }

    public void DeleteMode()
    {
        deleteModeOn = !deleteModeOn;
    }

    private bool CostTooMuch()
    {
        if(currentPlaceableObject.GetComponent<ItemProperties>().goldCost > BarStatsHandler.GetComponent<BarStatsHandler>().totalGold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //this function moves the object to where the mouse is and rotates it to fit the orientation of the environment
    IEnumerator MoveCurrentObjectToMouse()
    {
        yield return new WaitForSeconds(1);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    //this function allows the player to rotate objects using the mouse wheel
    IEnumerator RotateFromMouseWheel()
    {
        yield return new WaitForSeconds(1);
        Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    //this function handles placing the object when the player clicks
    IEnumerator ReleaseIfClicked()
    {
        yield return new WaitForSeconds(1);
        if (Input.GetMouseButtonDown(0) && BarStatsHandler.GetComponent<BarStatsHandler>().totalGold >= currentPlaceableObject.GetComponent<ItemProperties>().goldCost)
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().totalGold -= currentPlaceableObject.GetComponent<ItemProperties>().goldCost;
            currentPlaceableObject.layer = 0;
            currentPlaceableObject = null;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced = true;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();
            Debug.Log(BarStatsHandler.GetComponent<BarStatsHandler>().totalGold);
            editModeOn = false;
        }
        else
        {
            Debug.Log("You broke bitch");
        }
             
    }
}
