using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
   [SerializeField] protected int _health;
   [SerializeField] protected int _damage;
   [SerializeField] protected BattlePatternElement[] _battlePattern;

    public BattlePatternElement[] BattlePattern { get => _battlePattern;}
    
  
   
}
public enum BattlePatternElement
   {
       XSlash,
       YSlash,
       ADefend,
       Wait,
   }