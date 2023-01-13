using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    public bool debugInteraction;
    [SerializeField] float InteractRange = 2f;
    [SerializeField] LayerMask InteratLayer;

    [SerializeField] InteractUIController interactUI;
    [SerializeField] PlayerInputController inputController;

    private void OnValidate() {
        if (interactUI == null)
            interactUI = FindObjectOfType<InteractUIController>();
    }

    private void FixedUpdate()
    {
        var itemReturn = GetItemColletable();
        if (itemReturn != null) {
            interactUI.Show(itemReturn);

            ItemCollectable item = itemReturn as ItemCollectable;
            if (item != null) {
                inputController.InteractEvent.RemoveAllListeners();
                inputController.InteractEvent.AddListener(item.Interact);
            }
        }
        else {
            interactUI.Hide();
        }
    }

    public Interactable GetItemColletable()
    {
        List<Interactable> interactList = new List<Interactable>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, InteractRange, InteratLayer);
        foreach(Collider collider in colliders) {
            if(collider.TryGetComponent(out Interactable collectable)) {
                interactList.Add(collectable);
            }
        }

        Interactable closestInteractable = null;
        foreach(Interactable interactable in interactList)
        {
            if(closestInteractable == null) {
                closestInteractable = interactable;
            }
            else {
                if(Vector3.Distance(transform.position, interactable.transform.position) < Vector3.Distance(transform.position, closestInteractable.transform.position)) {
                    closestInteractable = interactable;
                }
            }
        }
        return closestInteractable;
    }

    #region Debug

    void OnDrawGizmosSelected()
    {
        if (debugInteraction) {
            Gizmos.color = new Color(100, 100, 100, 100);
            Gizmos.DrawSphere(transform.position, InteractRange);
        }
    }

    #endregion[
}
