using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIHandler : MonoBehaviour
{
    public PointOfInterest currentArea;
    public List<PointOfInterest> knownAreas;

    private EventHandler eventHandler;

    private void Awake()
    {
        eventHandler = GetComponent<EventHandler>();
        eventHandler.UpdateCurrentAreaEvent += UpdateCurrentArea;
    }

    private void UpdateCurrentArea(PointOfInterest poi)
    {
        currentArea = poi;

        if (!knownAreas.Contains(poi))
            knownAreas.Add(poi);
    }
}
