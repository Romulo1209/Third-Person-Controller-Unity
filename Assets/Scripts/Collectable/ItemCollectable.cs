using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollectable : Interactable
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;

    private void OnValidate()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();
    }
    public override void Interact()
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }
}
