using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryUI : MonoBehaviour
{
    public Action<InventoryCategory> ChangeCategoryEvent;

    InventoryCategory curCategory = InventoryCategory.None;

    public void SelectCategory(InventoryCategory category)
    {
        if (category != curCategory)
            curCategory = category;
        else
            curCategory = InventoryCategory.None;
        ChangeCategoryEvent?.Invoke(curCategory);
    }

    public enum InventoryCategory
    {
        None,
        Food
    }
}
