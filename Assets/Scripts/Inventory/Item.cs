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
        Container c = other.GetComponentInParent<TestPlayerMovement>().playerInventory;
        Collect(c);
    }
    public void Collect(Container container)
    {
        if (container.AddItem(this, amount))
            Destroy(gameObject);
        else
            Debug.Log("Inventory is full");
    }
    public void InstantiateItem()
    {

    }
    public void UseItem()
    {

    }
}
