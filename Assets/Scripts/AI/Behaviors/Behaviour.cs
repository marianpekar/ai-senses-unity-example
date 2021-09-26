using UnityEngine;

public class Behaviour : MonoBehaviour
{
    public virtual void OnActivate(AIController aIController) { }
    public virtual void OnUpdate(AIController aIController) { }
    public virtual void OnDeactivate(AIController aIController) { }
}
