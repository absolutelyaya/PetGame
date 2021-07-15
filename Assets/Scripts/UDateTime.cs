using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UDateTime : ISerializationCallbackReceiver
{
    public DateTime dateTime;

    public int Year, Month, Day, Hour, Minute, Second;

    public int GetSecondsSince()
    {
        return (int)(dateTime - DateTime.Now).TotalSeconds;
    }

    public void OnAfterDeserialize()
    {
        try
        {
            dateTime = new DateTime(Year, Month, Day, Hour, Minute, Second);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void OnBeforeSerialize()
    {
        Year = dateTime.Year;
        Month = dateTime.Month;
        Day = dateTime.Day;
        Hour = dateTime.Hour;
        Minute = dateTime.Minute;
        Second = dateTime.Second;
    }
}
