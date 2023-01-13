using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public static string _ItemName { get { return _ItemName; } }

    public string ItemName;
    public Sprite ItemIcon;
}
