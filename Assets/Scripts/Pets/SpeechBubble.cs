using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    Canvas canvas;
    TextMeshProUGUI text;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        canvas.enabled = text.text.Length > 0;
    }
}
