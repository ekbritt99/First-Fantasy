using System;
using UnityEngine;

// An item buff that can be applied to an item
// Can be manually set or generated randomly between min and max values
[Serializable]
public class ItemBuff
{
    public Attributes stat;
    public int value;
    [SerializeField]
    private int min;
    public int Min => min;
    [SerializeField]
    private int max;
    public int Max => max;



    public ItemBuff(int min, int max)
    {
        this.min = min;
        this.max = max;
        GenerateValue();
    }

    // Check if the buff can be upgraded
    public bool IsUpgradable() {
        if(value < max)
            return true;
        return false;
    }

    // Upgrade the buff by the given value
    public void UpgradeStat(int v)
    {
        value += v;
        if(value > max)
        {
            value = max;
        }
    }

    // Generate a random value for the buff between the min and max values
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }

}

public enum Attributes
{
    Strength,
    Agility,
    Intellect,
    Defense,
    Healing
}