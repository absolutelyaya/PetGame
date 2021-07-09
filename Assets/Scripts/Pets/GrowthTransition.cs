using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GrowthTransition
{
    public string ID;
    public string Destination = string.Empty;
    public List<GrowthRequirement> Requirements;

    public GrowthTransition(string destination)
    {
        Destination = destination;
        ID = Guid.NewGuid().ToString();
        Requirements = new List<GrowthRequirement>() { new GrowthRequirement() };
    }
}
