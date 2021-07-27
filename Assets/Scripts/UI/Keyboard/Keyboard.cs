using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using System;
using TMPro;

public class Keyboard : MonoBehaviour
{
    public static Keyboard Reference;

    public TextMeshProUGUI Question;
    public TextMeshProUGUI Output;
    public int MaxLength;
    public event Action<string> ConfirmEvent;

    int curChar;
    string key;

    private void Start()
    {
        if (Reference)
            Destroy(gameObject);
        else
            Reference = this;
        Output.text = new string('_', MaxLength);
    }

    public void OnKeyPressed(char key)
    {
        switch (key)
        {
            case 'ö':
                Confirm();
                break;
            case 'ä':
                DeleteChar();
                break;
            default:
                TypeChar(key);
                break;
        }
    }

    void TypeChar(char c)
    {
        if(curChar < MaxLength)
        {
            var sb = new StringBuilder(Output.text);
            sb.Remove(curChar, 1);
            sb.Insert(curChar, c);
            Output.text = sb.ToString();
            curChar++;
        }
    }

    void DeleteChar()
    {
        if (curChar > 0)
        {
            var sb = new StringBuilder(Output.text);
            sb.Remove(curChar - 1, 1);
            sb.Insert(curChar - 1, '_');
            Output.text = sb.ToString();
            curChar--;
        }
    }

    void Confirm()
    {
        string output = Output.text.TrimEnd('_');
        if (output.Length > 0)
        {
            ConfirmEvent?.Invoke(key + "|" + output);
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Keyboard"));
        }
    }

    public void SetQuestion(string question)
    {
        Question.text = string.Empty;
        var tt = new TextTyper(Question, 16);
        key = question.Split('|')[0];
        tt.SetText(question.Split('|')[1] + '\u23F9');
    }
}
