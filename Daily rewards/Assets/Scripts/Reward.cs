using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    public enum RewardType
    {
        GOLD,
        CABOOLLS
    }

    public RewardType Type;
    public int Value;
    public string Name;// что бы отобразить какую награду получит игрок

}
