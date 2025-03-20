using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POIType { City, MilitaryBase, Forest, Mountain}
public class PointOfInterest : MonoBehaviour
{
    public string Name;
    public POIType type;
    public int dangerLevel;

    private void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if(collider == null)
            return;

        Gizmos.color = Color.magenta;

        Gizmos.DrawWireCube(transform.position + collider.center, collider.size);
    }

    private void OnTriggerEnter(Collider other)
    {
        EventHandler eventHandler = other.GetComponentInParent<EventHandler>();

        if (eventHandler != null)
            eventHandler.UpdateCurrentArea(this);
    }
    private void OnTriggerExit(Collider other)
    {
        EventHandler eventHandler = other.GetComponentInParent<EventHandler>();

        if (eventHandler != null)
            eventHandler.UpdateCurrentArea(null);
    }
}
