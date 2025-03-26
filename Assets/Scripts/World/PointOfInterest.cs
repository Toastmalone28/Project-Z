using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POIType { City, MilitaryBase, Forest, Mountain, None}
public class PointOfInterest : MonoBehaviour
{
    public string Name;
    public POIType type;
    public int dangerLevel;
    public Vector3 areaSize {  get; private set; }
    public Vector3 areaCenter { get; private set; }

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();

        areaSize = boxCollider.size;
        areaCenter = boxCollider.center;
    }

    private void OnDrawGizmos()
    {
        if(GetComponent<BoxCollider>() == null)
            return;

        Gizmos.color = Color.magenta;

        Gizmos.DrawWireCube(transform.position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
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
