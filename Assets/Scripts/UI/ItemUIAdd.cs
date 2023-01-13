using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUIAdd : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] TextMeshProUGUI ItemAddText;

    public void SetItem(Item item)
    {
        ItemImage.sprite = item.ItemIcon;
        ItemAddText.text = item.ItemName + " Added";
    }

    public void EndAnim() {
        Destroy(gameObject);
    }
}
