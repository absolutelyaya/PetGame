using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardKey : MonoBehaviour
{
    TextMeshProUGUI keyName;
    Keyboard parent;

    private void Start()
    {
        parent = GetComponentInParent<Keyboard>();
        keyName = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnKeyPressed()
    {
        parent.OnKeyPressed(char.Parse(keyName.text));
    }
}
