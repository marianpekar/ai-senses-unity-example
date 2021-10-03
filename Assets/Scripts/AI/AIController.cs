using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(Investigate))]
[RequireComponent(typeof(Chase))]
public class AIController : MonoBehaviour
{
    private AIBehaviour currentBehavior;
    public AIBehaviour CurrentBehavior
    {
        get => currentBehavior;
        private set
        {
            currentBehavior?.OnDeactivate(this);
            value.OnActivate(this);
            currentBehavior = value;
        }
    }

    private Patrol PatrolBehavior;
    private Investigate InvestigateBehavior;
    private Chase ChaseBehavior;

    private NavMeshAgent agent;
    public float RemainingDistance { get => agent.remainingDistance; }
    public float StoppingDistance { get => agent.stoppingDistance; }
    public void SetDestination(Vector3 destination) => agent.SetDestination(destination);

    private float defaultAgentSpeed;
    public void MultiplySpeed(float factor) => agent.speed = defaultAgentSpeed * factor;
    public void SetDefaultSpeed() => agent.speed = defaultAgentSpeed;

    private Eyes eyes;
    private Ears ears;
    public void IgnoreEars(bool ignore) => ears.gameObject.SetActive(!ignore);

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        defaultAgentSpeed = agent.speed;

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
}