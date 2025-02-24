using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType { Food, Water, Resource, Ammunition, Equipment, Weapon}

[CreateAssetMenu(fileName = "New Empty Item", menuName ="Inventory System/Items/Empty")]
public class ItemObject : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public GameObject itemPrefab;
    public bool isUnique;
    public ItemType type;

    public void DropItem()
    {

    }
    public virtual void UseItem()
    {

    }
}
