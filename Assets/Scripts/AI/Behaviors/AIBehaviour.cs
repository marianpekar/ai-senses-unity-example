using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    public virtual void Activate(AIController aIController) { }
    public abstract void UpdateStep(AIController aIController);
    public virtual void Deactivate(AIController aIController) { }
}
