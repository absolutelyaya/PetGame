using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public bool Selected
    {
        get { return selected; }
        set
        {
            frame.sprite = value ? Active : Inactive;
            selected = value;
        }
    }
    public Sprite Active;
    public Sprite Inactive;
    public Image ItemIcon;

    private bool selected;
    private Image frame;
    private InventoryUI owner;
    private ItemStack item;

    private void Start()
    {
        frame = GetComponent<Image>();
        owner = GetComponentInParent<InventoryUI>();
        owner.ChangeCategoryEvent += (c) => Selected = false;
    }

    public void OnPressed()
    {
        if(item != null)
            Selected = !Selected;
        ///TODO: Inspect item
    }

    public void SetItem(ItemStack stack)
    {
        item = stack;
        if (stack != null)
        {
            item = stack;
            ItemIcon.enabled = true;
        }
        else
            ItemIcon.enabled = false;
    }
}
