using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIHandler : MonoBehaviour
{
    public PointOfInterest currentArea {  get; private set; }
    public PointOfInterest destinationArea {  get; private set; }

    public List<PointOfInterest> knownAreas;

    private EventHandler eventHandler;

    private void Awake()
    {
        eventHandler = GetComponent<EventHandler>();
        eventHandler.UpdateCurrentAreaEvent += UpdateCurrentArea;
        eventHandler.UpdateDestinationEvent += UpdateDestination;
    }

    private void UpdateDestination(PointOfInterest interest)
    {
        destinationArea = interest;
    }

    private void UpdateCurrentArea(PointOfInterest poi)
    {
        currentArea = poi;

        if (poi != null)
        {
            eventHandler.UpdateDestination(null);

            if (!knownAreas.Contains(poi))
                knownAreas.Add(poi);
        }
    }
}
