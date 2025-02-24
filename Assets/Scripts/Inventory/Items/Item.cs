using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject itemData;
    public int amount;

    private void OnTriggerEnter(Collider other)
    {
        Inventory c = other.GetComponentInParent<Inventory>();
        Collect(c);
    }
    public void Collect(Inventory container)
    {
        if (container.AddItem(this, amount))
            Destroy(gameObject);
        else
            Debug.Log("Inventory is full");
    }
}
