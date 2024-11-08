using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterBase : MonoBehaviour
{
    public EncounterType encounterType{get; protected set;}
}
public enum EncounterType
{
    Battle,
    NPC,
    Treasure
}
