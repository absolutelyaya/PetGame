using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Action<InventoryCategory> ChangeCategoryEvent;
    public Action<ItemSlot> InspectSlotEvent;
    public List<ItemSlot> Slots = new List<ItemSlot>();
    [Header("Inspector")]
    public ItemSlot InspectorSlot;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDesc;
    public TextMeshProUGUI ItemCount;
    public Button ActionButton;
    public TextMeshProUGUI ActionLabel;

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
        ActionLabel.text = category switch
        {
            InventoryCategory.Food => "feed",
            InventoryCategory.Placeholder1 => "???",
            InventoryCategory.Placeholder2 => "???",
            _ => ""
        };
    }

    public void InspectItemStack(ItemSlot slot)
    {
        InspectSlotEvent?.Invoke(slot);

        if(slot == null)
        {
            ItemName.text = string.Empty;
            ItemDesc.text = string.Empty;
            ItemCount.text = string.Empty;
            InspectorSlot.SetItem(null);
        }
        else
        {
            ItemName.text = slot.GetItemStack().Template.name;
            ItemDesc.text = slot.GetItemStack().Template.Description;
            ItemCount.text = $"X{slot.GetItemStack().Count}";
            InspectorSlot.SetItem(slot.GetItemStack());
        }
        ActionButton.enabled = slot != null;
    }

    public enum InventoryCategory
    {
        None,
        Food,
        Placeholder1,
        Placeholder2
    }
}
