using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Booster", menuName = "Booster/Booster Data")]
public class BoosterData : ScriptableObject
{
    public string id;
    public string displayName;
    public int currentAmount;
    public int maxAmount;
    public int priceGem;
    public int priceFruit;
}
