using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerDownHandler
{
    public bool generator; // if true, makes a copy of the object instead of moving the original
    private bool selected = false;

    private void AddPhysicsRaycaster()
    {
        Physics2DRaycaster raycast = FindObjectOfType<Physics2DRaycaster>();
        if (raycast == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    private void GenerateClone()
    {
        GameObject clone = Object.Instantiate(gameObject, transform.position, Quaternion.identity);
        
        Clickable script = clone.GetComponent<Clickable>();
        if (script == null) { Debug.LogError("Generator generated an object without script."); }
        // if it's copy of object with this script, should have this script

        script.generator = false;
        script.selected = true;
    }
    
    // FOR THE OnPointerDown FUNCTION TO WORK ;
    // There must be an EventSystems component somewhere in the project
    //      (does not have to be on the thing with the click script)
    // On this EventSystems object, add in a "StandaloneInputModule"
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject target = eventData.pointerCurrentRaycast.gameObject;
        if (target != gameObject) { return; }

        if (selected)
        {
            selected = false;
        }
        else if (generator)
        {
            GenerateClone();
        }
        else
        {
            selected = true;
        }
    }

    void Start()
    {
        Debug.Log("Mounted");
        AddPhysicsRaycaster();
    }

    void Update()
    {
        if (!selected) { return; }

        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = mouse_pos;
    }
}
