using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Empty Item", menuName ="Inventory System/Items/Empty")]
public class ItemObject : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public bool isUnique;
}
