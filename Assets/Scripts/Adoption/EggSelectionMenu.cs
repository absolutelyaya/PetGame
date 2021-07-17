using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using TMPro;

[DefaultExecutionOrder(-1)]
public class EggSelectionMenu : MonoBehaviour
{
    public TextMeshProUGUI Header;
    public TextMeshProUGUI InfoText;
    public AdoptionPlaceSO Place;
    public SelectableEgg LeftEgg;
    public SelectableEgg RightEgg;
    public SelectableEgg Choice;
    [Tooltip("Chars per second")]
    public float TypeSpeed = 4;

    TextTyper infotextTT;

    private void Start()
    {
        infotextTT = new TextTyper(InfoText, 20, OnFinishSequence, OnKeyboardOutput);

        if(AdoptionPlaceMenu.Reference != null)
            Place = AdoptionPlaceMenu.Reference.GetAdoptionPlace();

        int rolls = 0;
        List<int> picks = new List<int>();
        while(picks.Count < 2)
        {
            int r = UnityEngine.Random.Range(0, Place.EggPool.Count);
            if (!picks.Contains(r) || rolls > 5)
                picks.Add(r);
            rolls++;
        }
        LeftEgg.Egg = Place.EggPool[picks[0]];
        RightEgg.Egg = Place.EggPool[picks[1]];
    }

    public void UpdateInfoText(string text)
    {
        infotextTT.SetText(text);
    }

    public void HideOtherEgg(SelectableEgg egg)
    {
        if (egg == LeftEgg)
        {
            RightEgg.DesiredPos = RightEgg.transform.position + Vector3.down * 5;
        }
        else
        {
            LeftEgg.DesiredPos = LeftEgg.transform.position + Vector3.down * 5;
        }
        StartCoroutine(DeleteTitle());
    }

    void OnKeyboardOutput(string output)
    {
        string[] args = output.Split('|');
        switch (args[0])
        {
            case "name":
                infotextTT.SetText("Great!㊡∛Time to welcome " + args[1] + " into your home.㊡㊡⏹");
                GameManager.Reference.data.EggName = args[1];
                GameManager.Reference.OwnedEgg = Choice.Egg;
                GameManager.Reference.data.OwnedEggID = Choice.Egg.name;
                GameManager.Reference.data.PreviouslyOwnedEggIDs.Add(Choice.Egg.name);
                GameManager.Reference.data.HatchingSince.dateTime = DateTime.Now;
                break;
            default:
                Debug.Log(output);
                break;
        }
    }

    void OnFinishSequence()
    {
        SceneManager.LoadScene("HatchingGrounds");
    }

    IEnumerator DeleteTitle()
    {
        while(Header.text.Length > 0)
        {
            yield return new WaitForSeconds(1 / TypeSpeed);
            Header.text = Header.text.Substring(0, Header.text.Length - 1);
        }
    }
}
