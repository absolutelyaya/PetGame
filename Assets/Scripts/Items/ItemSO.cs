using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Pets/Item")]
public class ItemSO : ScriptableObject
{
    public ItemType Type;
    public Sprite Icon;
    [Multiline(4)]
    public string Description;

    public enum ItemType
    {
        FOOD
    }
}
