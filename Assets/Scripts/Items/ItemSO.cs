using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Pets/Item")]
public class ItemSO : ScriptableObject
{
    public InventoryUI.InventoryCategory Category;
    public Sprite Icon;
    [Multiline(4)]
    public string Description;
}
