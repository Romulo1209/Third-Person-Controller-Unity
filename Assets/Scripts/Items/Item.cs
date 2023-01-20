using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ItemName;
    public Sprite ItemIcon;
    [Range(1, 999)] public int maximumStack = 1;

    public static string _ItemName { get { return _ItemName; } }
    public string ID { get { return id; } }

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }

    public virtual Item GetCopy()
    {
        return this;
    }
    public virtual void Destroy()
    {

    }
}
