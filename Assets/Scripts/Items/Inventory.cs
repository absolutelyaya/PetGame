using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    readonly List<ItemStack> Items = new List<ItemStack>();

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
