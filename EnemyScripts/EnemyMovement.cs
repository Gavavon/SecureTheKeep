using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public enum stages
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5,
        Stage6,
        Attack1,
        Attack2,
        Pause,
        vineAttacked
    }
    public stages currentStage = stages.Stage1;
    public stages mainStage = stages.Stage1;

    public enum effects
    {
        none,
        chilled
    }
    public effects currentEffect = effects.none;

    bool attackStart = false;

    public NavMeshAgent agent;

    public float agentDefaultSpeed;

    public Transform[] movementLocations;

    public GateMechanics gate1;
    public Transform gate1Location;
    public GateMechanics gate2;
    public Transform gate2Location;

    [Header("Animator")]
    Animator animator;

    public static EnemyMovement instance;
    private void Awake()
    {
        instance = this;
        Physics.IgnoreLayerCollision(gameObject.layer, 0, true);
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        updateStage();
        changeStages();
        effectFunctionality();
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (currentStage != stages.Attack1 || currentStage != stages.Attack2) 
        {
            animator.SetBool("Attacking", false);
        }
    }
    public void updateStage() 
    {
        switch (currentStage)
        {
            case stages.Stage1:
                agent.isStopped = false;
                agent.destination = movementLocations[0].position;
                break;
            case stages.Stage2:
                agent.isStopped = false;
                agent.destination = movementLocations[1].position;
                break;
            case stages.Stage3:
                agent.isStopped = false;
                agent.destination = movementLocations[2].position;
                break;
            case stages.Stage4:
                agent.isStopped = false;
                agent.destination = movementLocations[3].position;
                break;
            case stages.Stage5:
                agent.isStopped = false;
                agent.destination = movementLocations[4].position;
                break;
            case stages.Stage6:
                agent.isStopped = false;
                agent.destination = movementLocations[5].position;
                break;
            case stages.Pause:
				agent.isStopped = true;
                break;
            case stages.vineAttacked:
                agent.destination = transform.position;
                agent.isStopped = true;
                break;
        }
    }
    public void changeStages()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) < 2) 
        {
            switch (currentStage)
            {
                case stages.Stage1:
                    currentStage = stages.Stage2;
                    mainStage = stages.Stage2;
                    break;
                case stages.Stage2:
                    currentStage = stages.Stage3;
                    mainStage = stages.Stage3;
                    break;
                case stages.Stage3:
                    if (gate1.currentGateHealth <= 0)
                    {
                        attackStart = false;
                        currentStage = stages.Stage4;
                    }
                    else 
                    {
                        currentStage = stages.Attack1;
                    }
                    break;
                case stages.Attack1:
                    if (!attackStart)
                    {
                        if (Vector3.Distance(gate1.transform.position, agent.transform.position) < 5)
                        {
                            attackStart = true;
                            StartCoroutine(attack(gate1));
                        }
                        else
                        {
                            agent.destination = gate1Location.position;
                        }
					}
					else 
                    {
                        if (gate1.currentGateHealth <= 0)
                        {
                            currentStage = stages.Stage4;
                            attackStart = false;
                        }
                    }
                    break;
                case stages.Stage4:
                    currentStage = stages.Stage5;
                    mainStage = stages.Stage5;
                    break;
                case stages.Stage5:
                    currentStage = stages.Stage6;
                    mainStage = stages.Stage6;
                    break;
                case stages.Stage6:
                    currentStage = stages.Attack2;
                    break;
                case stages.Attack2:
                    if (!attackStart)
                    {
                        if (Vector3.Distance(gate2.transform.position, agent.transform.position) < 5)
                        {
                            attackStart = true;
                            StartCoroutine(attack(gate2));
                        }
                        else
                        {
                            agent.destination = gate2Location.position;
                        }
                    }
                    else
                    {
                        if (gate2.currentGateHealth <= 0)
                        {
                            //win the game
                        }
                    }
                    break;
            }
        }
    }
    public void effectFunctionality() 
    {
        switch (currentEffect)
        {
            case effects.none:
                agent.speed = agentDefaultSpeed;
                break;
            case effects.chilled:
                agent.speed = agentDefaultSpeed - (agentDefaultSpeed * IceBall.instance.speedDecreasePercent);
                StartCoroutine(chilledReset());
                break;
        }
    }

    [ContextMenu("Get Distance")]
    public void getDistance() 
    {
        Debug.Log(Vector3.Distance(agent.destination, agent.transform.position));
    }

    [ContextMenu("Freeze")]
    public void freeze()
    {
        currentStage = stages.Pause;
    }
    public void unFreeze(stages lastStage)
    {
        currentStage = lastStage;
    }
    IEnumerator attack(GateMechanics gate)
    {
        gate.takeDamage(EnemyStats.instance.attackDamage);
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(EnemyStats.instance.attackSpeed);
        if (gate.currentGateHealth > 0)
        {
            StartCoroutine(attack(gate));
        }
    }
    public void agentSetSpeed(float i) 
    {
        agent.speed = i;
    }
    IEnumerator chilledReset() 
    {
        yield return new WaitForSeconds(2f);
        currentEffect = effects.none;
    }
}
