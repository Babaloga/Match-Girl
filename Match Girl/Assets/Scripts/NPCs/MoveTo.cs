// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MoveTo : MonoBehaviour
{
    Transform goal;
    NavMeshAgent agent;

    CrowdPoint[] points;

    public float maxRadius = 100;

    public float maxPauseTime = 10;
    public float minPauseTime = 1;

    public float maxTimeBetween = 180;
    public float minTimeBetween = 40;

    public float goalBuffer = 1;

    public AnimationCurve xAxis;
    public AnimationCurve zAxis;

    public LayerMask rayMask;

    float pauseTime;
    float timeUntilNextPause;
    float timeMarker;

    int lastGoal = -1;

    public bool paused;

    public bool overriden = false;

    [ShowOnly]
    public bool agentStopped = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        goal = new GameObject(gameObject.name + " goal").transform;

        StartCoroutine(StartDelay());

        points = FindObjectsOfType<CrowdPoint>();

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
        Transform startPoint;

        //Randomly picks a point to raycast from. For the first cast all points are equally weighted. For all subsequent casts the NPC is more likely to stay under the same point.
        if (lastGoal == -1) {
            int rand = Random.Range(0, points.Length);
            startPoint = points[rand].transform;
            lastGoal = rand;
        }
        else
        {
            int rand = Random.Range(-points.Length, points.Length);

            if (rand < 0)
            {
                rand = lastGoal;
            }
            startPoint = points[rand].transform;
        }

        bool validPoint = false;
        Vector3 goalPos = Vector3.zero;

        int i = 0;

        while (!validPoint)
        {
            //Break while look if more than 100 iterations occur to prevent freezing.
            if(i > 100) {
                Debug.LogWarning("Couldn't find viable ground under " + startPoint.name, transform);
                break;
            }

            //Generate random Vector3
            Vector3 randomDirection = Random.insideUnitSphere;
            //Set random Vector3 y axis to a new random value between -0.5 and -1
            randomDirection.y = Random.Range(-1, -0.5f);

            RaycastHit hit;

            //Raycast and check for a walkable surface
            if(Physics.Raycast(startPoint.position, randomDirection, out hit, Mathf.Infinity, rayMask, QueryTriggerInteraction.Ignore) && hit.collider.gameObject.layer == 12)
            {
                validPoint = true;
                goalPos = hit.point;
            }

            i++;
        }

        Debug.DrawLine(startPoint.position, goalPos, Color.red, 2f);

        goal.position = goalPos;
    }

    public void DestinationReached()
    {
        Pause();
        PickNewGoal();
        agent.destination = goal.position;
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