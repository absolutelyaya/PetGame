using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GrowthRequirement
{
    public GrowthRequirementType Type;
    public float MinAge = -1;

    public bool IsFullfilled()
    {
        switch (Type)
        {
            case GrowthRequirementType.Age:
                break;
            case GrowthRequirementType.Food:
                break;
            default:
                break;
        }
        return true;
    }
}

[Serializable]
public enum GrowthRequirementType
{
    Age,
    Food
}