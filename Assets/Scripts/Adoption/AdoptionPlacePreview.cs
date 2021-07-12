using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdoptionPlacePreview : MonoBehaviour
{
    SpriteRenderer render;
    Coroutine showRoutine;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    public void ShowPreview(Sprite sprite)
    {
        if (showRoutine != null)
            StopCoroutine(showRoutine);
        showRoutine = StartCoroutine(ShowPreviewRoutine(sprite));
    }

    IEnumerator ShowPreviewRoutine(Sprite sprite)
    {
        render.sharedMaterial.SetFloat("_Visibility", 0f);
        yield return new WaitForEndOfFrame();
        render.sprite = sprite;
        float visibility = -1f;
        while(visibility < 3)
        {
            visibility += Time.deltaTime * 3;
            render.sharedMaterial.SetFloat("_Visibility", visibility);
            yield return new WaitForEndOfFrame();
        }
        showRoutine = null;
    }

    private void OnDestroy()
    {
        render.sharedMaterial.SetFloat("_Visibility", -1);
    }
}
