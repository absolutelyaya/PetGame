using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Message
{
    public MessageType Type;
    public string Text;
    public int Weight;
    public float Duration;

    public Message(MessageType type, string text, int weight, float duration)
    {
        Type = type;
        Text = text;
        Weight = weight;
        Duration = duration;
    }

    public static Message defaultChat = new Message(MessageType.Chat, "  ü  ", 1, 5f);
    public static Message defaultHunger = new Message(MessageType.Hungry, "I'm hungry", 1, 5f);

    [Serializable]
    public enum MessageType
    {
        Chat,
        Hungry
    }
}
