using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;

    public bool frozen;

    public float attackSpeed;

    public float attackDamage;

    public enum enemyType 
    {
        normal,
        tutorial
    }
    public enemyType currentEnemyType = enemyType.normal;
    public enum enemyState
    {
        alive,
        dead
    }
    public enemyState currentEnemyState = enemyState.alive;

    public static EnemyStats instance;
    private void Awake()
    {
        instance = this;
    }
}
