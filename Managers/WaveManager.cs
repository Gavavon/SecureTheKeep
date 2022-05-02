using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Dialogue Mechanics")]
    public TextMover dialogue;

    [Header("Importasnt Wave Info")]
    public static int waveNumber = 0;
    public static int setsPerWave = 1;
    public static int maxRandomAdd = 1;

    //[Header("Wave Info")]
    [HideInInspector]
    public float maxTimeBetweenWave = 5;
    public float minTimeBetweenWave = 2;
    public float maxTimeBetweenSet = 30;
    public float minTimeBetweenSet = 20;
    public int maxEnemiesInSet = 14;
    public int minEnemiesInSet = 7;
    [HideInInspector]

    [Header("Enemy Info")]
    public GameObject enemy;
    public GameObject bigEnemy;
    public GameObject barbEnemy;
    public GameObject hero1;
    public GameObject hero2;

    [HideInInspector]
    public bool heroSummoned = false;
    public bool allSetsSpawnedIn = false;
    public int numCurrentEnemies = 0;
    public int totalNumEnemies = 0;
    public int numSetsInWave;
    [HideInInspector]

    public static WaveManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (GameplayManager.instance.getPlayTutorial())
        {
            dialogue.progressText();
        }
        else
        {
            createWave();
        }

        switch (GameManagement.instance.getGameDifficulty()) 
        {
            case GameManagement.gameDifficulty.easy:
                maxTimeBetweenWave = 5;
                minTimeBetweenWave = 2;
                maxTimeBetweenSet = 30;
                minTimeBetweenSet = 20;
                maxEnemiesInSet = 10;
                minEnemiesInSet = 5;
                break;
            case GameManagement.gameDifficulty.normal:
                maxTimeBetweenWave = 5;
                minTimeBetweenWave = 2;
                maxTimeBetweenSet = 30;
                minTimeBetweenSet = 15;
                maxEnemiesInSet = 12;
                minEnemiesInSet = 6;
                break;
            case GameManagement.gameDifficulty.hard:
                maxTimeBetweenWave = 2;
                minTimeBetweenWave = 1;
                maxTimeBetweenSet = 15;
                minTimeBetweenSet = 10;
                maxEnemiesInSet = 14;
                minEnemiesInSet = 7;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (numCurrentEnemies == 0 && allSetsSpawnedIn)
        {
            StartCoroutine(endWaveCoroutine());
        }
    }

    IEnumerator waveCoroutine()
    {
        for (int i = 0; i < numSetsInWave; i++)
        {
            createSet();
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSet, (maxTimeBetweenSet + 1)));
        }
        allSetsSpawnedIn = true;
    }
    IEnumerator endWaveCoroutine()
    {
        if (waveNumber % 10 == 0)
        {
            GateMechanics.instance.healTenWaveGate();
        }
        else
        {
            GateMechanics.instance.healGate();
        }
        allSetsSpawnedIn = false;
        heroSummoned = false;
        totalNumEnemies = 0;
        yield return new WaitForSeconds(Random.Range(minTimeBetweenWave, (maxTimeBetweenWave + 1)));
        createWave();
    }
    public void resetWaveManager()
    {
        waveNumber = 0;
        setsPerWave = 1;
        maxRandomAdd = 1;
    }
    //----------------------------------------------------------------
    #region Variable Set Up
    public void determineMaxRandom()
    {
        if (waveNumber % 5 == 0)
        {
            maxRandomAdd += 1;
        }
    }
    public void updateSetsPerWave()
    {
        setsPerWave = waveNumber + 1;
    }
    #endregion
    //----------------------------------------------------------------
    #region Wave and Set Creation
    [ContextMenu("Start Waves")]
    public void createWave()
    {
        waveNumber += 1;
        updateSetsPerWave();
        determineMaxRandom();
        numSetsInWave = setsPerWave + Random.Range(0, (maxRandomAdd + 1));
        StartCoroutine(waveCoroutine());
    }
    public void createSet()
    {
        int setCount = Random.Range(minEnemiesInSet, (maxEnemiesInSet + 1));
        bool temp = true;
        switch (temp)
        {
            case true when waveNumber % 10 == 0:
                if (!heroSummoned)
                {
                    summonHeroSet();
                }
                StartCoroutine(spawnEnemy(setCount, false, Random.Range(0, 3)));
                break;
            case true when waveNumber >= 5:
                StartCoroutine(spawnEnemy(setCount, false, Random.Range(0, 3)));
                break;
            case true when waveNumber >= 3:
                StartCoroutine(spawnEnemy(setCount, false, Random.Range(0, 2)));
                break;
            default:
                StartCoroutine(spawnEnemy(setCount, false, 0));
                break;
        }
        numCurrentEnemies += setCount;
        totalNumEnemies += setCount;
    }
    public void summonHeroSet()
    {
        StartCoroutine(spawnEnemy(1, false, Random.Range(3, 5)));
        heroSummoned = true;
    }
    public IEnumerator spawnEnemy(int spawnNum, bool tutorial, int num)
    {
        GameObject tempEnemy;
        switch (num)
        {
            case 0:
                tempEnemy = enemy;
                break;
            case 1:
                tempEnemy = barbEnemy;
                break;
            case 2:
                tempEnemy = bigEnemy;
                break;
            case 3:
                tempEnemy = hero1;
                break;
            case 4:
                tempEnemy = hero2;
                break;
            default:
                tempEnemy = enemy;
                break;
        }
        for (int i = 0; i < spawnNum; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.09f));
            GameObject instEnemy = Instantiate(tempEnemy, new Vector3(Random.Range(70.5f, 83.5f), 1.04f, Random.Range(-65, 80)), Quaternion.identity) as GameObject;
            if (tutorial)
            {
                instEnemy.GetComponent<EnemyStats>().currentEnemyType = EnemyStats.enemyType.tutorial;
            }

        }
    }
    #endregion
    //----------------------------------------------------------------
    #region setters and getters
    public int getWaveNumber()
    {
        return waveNumber;
    }
    #endregion
    //----------------------------------------------------------------
    #region context menus
    [ContextMenu("Spawn Enemy")]
    public void spawnEnemy() 
    {
        StartCoroutine(spawnEnemy(1, false,0));
    }
    [ContextMenu("Spawn BarbEnemy")]
    public void spawnBarbEnemy()
    {
        StartCoroutine(spawnEnemy(1, false, 1));
    }
    [ContextMenu("Spawn BigEnemy")]
    public void spawnBigEnemy()
    {
        StartCoroutine(spawnEnemy(1, false, 2));
    }
    [ContextMenu("Spawn HeroEnemy")]
    public void spawnHero1Enemy()
    {
        summonHeroSet();
    }
    #endregion
}
