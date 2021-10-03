using UnityEngine;

public class Chase : AIBehaviour
{
    public Transform Target;
    public float AgentSpeedMultiplier = 2f;

    public override void OnActivate(AIController controller)
    {
        controller.MultiplySpeed(AgentSpeedMultiplier);
        controller.IgnoreEars(true);
    }

    public override void OnUpdate(AIController controller)
    {
        controller.SetDestination(Target.position);
    }

    public override void OnDeactivate(AIController controller)
    {
        controller.IgnoreEars(false);
    }
}
