using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { Wood, Stone, Metal}
[CreateAssetMenu(fileName = "New Empty Resource Item", menuName = "Inventory System/Items/Resource")]
public class Resource : ItemObject
{
    public ResourceType ResourceType;
}
