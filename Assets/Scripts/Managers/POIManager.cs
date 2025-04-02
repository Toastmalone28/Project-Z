using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIManager : MonoBehaviour
{
    public static POIManager instance;
    private List<PointOfInterest> availableAreas;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        InitializeAreaList();
    }

    private void InitializeAreaList()
    {
        availableAreas = new List<PointOfInterest>();

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Area"))
        {
            availableAreas.Add(item.GetComponent<PointOfInterest>());
        }
    }

    public PointOfInterest GetClosestArea(NPCController npc, POIType type)
    {
        PointOfInterest nearestArea = null;
        float distance = Mathf.Infinity;

        foreach (PointOfInterest area in availableAreas) 
        {
            PointOfInterest poi = area.GetComponent<PointOfInterest>();

            if (type != POIType.None)
            {
                if (poi.type != type)
                    continue;
            }
            else
            {
                if(npc.poiHandler.knownAreas.Contains(poi))
                    continue;
            }

            float newDistance = Vector3.Distance(npc.transform.position, area.transform.position);
            if (newDistance < distance)
            {
                distance = newDistance;
                nearestArea = area;
            }                
        }
        return nearestArea;
    }

    public int GetNumberOfUnexploredAreas(NPCController npc)
    {
        int areaCount = 0;

        foreach (PointOfInterest area in availableAreas)
        {
            if (!npc.poiHandler.knownAreas.Contains(area))
                areaCount++;
        }

        return areaCount;
    }
}
