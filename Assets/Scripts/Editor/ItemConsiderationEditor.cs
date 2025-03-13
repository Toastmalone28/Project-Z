using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemInInventoryConsideration))]
public class ItemConsiderationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemInInventoryConsideration itemInInventoryConsideration = (ItemInInventoryConsideration)target;

        itemInInventoryConsideration.itemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", itemInInventoryConsideration.itemType);

        switch (itemInInventoryConsideration.itemType)
        {
            case ItemType.Ammunition:
                itemInInventoryConsideration.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Ammunition Type", itemInInventoryConsideration.weaponType);
                break;
            case ItemType.Equipment:
                itemInInventoryConsideration.equipmentType = (EquipmentType)EditorGUILayout.EnumPopup("Equipment Type", itemInInventoryConsideration.equipmentType);
                break;
            case ItemType.Resource:
                itemInInventoryConsideration.resourceType = (ResourceType)EditorGUILayout.EnumPopup("Resource Type", itemInInventoryConsideration.resourceType);
                break;
            case ItemType.Consumable:
                itemInInventoryConsideration.consumableType = (ConsumableType)EditorGUILayout.EnumPopup("Consumable Type", itemInInventoryConsideration.consumableType);
                break;
            case ItemType.Weapon:
                itemInInventoryConsideration.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type", itemInInventoryConsideration.weaponType);
                break;

        }
        if(GUI.changed)
            EditorUtility.SetDirty(itemInInventoryConsideration);
    }
}
