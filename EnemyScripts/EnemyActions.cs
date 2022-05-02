using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{

    public GateMechanics gate1;
    public GateMechanics gate2;

    public static EnemyActions instance;
    private void Awake()
    {
        instance = this;
    }
    public void die() 
    {
        gameObject.GetComponent<EnemyStats>().currentEnemyState = EnemyStats.enemyState.dead;
        if (gameObject.GetComponent<EnemyStats>().currentEnemyType != EnemyStats.enemyType.tutorial)
        {
            WaveManager.instance.numCurrentEnemies -= 1;
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
    public void takeDamage(float damage) 
    {
        gameObject.GetComponent<EnemyStats>().health -= damage;
        if (gameObject.GetComponent<EnemyStats>().health <= 0) 
        {
            if (gameObject.GetComponent<EnemyStats>().currentEnemyState != EnemyStats.enemyState.dead)
            {
                die();
            }
        }
    }
    public IEnumerator stopEnemies(float duration) 
    {
        EnemyMovement.stages temp = gameObject.GetComponent<EnemyMovement>().mainStage;
        gameObject.GetComponent<EnemyMovement>().currentStage = EnemyMovement.stages.vineAttacked;
        yield return new WaitForSeconds(duration);
        try
        {
            gameObject.GetComponent<EnemyMovement>().currentStage = temp;
        }
        catch { }
    }
    public IEnumerator slowEnemies(float duration)
    {
        gameObject.GetComponent<EnemyMovement>().currentEffect = EnemyMovement.effects.chilled;
        yield return new WaitForSeconds(duration);
        try
        {
            gameObject.GetComponent<EnemyMovement>().currentEffect = EnemyMovement.effects.none;
        }
        catch { }
    }
}
