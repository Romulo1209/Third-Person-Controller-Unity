using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBilboard : MonoBehaviour
{
    [SerializeField] bool show = false;
    [SerializeField] Transform cameraPosition;
    [SerializeField] Image lifeImage;
    [SerializeField] float lifeFloat;
    [SerializeField] Animator animator;
    public float LifeFloat { set { lifeFloat = value; } }

    private void OnValidate() {
        Setup();
    }
    private void Awake() {
        Setup();
        Hide();
    }
    void Setup() {
        if (cameraPosition == null)
            cameraPosition = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    private void Update() {
        if (show) {
            transform.LookAt(cameraPosition);
            lifeImage.fillAmount = lifeFloat;
        } 
    }

    public void Show()
    {
        show = true;
        animator.SetTrigger("Switch");
        animator.SetBool("State", show);
    }
    public void Hide()
    {
        show = false;
        animator.SetTrigger("Switch");
        animator.SetBool("State", show);
    }
}
