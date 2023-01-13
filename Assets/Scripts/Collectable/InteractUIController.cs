using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractUIController : MonoBehaviour
{
    [SerializeField] GameObject interactButton;
    [SerializeField] TMP_Text interactText;

    [SerializeField] GameObject itemAddPrefab;
    [SerializeField] Transform itemAddTransform;

    public static InteractUIController instance;
    private void Awake()
    {
        instance = this;
    }

    public void AddItemToInventory(Item item)
    {
        GameObject uiItem = Instantiate(itemAddPrefab, itemAddTransform);
        uiItem.GetComponent<ItemUIAdd>().SetItem(item);
    }
    public void Show(Interactable interactable)
    {
        interactButton.SetActive(true);
        interactText.text = interactable.GetInteractText();
    }
    public void Hide()
    {
        interactButton.SetActive(false);
    }
}
