using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class AdoptionPlaceMenu : MonoBehaviour
{
    public static AdoptionPlaceMenu Reference;

    public GameObject ButtonPrefab;
    public AdoptionPlacePreview Preview;

    AdoptionPlaceSO chosenPlace;

    private void Start()
    {
        Reference = this;
        DontDestroyOnLoad(this);
        string assetBundleDirectory = "Assets/AssetBundles";
        var places = AssetBundle.LoadFromFile($"{assetBundleDirectory}/adoptionplaces");
        int buttonIndex = 0;
        foreach (var name in places.GetAllAssetNames())
        {
            var asset = places.LoadAsset(name);
            if (asset is AdoptionPlaceSO a)
            {
                var newButton = Instantiate(ButtonPrefab, transform.position - new Vector3(0, buttonIndex * -1.125f), Quaternion.identity, transform).GetComponent<AdoptionPlaceButton>();
                newButton.Render.sprite = a.ButtonSprite;
                newButton.Place = a;
                buttonIndex++;
            }
        }
    }

    public void OnButtonPressed(AdoptionPlaceSO place)
    {
        if(chosenPlace == null)
        {
            chosenPlace = place;
            StartCoroutine(ExitRoutine());
        }
    }

    IEnumerator ExitRoutine()
    {
        float time = 0;
        while (time < 0.5f)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            transform.position += Vector3.left * 6 * Time.deltaTime;
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EggSelection");
    }

    public AdoptionPlaceSO GetAdoptionPlace()
    {
        Destroy(gameObject);
        return chosenPlace;
    }
}
