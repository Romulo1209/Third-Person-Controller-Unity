using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] string InteractText;
    public virtual void Interact()
    {
        
    }
    public string GetInteractText()
    {
        return InteractText;
    }
}
