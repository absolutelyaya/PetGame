using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using System;
using TMPro;

[Serializable]
public class GrowthStage
{
    public string ID;
    [HideInInspector]
    public Rect Display;
    [HideInInspector]
    public int Depth;
    public Sprite Icon;
    public AnimatorController Animator;
    public List<GrowthTransition> Transitions = new List<GrowthTransition>();
    public PetStats DefaultStats;
    public TMP_FontAsset ChatFont;
    public List<Message> Messages = new List<Message>() { Message.defaultChat, Message.defaultHunger };

    public GrowthStage()
    {
        ID = Guid.NewGuid().ToString();
    }
}
