using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemDrop : MonoBehaviour
{
    public static Action<ItemDrop> OnLandEvent;
    public static Action<ItemSO> OnConsumeEvent;

    public InventoryUI.InventoryCategory Category;
    public SpriteRenderer ItemRender;
    
    int health = 3;
    ItemSO template;

    public void OnLand()
    {
        OnLandEvent?.Invoke(this);
        GetComponent<Collider2D>().enabled = true;
        Transform root = transform.parent;
        transform.parent = null;
        Destroy(root.gameObject);
    }

    public void Initiate(ItemSO _template)
    {
        template = _template;
        Category = _template.Category;
        ItemRender.sprite = _template.Icon;
    }

    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
            if (Category == InventoryUI.InventoryCategory.Food)
                OnConsumeEvent?.Invoke(template);
        }
    }
}
