using UnityEngine;

public class Patrol : AIBehaviour
{
    public Transform[] PatrolPoints;
    private int currentPPIndex;

    public override void OnActivate(AIController controller)
    {
        controller.SetDefaultSpeed();
        controller.SetDestination(PatrolPoints[currentPPIndex].position);    
    }

    public override void OnUpdate(AIController controller)
    {
        if (controller.RemainingDistance <= controller.StoppingDistance) {
            currentPPIndex = currentPPIndex < PatrolPoints.Length - 1 ? currentPPIndex + 1 : 0;
            controller.SetDestination(PatrolPoints[currentPPIndex].position);
        }
    }
}
