// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class MoveTo : MonoBehaviour
{
    Transform goal;
    NavMeshAgent agent;

    List<CrowdPoint> points;

    public float maxRadius = 100;

    public float maxPauseTime = 10;
    public float minPauseTime = 1;

    public float maxTimeBetween = 180;
    public float minTimeBetween = 40;

    public float goalBuffer = 1;

    public AnimationCurve xAxis;
    public AnimationCurve zAxis;

    float pauseTime;
    float timeUntilNextPause;
    float timeMarker;

    int lastGoal = -1;

    public bool paused;

    public bool overriden = false;

    NPCType npcType;

    CrowdPoint goalPoint;

    [ShowOnly]
    public bool agentStopped = false;

    void Start()
    {
        npcType = GetComponent<NPCInteractionBasic>().npcType;

        agent = GetComponent<NavMeshAgent>();
        
        goal = new GameObject(gameObject.name + " goal").transform;

        StartCoroutine(StartDelay());

        points = new List<CrowdPoint>();

        switch (npcType)
        {
            case (NPCType.Rich):

                foreach(CrowdPoint p in FindObjectsOfType<CrowdPoint>())
                {
                    if (p.allowRich)
                    {
                        points.Add(p);
                    }
                }

                break;

            case (NPCType.Poor):

                foreach (CrowdPoint p in FindObjectsOfType<CrowdPoint>())
                {
                    if (p.allowPoor)
                    {
                        points.Add(p);
                    }
                }

                break;
        }


        if (points.Count == 0) gameObject.SetActive(false);

        NavMesh.avoidancePredictionTime = 0.2f;
        NavMesh.pathfindingIterationsPerFrame = 500;
    }

    private IEnumerator StartDelay()
    {
        yield return null;

        PickNewGoal();

        agent.Warp(goal.position);

        Resume();
    }

    private void Update()
    {
        if (agent.isOnNavMesh) agentStopped = agent.isStopped;

        if (!overriden)
        {
            if (paused)
            {
                if(agent.isOnNavMesh)
                    agent.isStopped = true;

                if (Time.time - timeMarker > pauseTime)
                {
                    if (agent.isOnNavMesh) Resume();
                }
            }
            else
            {
                if (agent.isOnNavMesh) agent.isStopped = false;

                if (Time.time - timeMarker > timeUntilNextPause)
                {
                    if (agent.isOnNavMesh) Pause();
                }
            }

            Vector3 offset = transform.position - goal.position;

            offset.y = 0;

            if (offset.magnitude <= goalBuffer)
            {
                DestinationReached();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (goal)
        {
            Gizmos.DrawSphere(goal.position, 1);
        }
    }

    private void PickNewGoal()
    {
        //Randomly picks a point to raycast from. For the first cast all points are equally weighted. For all subsequent casts the NPC is more likely to stay under the same point.
        if (lastGoal == -1) {
            int rand = Random.Range(0, points.Count);
            goalPoint = points[rand];
            lastGoal = rand;
        }
        else
        {
            int rand = Random.Range(-points.Count, points.Count);

            if (rand < 0)
            {
                rand = lastGoal;
            }
            goalPoint = points[rand];
        }

        goal.position = goalPoint.GetPosition();
    }

    public void DestinationReached()
    {
        ExitPoint exit = goalPoint as ExitPoint;
        if (exit) Destroy(gameObject);
        else
        {
            Pause();
            PickNewGoal();
            agent.destination = goal.position;
        }
    }

    public void Pause()
    {
        pauseTime = Random.Range(minPauseTime, maxPauseTime);
        timeMarker = Time.time;
        paused = true;
    }

    public void Pause(float _time)
    {
        pauseTime = _time;
        timeMarker = Time.time;
        paused = true;
    }

    public void Resume()
    {
        timeUntilNextPause = Random.Range(minTimeBetween, maxTimeBetween);
        timeMarker = Time.time;
        paused = false;
    }
}