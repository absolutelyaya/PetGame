using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    readonly List<ItemStack> Items = new List<ItemStack>();

    public Inventory(bool debug)
    {
        if (debug)
            foreach (var item in GameManager.Reference.GetSOsOfType(typeof(ItemSO)))
            {
                Items.Add(new ItemStack((ItemSO)item, 69));
            }
    }

    public ItemStack GetStackByName(string name)
    {
        foreach (var stack in Items)
        {
            if (stack.Template.name == name)
                return stack;
        }
        return null;
    }

    public void AddItem(string itemName, int count)
    {
        var stack = GetStackByName(itemName);
        if (stack != null)
            stack.Count += count;
        else
            Items.Add(new ItemStack((ItemSO)GameManager.Reference.GetSOByName<ItemSO>(itemName), count));
    }

    public List<ItemStack> GetItems(InventoryUI.InventoryCategory category)
    {
        List<ItemStack> validItems = new List<ItemStack>();
        if (category == InventoryUI.InventoryCategory.None)
            return validItems;
        foreach (var item in Items)
        {
            if(item.Template.Category == category)
                validItems.Add(item);
        }
        return validItems;
    }

    ///TODO: Inspector Logic
}

public class ItemStack
{
    public ItemSO Template;
    public int Count;

    public ItemStack(ItemSO template, int count)
    {
        Template = template;
        Count = count;
    }
}
