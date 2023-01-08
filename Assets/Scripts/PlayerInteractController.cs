using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    [SerializeField] float InteractRange = 2f;

    [SerializeField] GameObject InteractUI;
    [SerializeField] LayerMask InteratLayer;
    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, InteractRange, InteratLayer);
        if(colliders.Length != 0) {
            InteractUI.SetActive(true);
        }
        else {
            InteractUI.SetActive(false);
        }
    }
}
