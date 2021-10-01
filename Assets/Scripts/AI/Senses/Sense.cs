using UnityEngine;
using UnityEngine.Events;

public class Sense : MonoBehaviour
{
    public Detectable Detectable;
    public float Distance;

    protected bool IsSensing;

    public bool IsDetectionContinuous = true;

    public UnityEvent<Detectable> OnDetect;
    public UnityEvent<Detectable> OnLost;

#if UNITY_EDITOR
    public Color DebugDrawColor = Color.green;
#endif

    private void Detect(Detectable detectable) 
    {
        IsSensing = true;
        OnDetect?.Invoke(detectable);
    }
    private void Lost(Detectable detectable)
    {
        IsSensing = false;
        OnLost?.Invoke(detectable); 
    }

    void Update()
    {
        if (IsSensing)
        {
            if (!HasDetected(Detectable))
            {
                Lost(Detectable);
                return;
            }

            if(IsDetectionContinuous) 
            {
                Detect(Detectable);
            }
        }
        else
        {
            if (!HasDetected(Detectable))
                return;

            Detect(Detectable);
        }      
    }

    protected virtual bool HasDetected(Detectable detectable) => false;
}