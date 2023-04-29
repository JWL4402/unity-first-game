using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerDownHandler
{
    public bool generator; // if true, makes a copy of the object instead of moving the original
    private List<GameObject> selectedObjects = new List<GameObject>();

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
            if (generator)
            {
                GameObject clone = Object.Instantiate(target, transform.position, Quaternion.identity);

                Click script = clone.GetComponent<Click>(); 
                if (script == null) { Debug.LogError("Generator generated an object without script."); }
                // if it's copy of object with this script, should have this script

                script.generator = false;
                script.selectedObjects.Add(clone);
            }
            else
            {
                selectedObjects.Add(target);
            }
        }
        else if (selectedObjects.Contains(target))
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
        if (selectedObjects.Count == 0) { return; }

        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach (GameObject obj in selectedObjects)
        {
            obj.transform.position = mouse_pos;
        }
    }
}
