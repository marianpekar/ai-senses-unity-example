using UnityEngine;

public class Chase : Behaviour
{
    public Transform Target;

    public override void OnActivate(AIController controller)
    {
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
