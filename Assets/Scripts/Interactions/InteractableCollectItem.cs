using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableCollectItem : Interactable
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;

    private void Awake()
    {
        GetReferences();
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        GetReferences();
    }
#endif

    void GetReferences()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Character>().InventoryGet;
    }
    public override void Interact()
    {
        InteractUIController.instance.AddItemToInventory(item);
        inventory.AddItem(item.GetCopy());
        Destroy(gameObject);
    }
}
