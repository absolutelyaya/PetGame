using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Reference;

    public PlayerData data;

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
            data = SaveSystem.TryLoadData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveData(data);
    }
}
