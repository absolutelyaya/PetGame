using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerData
{
    [Header("Egg")]
    public string OwnedEggID;
    public string EggName;
    public UDateTime HatchingSince;
    [Header("Pet")]
    public string OwnedPetID;
    public string PetName;
    public byte EvolutionStage;
    public UDateTime LastEvolutionTime;
    [Header("Other")]
    public List<string> PreviouslyOwnedEggIDs;
}