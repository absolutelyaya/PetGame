using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryUI : MonoBehaviour
{
    public Action<InventoryCategory> ChangeCategoryEvent;
    public List<ItemSlot> Slots = new List<ItemSlot>();

    InventoryCategory curCategory = InventoryCategory.None;

    public void SelectCategory(InventoryCategory category)
    {
        if (category != curCategory)
            curCategory = category;
        else
            curCategory = InventoryCategory.None;
        ChangeCategoryEvent?.Invoke(curCategory);

        List<ItemStack> Items = GameManager.Reference.Inventory.GetItems(curCategory);
        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].SetItem(i < Items.Count ? Items[i] : null);
        }
    }

    public enum InventoryCategory
    {
        None,
        Food,
        Placeholder1,
        Placeholder2
    }
}
