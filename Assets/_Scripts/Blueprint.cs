using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blueprint
{
    public GameObject[] prefabs;
    public int[] towerCosts;

    public int GetSellAmount(int levelIndex)
    {
        return towerCosts[levelIndex] / 3;
    }
}
