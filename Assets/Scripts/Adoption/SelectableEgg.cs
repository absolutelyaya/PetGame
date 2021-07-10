using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableEgg : MonoBehaviour
{
    public EggSelectionMenu Owner;
    public EggSO Egg;

    SpriteRenderer render;

    //TODO: Animate and add functionality.

    private void OnMouseEnter()
    {
        Owner.UpdateInfoText(Egg.RetrievalText);
    }

    private void OnMouseExit()
    {
        Owner.UpdateInfoText(string.Empty);
    }

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        if (Egg)
            render.sprite = Egg.Phases[0].Sprite;
    }
}
