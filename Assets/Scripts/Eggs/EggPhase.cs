using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EggPhase
{
    [Tooltip("Minutes")]
    public float Duration;
    public Sprite Sprite;
    [Range(0f, 45f)]
    public float WiggleAmount = 0;
    [Range(1f, 6f)]
    public float WiggleSpeed = 1;

    public EggPhase(Sprite sprite, int duration, float wiggleAmount, float wiggleSpeed)
    {
        Sprite = sprite;
        Duration = duration;
        WiggleAmount = wiggleAmount;
        WiggleSpeed = wiggleSpeed;
    }
}
