using UnityEngine;

public class Chase : AIBehaviour
{
    public Transform Target;
    public float AgentSpeedMultiplier = 2f;

    public override void Activate(AIController controller)
    {
        controller.MultiplySpeed(AgentSpeedMultiplier);
        controller.IgnoreEars(true);
    }

    public override void UpdateStep(AIController controller)
    {
        controller.SetDestination(Target.position);
    }

    public override void Deactivate(AIController controller)
    {
        controller.IgnoreEars(false);
    }
}
