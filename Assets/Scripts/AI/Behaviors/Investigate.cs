using UnityEngine;

public class Investigate : AIBehaviour
{
    public Vector3 Destination;
    public int InvestigateForSeconds = 3;
    public float AgentSpeedMultiplier = 1.5f;

    private bool isInvestigating;
    private float investigationStartTime;

    public override void OnActivate(AIController controller)
    {
        controller.MultiplySpeed(AgentSpeedMultiplier);
        controller.SetDestination(Destination);
    }

    public override void OnUpdate(AIController controller)
    {
        if (controller.RemainingDistance <= controller.StoppingDistance && !isInvestigating)
        {
            isInvestigating = true;
            investigationStartTime = Time.time;
        }

        if (isInvestigating && Time.time > investigationStartTime + InvestigateForSeconds) 
        {
            isInvestigating = false;
            controller.Patrol();
        }
    }

    public override void OnDeactivate(AIController aIController)
    {
        isInvestigating = false;
    }
}
