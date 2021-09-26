using UnityEngine;

public class Investigate : Behaviour
{
    public Vector3 Destination;
    public int InvestigateForSeconds = 3;

    private bool isInvestigating;
    private float investigationStartTime;

    public override void OnActivate(AIController controller)
    {
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
}
