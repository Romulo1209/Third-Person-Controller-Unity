using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] Image SlotImage;

    public event Action<Item> OnRightClickEvent;

    private Item _item;
    public Item Item {
        get { return _item; }
        set {
            _item = value;

            if(Item == null) {
                SlotImage.enabled = false;
            }
            else {
                SlotImage.sprite = _item.ItemIcon;
                SlotImage.preserveAspect = true;
                SlotImage.enabled = true;
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (SlotImage == null)
            SlotImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null) {
                OnRightClickEvent(Item);
            }
        }
    }
}
