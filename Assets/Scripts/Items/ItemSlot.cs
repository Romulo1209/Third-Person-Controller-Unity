using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler , IBeginDragHandler , IEndDragHandler , IDragHandler , IDropHandler
{
    [SerializeField] Image SlotImage;
    [SerializeField] TMP_Text amountText;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    private Item _item;
    public Item Item {
        get { return _item; }
        set {
            _item = value;

            if(Item == null) {
                SlotImage.color = disabledColor;
            }
            else {
                SlotImage.sprite = _item.ItemIcon;
                SlotImage.color = normalColor;
                SlotImage.preserveAspect = true;
            }
        }
    }

    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set {
            _amount = value;
            amountText.enabled = _item != null && _item.maximumStack > 1;
            if (amountText.enabled) {
                amountText.text = _amount.ToString();
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (SlotImage == null)
            SlotImage = transform.GetChild(0).GetComponent<Image>();
        if (amountText == null)
            amountText = GetComponentInChildren<TMP_Text>();
    }

    public virtual bool CanRecieveItem(Item item)
    {
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null) {
                OnRightClickEvent(this);
            }
        }
    }

    #region Events

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
            OnBeginDragEvent(this);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
            OnEndDragEvent(this);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);
    }

    #endregion
}
