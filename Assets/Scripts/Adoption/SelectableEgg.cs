using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableEgg : MonoBehaviour
{
    public Vector2 DesiredPos;
    public EggSelectionMenu Owner;
    public EggSO Egg;

    SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.sharedMaterial = new Material(render.sharedMaterial);
        if (Egg)
            render.sprite = Egg.Phases[0].Sprite;
        DesiredPos = transform.localPosition;
    }

    private void Update()
    {
        if (Vector2.Distance(DesiredPos, transform.localPosition) > 0.01)
            transform.localPosition = Vector2.Lerp(transform.localPosition, DesiredPos, Time.deltaTime * 7.5f);
    }

    private void OnMouseEnter()
    {
        if (Owner.Choice == null)
        {
            Owner.UpdateInfoText(Egg.RetrievalText);
            DesiredPos += new Vector2(0, 0.3f);
        }
    }

    private void OnMouseExit()
    {
        if(Owner.Choice == null)
        {
            Owner.UpdateInfoText(string.Empty);
            DesiredPos -= new Vector2(0, 0.3f);
        }
    }

    private void OnMouseDown()
    {
        if (Owner.Choice == null)
        {
            Owner.Choice = this;
            Owner.HideOtherEgg(this);
            DesiredPos = Vector2.zero + Vector2.down * 0.5f;
            StartCoroutine(Reveal());
        }
    }

    IEnumerator Reveal()
    {
        Owner.UpdateInfoText(string.Empty);
        float time = -1.25f;
        while(time < 4)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            render.sharedMaterial.SetFloat("_Visibility", time);
        }
        Owner.UpdateInfoText(Egg.Description);
    }
}
