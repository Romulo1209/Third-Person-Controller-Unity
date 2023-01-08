using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] Window FocusWindow;
    [SerializeField] Window[] Windows;

    private void OnValidate()
    {
        if (Windows.Length == 0)
            Windows = GetComponentsInChildren<Window>();
    }

    public virtual void OpenWindow(Window window) {
        foreach (Window win in Windows)
            win.CloseWindow();
        window.OpenWindow();
        FocusWindow = window;
    }


    //Temporario pq nao tem nada do sistema de janelas
    public void OpenGameplay() {
        Time.timeScale = 1;
        OpenWindow(Windows[0]);
    }
    public void OpenPause() {
        Time.timeScale = 0;
        OpenWindow(Windows[2]);
    }
    public void OpenBackpack() {
        OpenWindow(Windows[1]);
    }
}
