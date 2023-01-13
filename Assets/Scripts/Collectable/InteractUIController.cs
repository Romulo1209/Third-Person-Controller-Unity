using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractUIController : MonoBehaviour
{
    [SerializeField] GameObject interactButton;
    [SerializeField] TMP_Text interactText;

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
