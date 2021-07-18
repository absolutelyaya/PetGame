using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GrowthStage
{
    public string ID;
    [HideInInspector]
    public Rect Display;
    [HideInInspector]
    public int Depth;
    public Sprite Icon;
    public Animator Animator;
    public List<GrowthTransition> Transitions = new List<GrowthTransition>();
    public PetStats DefaultStats;

    public GrowthStage()
    {
        ID = Guid.NewGuid().ToString();
    }
}
