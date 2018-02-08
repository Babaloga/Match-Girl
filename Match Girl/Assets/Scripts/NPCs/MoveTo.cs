// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MoveTo : MonoBehaviour
{
    Transform goal;
    NavMeshAgent agent;

    public float maxRadius = 100;

    public float maxPauseTime = 10;
    public float minPauseTime = 1;

    public float maxTimeBetween = 180;
    public float minTimeBetween = 40;

    public float goalBuffer = 1;

    public LayerMask mask;

    public AnimationCurve xAxis;
    public AnimationCurve zAxis;

    float pauseTime;
    float timeUntilNextPause;
    float timeMarker;

    bool paused;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        goal = new GameObject(transform.name + " goal").transform;

        StartCoroutine(StartDelay());
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
        if (paused)
        {
            agent.isStopped = true;

            if(Time.time - timeMarker > pauseTime)
            {
                Resume();
            }
        }
        else
        {
            agent.isStopped = false;

            if (Time.time - timeMarker > timeUntilNextPause)
            {
                Pause();
            }
        }

        Vector3 offset = transform.position - goal.position;

        offset.y = 0;

        if (offset.magnitude <= goalBuffer)
        {
            DestinationReached();
        }
    }

    private void OnDrawGizmos()
    {
        if (goal)
        {
            Gizmos.DrawSphere(goal.position, 3);
        }
    }

    private void PickNewGoal()
    {
        //Vector2 rand2D = Random.insideUnitCircle * maxRadius;
        Vector3 randomPosition = new Vector3(xAxis.Evaluate(Random.Range(-100, 100)), 0, zAxis.Evaluate(Random.Range(-100, 100)));

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPosition, out hit, maxRadius, 24);

        //print(transform.name + " " + hit.mask);

        goal.position = hit.position;
    }

    private void DestinationReached()
    {
        Pause();
        PickNewGoal();
        agent.destination = goal.position;
    }

    private void Pause()
    {
        pauseTime = Random.Range(minPauseTime, maxPauseTime);
        timeMarker = Time.time;
        paused = true;
    }

    private void Resume()
    {
        timeUntilNextPause = Random.Range(minTimeBetween, maxTimeBetween);
        timeMarker = Time.time;
        paused = false;
    }
}