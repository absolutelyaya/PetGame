using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DefaultExecutionOrder(-666)]
public class GameManager : MonoBehaviour
{
    public static GameManager Reference;

    public PlayerData data;
    public EggSO OwnedEgg;
    public List<EggSO> PreviouslyOwnedEggs;
    public PetSO OwnedPet;

    readonly Dictionary<string, EggSO> allEggs = new Dictionary<string, EggSO>();
    readonly Dictionary<string, PetSO> allPets = new Dictionary<string, PetSO>();
    readonly Dictionary<string, AdoptionPlaceSO> allAdoptionPlaces = new Dictionary<string, AdoptionPlaceSO>();

    private void Start()
    {
        if(Reference != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Reference = this;
            DontDestroyOnLoad(gameObject);
            data = SaveSystem.LoadData();
            FetchScriptableObjects();
            if (data.OwnedEggID != null && data.OwnedEggID.Length > 0)
                OwnedEgg = (EggSO)GetSOByName<EggSO>(data.OwnedEggID);
            if (data.OwnedPetID != null && data.OwnedPetID.Length > 0)
                OwnedEgg = (EggSO)GetSOByName<EggSO>(data.OwnedPetID);
            if(data.PreviouslyOwnedEggIDs != null && data.PreviouslyOwnedEggIDs.Count > 0)
            {
                foreach (var item in data.PreviouslyOwnedEggIDs)
                {
                    PreviouslyOwnedEggs.Add((EggSO)GetSOByName<EggSO>(item));
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveData(data);
    }

    void FetchScriptableObjects()
    {
        foreach (var item in Resources.LoadAll<ScriptableObject>("ScriptableObjects"))
        {
            if (item is AdoptionPlaceSO place && !allAdoptionPlaces.ContainsKey(item.name))
                allAdoptionPlaces.Add(item.name, place);
            if (item is PetSO pet && !allPets.ContainsKey(item.name))
                allPets.Add(item.name, pet);
            if (item is EggSO egg && !allEggs.ContainsKey(item.name))
                allEggs.Add(item.name, egg);
        }
    }

    public ScriptableObject GetSOByName<Type>(string name)
    {
        switch (typeof(Type).ToString())
        {
            case "AdoptionPlaceSO":
                if (allAdoptionPlaces.TryGetValue(name, out AdoptionPlaceSO place))
                    return place;
                break;
            case "PetSO":
                if (allPets.TryGetValue(name, out PetSO pet))
                    return pet;
                break;
            case "EggSO":
                if (allEggs.TryGetValue(name, out EggSO egg))
                    return egg;
                break;
            default:
                break;
        }
        throw new Exception($"Scriptable Object of type {typeof(Type)} named {name} not found!");
    }
}
