using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerData
{
    [Header("Egg")]
    public EggSO OwnedEgg;
    public string EggName;
    public UDateTime HatchingSince;
    [Header("Pet")]
    public PetSO OwnedPet;
    public string PetName;
    public byte EvolutionStage;
    public UDateTime LastEvolutionTime;
    [Header("Other")]
    public List<EggSO> PreviouslyOwnedEggs;
}