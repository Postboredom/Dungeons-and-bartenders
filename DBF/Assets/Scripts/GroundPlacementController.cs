using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundPlacementController : MonoBehaviour
{

    public GameObject[] placeableObjectPrefabs; //array of objects that could be placed

    private GameObject currentPlaceableObject; //current object that the player is trying to place
    public GameObject currentPlaceableObjectNameHolder;
    public GameObject BarStatsHandler;
    public GameObject refundMenu;
    public Text refundedGoldAmt;

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
            if (deleteModeOn == true)
            {
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();
            }
            else
            {
                StartCoroutine(MoveCurrentObjectToMouse());
                StartCoroutine(RotateFromMouseWheel());
                StartCoroutine(CancelIfClicked());
                StartCoroutine(ReleaseIfClicked());
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
            }

        }
        if (deleteModeOn == true)
        {
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();
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
                else if (UpgradeAlreadyPurchased())
                {
                    Destroy(currentPlaceableObject);
                    currentPlaceableObject = null;
                    Debug.Log("Already got one of those");
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
                    if (CanEditItem(hitInfo.transform.gameObject.tag) == false)
                    {
                        Debug.Log("Item Can't be sold");
                    }
                    else
                    {
                        currentPlaceableObject = hitInfo.transform.gameObject;
                        if (currentPlaceableObject.GetComponent<ItemProperties>().noRefund == true)
                        {
                            Debug.Log("No refunds for this object");
                        }
                        else
                        {
                            refundMenu.SetActive(true);
                            refundedGoldAmt.text = currentPlaceableObject.GetComponent<ItemProperties>().refund.ToString();
                        }

                    }

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
                        objectCurrentMaterialHolder = currentPlaceableObject.GetComponent<Renderer>().material;
                        currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                    }

                }
            }
        }
    }

    public void EditMode()
    {
        editModeOn = !editModeOn;
        if (deleteModeOn == true)
        {
            deleteModeOn = false;
        }
    }

    public void DeleteMode()
    {
        deleteModeOn = !deleteModeOn;
    }

    public void RefundIt()
    {
        BarStatsHandler.GetComponent<BarStatsHandler>().totalGold += currentPlaceableObject.GetComponent<ItemProperties>().refund;
        BarStatsHandler.GetComponent<BarStatsHandler>().totalBarAttractiveness -= currentPlaceableObject.GetComponent<ItemProperties>().barAttraction;
        Destroy(currentPlaceableObject);
        currentPlaceableObject = null;
        refundMenu.SetActive(false);
    }
    public void DontRefundIt()
    {
        currentPlaceableObject = null;
        refundMenu.SetActive(false);
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
    private bool UpgradeAlreadyPurchased()
    {
        if (BarStatsHandler.GetComponent<BarStatsHandler>().secondStory == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Second Story")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().bedroomPurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Bedroom")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().planningRoomPurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Planning Room")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().kitchenPurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Kitchen")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().enchantingRoomPurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Enchanting Room")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().hearthFirePurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Hearthfire")
        {
            return true;
        }
        else
        {
            return false;
        }
    } 
    private void UpgradeInitialPurchase()
    {
        if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Second Story")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().secondStory = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Bedroom")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().bedroomPurchased = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Planning Room")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().planningRoomPurchased = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Kitchen")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().kitchenPurchased = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Enchanting Room")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().enchantingRoomPurchased = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Hearthfire")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().hearthFirePurchased = true;
        }
        
       
    }

    public bool CanEditItem (string objectTag)
    {
        if(objectTag != "Floor Object" && objectTag != "Wall Object" && objectTag != "Ceiling Object" && objectTag !="Table" && objectTag != "Table Object")
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
            if (currentPlaceableObject.GetComponent<RoomWarp>() != null && currentPlaceableObject.GetComponent<SphereCollider>() != null)
            {
                currentPlaceableObject.GetComponent<RoomWarp>().enabled = false;
                currentPlaceableObject.GetComponent<SphereCollider>().enabled = false;
                Debug.Log("yes it should be false");
            }
            if (hitInfo.transform.gameObject.tag == "Floor" && currentPlaceableObject.tag == "Floor Object")
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

    IEnumerator CancelIfClicked()
    {
        yield return new WaitForSeconds(1);
        if(editModeOn == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(currentPlaceableObject);
                currentPlaceableObject = null;
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced = true;
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();
            }
        }
        
    }

    //this function handles placing the object when the player clicks
    IEnumerator ReleaseIfClicked()
    {
        yield return new WaitForSeconds(1);
        if (Input.GetMouseButtonDown(0) && onPlaceableSurface == true)
        {
            if(editModeOn == false)//dont change total gold or bar attraction if in edit mode
            {
                BarStatsHandler.GetComponent<BarStatsHandler>().totalGold -= currentPlaceableObject.GetComponent<ItemProperties>().goldCost;
                BarStatsHandler.GetComponent<BarStatsHandler>().totalBarAttractiveness += currentPlaceableObject.GetComponent<ItemProperties>().barAttraction;
            }
            //set layer to default and give it it's original material
            currentPlaceableObject.layer = 0;
            currentPlaceableObject.GetComponent<Renderer>().material = objectCurrentMaterialHolder;
            //change ceiling height if purchasing the second story
            if(currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Second Story")
            {
                Vector3 ceilingPosition = secondStoryCeiling.transform.position;
                ceilingPosition.y += 5;
                BarStatsHandler.GetComponent<BarStatsHandler>().secondStory = true;
                secondStoryCeiling.transform.position = ceilingPosition;
                currentPlaceableObject.GetComponent<RoomWarp>().enabled = true;
               
            }
            //enable door upgrades to work and mark that they are already purchased
            if(currentPlaceableObject.GetComponent<RoomWarp>() != null && currentPlaceableObject.GetComponent<SphereCollider>() != null)
            {
                currentPlaceableObject.GetComponent<RoomWarp>().enabled = true;
                currentPlaceableObject.GetComponent<SphereCollider>().enabled = true;
              
            }

            UpgradeInitialPurchase();

            currentPlaceableObject = null;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced = true;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();

            if (editModeOn == true)
            {
                //if editing continue editing
            }
            else
            {
                editModeOn = false;
            }
            
        }
       
             
    }
}
