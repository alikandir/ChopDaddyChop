using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemiesToSpawn;
    [SerializeField] private Transform[] _enemyLocations;
}
