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
}
