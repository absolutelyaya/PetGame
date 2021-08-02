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
    [Header("Other")]
    public GameObject ItemPrefab;

    InventoryCategory curCategory = InventoryCategory.None;

    private void Start()
    {
        GameManager.Reference.Inventory.InventoryUpdateEvent += UpdateInventory;
    }

    public void SelectCategory(InventoryCategory category)
    {
        InspectSlot(null);
        if (category != curCategory)
            curCategory = category;
        else
            curCategory = InventoryCategory.None;
        ChangeCategoryEvent?.Invoke(curCategory);

        UpdateInventory();
    }

    public void UpdateInventory()
    {
        List<ItemStack> Items = GameManager.Reference.Inventory.GetItems(curCategory);
        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].SetItem(i < Items.Count ? Items[i] : null);
        }
        ActionLabel.text = curCategory switch
        {
            InventoryCategory.Food => "feed",
            InventoryCategory.Placeholder1 => "???",
            InventoryCategory.Placeholder2 => "???",
            _ => ""
        };
        InspectSlot(InspectorSlot);
    }

    public void InspectSlot(ItemSlot slot)
    {
        if(slot != InspectorSlot)
            InspectSlotEvent?.Invoke(slot);

        if(slot == null || slot.GetItemStack() == null)
        {
            ItemName.text = string.Empty;
            ItemDesc.text = string.Empty;
            ItemCount.text = string.Empty;
            InspectorSlot.SetItem(null);
        }
        else
        {
            if (slot.GetItemStack().Count <= 0)
            {
                slot.SetItem(null);
                InspectSlotEvent?.Invoke(null);
                InspectSlot(null);
                return;
            }
            ItemName.text = slot.GetItemStack().Template.name;
            ItemDesc.text = slot.GetItemStack().Template.Description;
            ItemCount.text = $"X{slot.GetItemStack().Count}";
            InspectorSlot.SetItem(slot.GetItemStack());
        }
        ActionButton.interactable = slot != null;
    }

    public void OnAction()
    {
        var template = InspectorSlot.GetItemStack().Template;
        if (GameManager.Reference.Inventory.RemoveItem(template.name, 1))
        {
            switch (curCategory)
            {
                case InventoryCategory.Food:
                    ///TODO: Feed
                    break;
                case InventoryCategory.Placeholder1:
                    break;
                case InventoryCategory.Placeholder2:
                    break;
                default:
                    break;
            }
            ThrowItem(template);
        }
    }

    public void ThrowItem(ItemSO template)
    {
        var item = Instantiate(ItemPrefab, new Vector2(UnityEngine.Random.Range(-5f, 5f) + 5.375f, UnityEngine.Random.Range(-2f, 2f)), Quaternion.identity).GetComponentInChildren<ItemDrop>();
        item.Initiate(template);
    }

    public enum InventoryCategory
    {
        None,
        Food,
        Placeholder1,
        Placeholder2
    }
}
