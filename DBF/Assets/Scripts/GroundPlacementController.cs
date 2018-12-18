using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{

    public GameObject[] placeableObjectPrefabs; //array of objects that could be placed

    private GameObject currentPlaceableObject; //current object that the player is trying to place
    public GameObject currentPlaceableObjectNameHolder;
    public GameObject BarStatsHandler;

    public bool editModeOn = false;
    public bool deleteModeOn = false;
    public bool onPlaceableSurface = false;

    private float mouseWheelRotation;


    private Material objectCurrentMaterialHolder;
    [SerializeField]
    private Material correctPlacementMaterial;
    [SerializeField]
    private Material incorrectPlacementMaterial;
    [SerializeField]
    private GameObject secondStoryCeiling;

    private void Update()
    {
        if (currentPlaceableObject == null)
        {
            GetObjectToPlace();
        }


        if (currentPlaceableObject != null) //if the player is trying to place an object
        {
            StartCoroutine(MoveCurrentObjectToMouse());
            StartCoroutine(RotateFromMouseWheel());
            StartCoroutine(ReleaseIfClicked());
        }
    }

    //this function handles the changing of the current placeable object
    private void GetObjectToPlace()
    {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if (placeableObjectPrefabs[i].name == currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().currentPlaceableObjectName && !currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced)
            {
                Debug.Log("I'd like to spawn a " + currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().currentPlaceableObjectName);
                currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
                if (CostTooMuch())
                {
                    Destroy(currentPlaceableObject);
                    currentPlaceableObject = null;
                    Debug.Log("You are too broke");
                }
                else if (ThisItemRequiresSecondStory())
                {
                    if (BarStatsHandler.GetComponent<BarStatsHandler>().secondStory == true)
                    {
                        objectCurrentMaterialHolder = currentPlaceableObject.GetComponent<Renderer>().material;
                        currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                    }
                    else
                    {
                        Destroy(currentPlaceableObject);
                        currentPlaceableObject = null;
                        Debug.Log("Get a second story yo");
                    }
                }
                else
                {
                    objectCurrentMaterialHolder = currentPlaceableObject.GetComponent<Renderer>().material;
                    currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                  
                }

            }

        }
        if (Input.GetMouseButtonDown(0) && editModeOn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (deleteModeOn)
                {
                    currentPlaceableObject = hitInfo.transform.gameObject;
                    currentPlaceableObject.layer = 2;
                    currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                    Destroy(currentPlaceableObject);
                }
                else
                {
                    if (CanEditItem(hitInfo.transform.gameObject.tag) == false)
                    {
                        Debug.Log("Item Can't be Edited");
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
        if (currentPlaceableObject.GetComponent<ItemProperties>().goldCost > BarStatsHandler.GetComponent<BarStatsHandler>().totalGold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool ThisItemRequiresSecondStory()
    {
        if (currentPlaceableObject.GetComponent<ItemProperties>().secondStoryRequired == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanEditItem (string objectTag)
    {
        if(objectTag != "Floor Object" && objectTag != "Wall Object" && objectTag != "Ceiling Object")
        {
            Debug.Log(objectTag);
            return false;
        }
        else
        {
            return true;
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
            //Debug.Log(hitInfo.transform.gameObject.tag);
            if(hitInfo.transform.gameObject.tag == "Floor" && currentPlaceableObject.tag == "Floor Object")
            {
                Debug.Log("I can place a floor object here");
                onPlaceableSurface = true;
                currentPlaceableObject.GetComponent<Renderer>().material = correctPlacementMaterial;
                
            }
            else if (hitInfo.transform.gameObject.tag == "Floor" && currentPlaceableObject.tag == "Table")
            {
                Debug.Log("I can place a floor object here");
                onPlaceableSurface = true;
                currentPlaceableObject.GetComponent<Renderer>().material = correctPlacementMaterial;

            }
            else if (hitInfo.transform.gameObject.tag == "Table" && currentPlaceableObject.tag == "Table Object")
            {
                Debug.Log("I can place a floor object here");
                onPlaceableSurface = true;
                currentPlaceableObject.GetComponent<Renderer>().material = correctPlacementMaterial;

            }
            else if (hitInfo.transform.gameObject.tag == "Ceiling" && currentPlaceableObject.tag == "Ceiling Object")
            {
                Debug.Log("I can place a ceiling object here");
                onPlaceableSurface = true;
                currentPlaceableObject.GetComponent<Renderer>().material = correctPlacementMaterial;
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.down, (hitInfo.normal));
                

            }
            else if (hitInfo.transform.gameObject.tag == "Wall" && currentPlaceableObject.tag == "Wall Object")
            {
                Debug.Log("I can place a wall object here");
                onPlaceableSurface = true;
                currentPlaceableObject.GetComponent<Renderer>().material = correctPlacementMaterial;
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward, (hitInfo.normal));
                if (hitInfo.normal.z == -1)
                {
                    Vector3 newdir = new Vector3(0,-1,-1);
                    currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward,Vector3.up*0);
                    currentPlaceableObject.transform.Rotate(Vector3.up, 180);

                }
                // currentPlaceableObject.transform.rotation = Quaternion.LookRotation(hitInfo.point, Vector3.up);
            }
            else
            {
                Debug.Log("I can't place this object here");
                onPlaceableSurface = false;
                currentPlaceableObject.GetComponent<Renderer>().material = incorrectPlacementMaterial;
            }
        }
    }
    

    //this function allows the player to rotate objects using the mouse wheel
    IEnumerator RotateFromMouseWheel()
    {
        yield return new WaitForSeconds(1);
        //Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    //this function handles placing the object when the player clicks
    IEnumerator ReleaseIfClicked()
    {
        yield return new WaitForSeconds(1);
        if (Input.GetMouseButtonDown(0) && onPlaceableSurface == true)
        {
            if(editModeOn == false)
            {
                BarStatsHandler.GetComponent<BarStatsHandler>().totalGold -= currentPlaceableObject.GetComponent<ItemProperties>().goldCost;
                BarStatsHandler.GetComponent<BarStatsHandler>().totalBarAttractiveness += currentPlaceableObject.GetComponent<ItemProperties>().barAttraction;
            }
            currentPlaceableObject.layer = 0;
            currentPlaceableObject.GetComponent<Renderer>().material = objectCurrentMaterialHolder;
            if(currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Second Story")
            {
                Vector3 ceilingPosition = secondStoryCeiling.transform.position;
                ceilingPosition.y += 5;
                BarStatsHandler.GetComponent<BarStatsHandler>().secondStory = true;
                secondStoryCeiling.transform.position = ceilingPosition;
                currentPlaceableObject.GetComponent<RoomWarp>().enabled = true;
            }
            if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Enchanting Room")
            {
                currentPlaceableObject.GetComponent<RoomWarp>().enabled = true;
            }
            if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Planning Room")
            {
               
                currentPlaceableObject.GetComponent<RoomWarp>().enabled = true;
            }
            if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Kitchen")
            {

                currentPlaceableObject.GetComponent<RoomWarp>().enabled = true;
            }
            if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Bedroom")
            {

                currentPlaceableObject.GetComponent<RoomWarp>().enabled = true;
            }
            currentPlaceableObject = null;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced = true;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();
           
            //Debug.Log(BarStatsHandler.GetComponent<BarStatsHandler>().totalGold);
            editModeOn = false;
        }
       
             
    }
}
