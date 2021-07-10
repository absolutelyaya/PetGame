using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(-1)]
public class EggSelectionMenu : MonoBehaviour
{
    public TextMeshProUGUI InfoText;
    public AdoptionPlaceSO Place;
    public SelectableEgg LeftEgg;
    public SelectableEgg RightEgg;

    public void UpdateInfoText(string text)
    {
        InfoText.text = text;
    }

    private void Start()
    {
        if(AdoptionPlaceMenu.Reference != null)
            Place = AdoptionPlaceMenu.Reference.GetAdoptionPlace();

        List<int> picks = new List<int>();
        while(picks.Count < 2)
        {
            int r = Random.Range(0, Place.EggPool.Count);
            if (!picks.Contains(r))
                picks.Add(r);
        }
        LeftEgg.Egg = Place.EggPool[picks[0]];
        RightEgg.Egg = Place.EggPool[picks[1]];
    }
}
