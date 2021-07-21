using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PetStats
{
    public float Speed = 6f;
    public Vector2 MoveInterval = new Vector2(1.5f, 3f);
    public Vector2 SpeakInterval = new Vector2(45f, 60f);
}
