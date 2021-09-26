using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(Investigate))]
[RequireComponent(typeof(Chase))]
public class AIController : MonoBehaviour
{
    private Behaviour currentBehavior;
    public Behaviour CurrentBehavior
    {
        get => currentBehavior;
        set
        {
            currentBehavior?.OnDeactivate(this);
            value.OnActivate(this);
            currentBehavior = value;
        }
    }

    public Patrol PatrolBehavior;
    public Investigate InvestigateBehavior;
    public Chase ChaseBehavior;

    private NavMeshAgent agent;
    public float RemainingDistance { get => agent.remainingDistance; }
    public float StoppingDistance { get => agent.stoppingDistance; }

    public void SetDestination(Vector3 destination) => agent.SetDestination(destination);

    private Eyes eyes;
    private Ears ears;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        PatrolBehavior = GetComponent<Patrol>();
        InvestigateBehavior = GetComponent<Investigate>();
        ChaseBehavior = GetComponent<Chase>();

        eyes = GetComponentInChildren<Eyes>();
        eyes.OnDetect.AddListener(Chase);
        eyes.OnLost.AddListener(Investigate);

        ears = GetComponentInChildren<Ears>();
        ears.OnDetect.AddListener(Investigate);

        Patrol();
    }

    void Update()
    {
        CurrentBehavior.OnUpdate(this);
    }

    public void Patrol()
    {
        CurrentBehavior = PatrolBehavior;
    }

    public void Investigate(Detectable detectable) 
    {
        InvestigateBehavior.Destination = detectable.transform.position;
        CurrentBehavior = InvestigateBehavior;
    }

    public void Chase(Detectable detectable) 
    {
        ChaseBehavior.Target = detectable.transform;
        CurrentBehavior = ChaseBehavior;
    }

    public void IgnoreEars(bool ignore) 
    {
        ears.gameObject.SetActive(!ignore);
    }
}