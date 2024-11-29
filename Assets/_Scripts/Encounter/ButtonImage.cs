using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonImage : MonoBehaviour
{
    [SerializeField] private BattlePatternElement _pattern; // I don't like this class, but this was the easiest way to get the pattern to the button
    public BattlePatternElement GetBattlePatternElement(){return _pattern;}
}
