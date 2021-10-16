using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    public virtual void OnActivate(AIController aIController) { }
    public abstract void OnUpdate(AIController aIController);
    public virtual void OnDeactivate(AIController aIController) { }
}
