using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    public TextMeshProUGUI Output;
    public int MaxLength;

    int curChar;

    private void Start()
    {
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
        string output = Output.text.Replace('_', '\0');
        if(output.Length > 0)
            Debug.Log(output);
    }
}
