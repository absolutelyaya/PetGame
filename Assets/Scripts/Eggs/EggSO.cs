using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Egg", menuName = "Pets/Egg")]
public class EggSO : ScriptableObject
{
    public string RetrievalText;
    public string Description;
    public List<EggPhase> Phases;
    public List<EggTag> Tags;
}
