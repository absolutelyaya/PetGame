using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "new Pet", menuName = "Pets/Pet")]
public class PetSO : ScriptableObject
{
    public List<GrowthStage> Stages;
    public GrowthStage InitialStage = new GrowthStage();

    public GrowthStage GetStageByID(string ID)
    {
        foreach (var stage in Stages)
        {
            if (stage.ID == ID)
                return stage;
        }
        throw new ArgumentException($"no stage with the ID '{ID}' found in EvolutionTree {name}");
    }
}
