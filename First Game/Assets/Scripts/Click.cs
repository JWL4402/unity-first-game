using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerDownHandler
{
    List<GameObject> selectedObjects = new List<GameObject>();

    private void AddPhysicsRaycaster()
    {
        Physics2DRaycaster raycast = FindObjectOfType<Physics2DRaycaster>();
        if (raycast == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }
    
    // FOR THE OnPointerDown FUNCTION TO WORK ;
    // There must be an EventSystems component somewhere in the project
    //      (does not have to be on the thing with the click script)
    // On this EventSystems object, add in a "StandaloneInputModule"
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject target = eventData.pointerCurrentRaycast.gameObject;

        if (selectedObjects.Count == 0)
        {
            selectedObjects.Add(target);
        }
        else if (selectedObjects.Count >= 1)
        {
            selectedObjects.Remove(target);
        }
    }

    void Start()
    {
        Debug.Log("Mounted");
        AddPhysicsRaycaster();
    }

    void Update()
    {
        Debug.Log(selectedObjects.Count);
        if (selectedObjects.Count == 0) { return; }
        foreach (GameObject obj in selectedObjects)
        {
            //Vector3 mouse_pos_3D = Input.mousePosition;
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            obj.transform.position = mouse_pos;
        }
    }
}
