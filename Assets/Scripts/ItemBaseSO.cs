using UnityEngine;

[CreateAssetMenu]
public class ItemBaseSO : ScriptableObject
{
    [field: SerializeField]
    public bool IsStackable { get; set; }
    
    public int ID => GetInstanceID();

    public int MaxStackSize;
    
    public string ItemName;
    public Sprite ItemSprite;
    public string ItemDescription;
}
