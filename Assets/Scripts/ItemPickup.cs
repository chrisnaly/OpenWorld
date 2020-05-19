using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp ()
    {
        Debug.Log("picking up " + item.itemname);
        bool wasPickedUp = Inventory.instance.Add(item);

        //add this item to our inventory
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
