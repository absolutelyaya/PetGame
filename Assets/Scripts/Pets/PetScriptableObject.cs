using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Pet", menuName = "Pets/Pet")]
public class PetScriptableObject : ScriptableObject
{
    public List<GrowthStage> Stages;
    public GrowthStage InitialStage = new GrowthStage();
}
