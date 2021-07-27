using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTab : MonoBehaviour
{
    public bool Selected
    {
        get { return selected; }
        set
        {
            if(img != null)
                img.sprite = value ? SelectedSprite : UnselectedSprite;
            selected = value;
        }
    }
    public InventoryUI.InventoryCategory Category;
    public Sprite SelectedSprite;
    public Sprite UnselectedSprite;

    private InventoryUI owner;
    private bool selected;
    private Image img;
        
    public void Start()
    {
        img = GetComponent<Image>();
        owner = GetComponentInParent<InventoryUI>();
        owner.ChangeCategoryEvent += (c) => Selected = c == Category;
    }

    public void OnPressed()
    {
        owner.SelectCategory(Category);
    }
}