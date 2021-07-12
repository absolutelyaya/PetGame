using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdoptionPlaceButton : MonoBehaviour
{
    public SpriteRenderer Render;
    public AdoptionPlaceSO Place;

    bool selected;
    bool clicked;
    float desiredX;

    private void OnMouseEnter()
    {
        if(!selected)
        {
            selected = true;
            desiredX = 1f;
            AdoptionPlaceMenu.Reference.Preview.ShowPreview(Place.Preview);
        }
    }

    private void OnMouseExit()
    {
        selected = false;
        if(!clicked)
            desiredX = 0f;
    }

    private void OnMouseDown()
    {
        clicked = true;
        transform.SetParent(new GameObject().transform);
        desiredX = -1.375f;
        AdoptionPlaceMenu.Reference.OnButtonPressed(Place);
    }

    private void Update()
    {
        if (Mathf.Abs(transform.localPosition.x - desiredX) > 0.01f)
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, desiredX, Time.deltaTime * 4), transform.localPosition.y);
    }
}
