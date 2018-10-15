using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    Transform player;
    NavMeshAgent agent;
    Vector3 target;
    Vector3 spawnLocation;

    private enum EnemyStates { Passive, Attack,};
    private EnemyStates enemyState = EnemyStates.Passive;

    public float detectRadius = 5f;
    public float returnToSpawnRadius = 25f;
    float distanceToPlayer;
    float distanceToSpawn;
    float randomAngle;

	// Use this for initialization
	void Start ()
    {
        spawnLocation = transform.position;
        target = spawnLocation;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("GameController").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);
        distanceToSpawn = Vector3.Distance(spawnLocation, transform.position);

        switch (enemyState)
        {
            case EnemyStates.Passive:

                if (!agent.pathPending)
                    if (agent.remainingDistance <= agent.stoppingDistance)
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                            RandomMovement();

                if (distanceToPlayer <= detectRadius)
                {
                    enemyState = EnemyStates.Attack;
                    MoveToTarget();

                }
                break;

            case EnemyStates.Attack:

                MoveToTarget();

                if (distanceToSpawn >= returnToSpawnRadius)
                {
                    enemyState = EnemyStates.Passive;
                    LookAtTarget(spawnLocation);
                    agent.SetDestination(spawnLocation);
                }
                break;
        }   
	}

    void MoveToTarget()
    {
        agent.SetDestination(player.position);

        if (distanceToPlayer <= agent.stoppingDistance)
        {
            LookAtTarget(player.position);
        }
    }

    void LookAtTarget(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void RandomMovement()
    {
        randomAngle = Random.Range(0f, Mathf.PI * 2f);
        target = new Vector3(spawnLocation.x + 5f*Mathf.Sin(randomAngle), spawnLocation.y, spawnLocation.z + 5f*Mathf.Cos(randomAngle));
        LookAtTarget(target);
        agent.SetDestination(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target);

    }
}
