using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
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

    string infoText = string.Empty;

    private void Start()
    {
        StartCoroutine(UpdateInfoText());
        if(AdoptionPlaceMenu.Reference != null)
            Place = AdoptionPlaceMenu.Reference.GetAdoptionPlace();

        int rolls = 0;
        List<int> picks = new List<int>();
        while(picks.Count < 2)
        {
            int r = Random.Range(0, Place.EggPool.Count);
            if (!picks.Contains(r) || rolls > 5)
                picks.Add(r);
            rolls++;
        }
        LeftEgg.Egg = Place.EggPool[picks[0]];
        RightEgg.Egg = Place.EggPool[picks[1]];
    }

    public void UpdateInfoText(string text)
    {
        infoText = text;
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

    IEnumerator UpdateInfoText()
    {
        bool finished = false;
        string curTask = string.Empty;
        bool UpdatePending = false;
        while (!finished)
        {
            yield return new WaitForEndOfFrame();
            if(curTask != infoText)
            {
                UpdatePending = true;
                curTask = infoText;
            }
            if(curTask == string.Empty && InfoText.text.Length > 0 && UpdatePending)
            {
                while (InfoText.text.Length > 0)
                {
                    yield return new WaitForSeconds(1 / (TypeSpeed * 2));
                    if(InfoText.text.EndsWith("<br>"))
                        InfoText.text = InfoText.text.Substring(0, InfoText.text.Length - 4);
                    else
                        InfoText.text = InfoText.text.Substring(0, InfoText.text.Length - 1);
                }
                UpdatePending = false;
            }
            else if(UpdatePending)
            {
                foreach (var c in curTask)
                {
                    yield return new WaitForSeconds(1 / TypeSpeed);
                    switch(c)
                    {
                        case '☑':
                            StartCoroutine(NamingRoutine());
                            break;
                        case '⏹':
                            finished = true;
                            break;
                        case '∛':
                            InfoText.text += "<br>";
                            break;
                        case '㊡':
                            yield return new WaitForSeconds(1f);
                            break;
                        case '.':
                            InfoText.text += c;
                            yield return new WaitForSeconds(0.2f);
                            break;
                        default:
                            InfoText.text += c;
                            break;
                    }
                    if (infoText == string.Empty)
                        break;
                }
                UpdatePending = false;
            }
        }
        Debug.Log("Transition to breeding grounds");
    }

    IEnumerator NamingRoutine()
    {
        yield return SceneManager.LoadSceneAsync("Keyboard", LoadSceneMode.Additive);
        while (!Keyboard.Reference)
            yield return new WaitForEndOfFrame();
        Keyboard.Reference.ConfirmEvent += ConfirmName;
        infoText = string.Empty;
    }

    void ConfirmName(string name)
    {
        infoText = "Great!㊡∛Time to welcome " + name + " into your home.㊡㊡⏹";
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
