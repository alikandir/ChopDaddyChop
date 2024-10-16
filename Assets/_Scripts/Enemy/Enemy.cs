
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy:MonoBehaviour
{
    abstract protected void TakeDamage(int amount);
    abstract protected void Attack();
    abstract protected void Die();
}
