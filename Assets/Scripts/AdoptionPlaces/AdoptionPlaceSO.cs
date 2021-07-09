using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Adoption Place", menuName = "Pets/Adoption Place")]
public class AdoptionPlaceSO : ScriptableObject
{
    [HideInInspector]
    public List<EggSO> EggPool = new List<EggSO>();
    [HideInInspector]
    public Sprite Preview;
    [HideInInspector]
    public Texture2D ButtonTexture;
}
