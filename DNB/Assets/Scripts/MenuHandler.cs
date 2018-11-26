using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuHandler : MonoBehaviour {

    public GameObject areaDecoratorUI;
    bool visibleUI = false;
    public GameObject playerObject;
    public string currentPlaceableObjectName;

    // Update is called once per frame
    void Update()
    {
        MenuToggle();
    }

    void MenuToggle()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (visibleUI == false)
            {
                visibleUI = true;
                Cursor.visible = true;
                areaDecoratorUI.SetActive(true);
                playerObject.GetComponent<vp_FPInput>().MouseCursorForced = true;
            }
            else
            {
                playerObject.GetComponent<vp_FPInput>().MouseCursorForced = false;
                areaDecoratorUI.SetActive(false);
                Cursor.visible = false;
                visibleUI = false;
            }
        }
    }
    public void ObjectButtonPressed()
    {
        currentPlaceableObjectName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(currentPlaceableObjectName);
    }
}
