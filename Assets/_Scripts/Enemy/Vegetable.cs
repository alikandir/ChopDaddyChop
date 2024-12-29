using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    [SerializeField] private VegetableType vegetableType;
    public VegetableType GetVegetableType(){return vegetableType;}
    [SerializeField] private int _rewardQuantity;
    public int GetRewardQuantity(){return _rewardQuantity;}

    public enum VegetableType{
        Onion,
        Carrot,
        Cabbage
    }
}

