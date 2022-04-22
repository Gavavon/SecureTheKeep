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

    [Header("Wave Info")]
    public float maxTimeBetweenWave = 30;
    public float minTimeBetweenWave = 20;
    public float maxTimeBetweenSet = 30;
    public float minTimeBetweenSet = 20;
    public int maxEnemiesInSet = 14;
    public int minEnemiesInSet = 7;

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
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSet, maxTimeBetweenSet));
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
        yield return new WaitForSeconds(Random.Range(minTimeBetweenWave, maxTimeBetweenWave));
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
        numSetsInWave = setsPerWave + Random.Range(0, maxRandomAdd);
        StartCoroutine(waveCoroutine());
    }
    public void createSet() 
    {
        int setCount = Random.Range(minEnemiesInSet, maxEnemiesInSet);
        bool temp = true;
        switch (temp)
        {
            case true when waveNumber % 10 == 0:
				if (!heroSummoned) 
                {
                    summonHeroSet();
                }
                StartCoroutine(spawnEnemy(setCount, false, Random.Range(0, 2)));
                break;
            case true when waveNumber >= 5:
                StartCoroutine(spawnEnemy(setCount, false, Random.Range(0, 2)));
                break;
            case true when waveNumber >= 3:
                StartCoroutine(spawnEnemy(setCount, false, Random.Range(0, 1)));
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
        StartCoroutine(spawnEnemy(1, false, Random.Range(3, 4)));
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
}
