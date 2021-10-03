using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public virtual void OnActivate(AIController aIController) { }
    public virtual void OnUpdate(AIController aIController) { }
    public virtual void OnDeactivate(AIController aIController) { }
}
