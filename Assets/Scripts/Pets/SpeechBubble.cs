using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(0)]
public class SpeechBubble : MonoBehaviour
{
    Canvas canvas;
    TextMeshProUGUI text;
    TextTyper tt;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void CreateTextTyper(int typeSpeed)
    {
        if (tt != null)
            tt.Destroy();
        if (text != null)
            tt = new TextTyper(text, typeSpeed);
    }

    private void Update()
    {
        canvas.enabled = text.text.Length > 0;
    }

    public void SetText(Message message)
    {
        tt.SetText(message.Text, message.Duration);
    }

    public void SetFont(TMP_FontAsset font)
    {
        text.font = font;
    }
}
