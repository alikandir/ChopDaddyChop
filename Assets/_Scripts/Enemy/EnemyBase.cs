using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
   [SerializeField] protected float _health;
   [SerializeField] protected float _damage;
   public float GetDamage{get=>_damage;}
   [SerializeField] protected BattlePatternElement[] _battlePattern;
   public event Action OnEnemyDied;
    public bool IsAlive { get => _health > 0; }
    public BattlePatternElement[] BattlePattern { get => _battlePattern;}
    public virtual void TakeDamage(float damage){
        _health-=damage;
        if (_health<=0){
            OnEnemyDied?.Invoke();
            }
    }
  
   
}
public enum BattlePatternElement
   {
       XSlash,
       YSlash,
       ADefend,
       Wait,
   }