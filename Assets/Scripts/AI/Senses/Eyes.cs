using UnityEngine;

public class Eyes : Sense
{
    [Range(0,180)]
    public float FieldOfView;

    private float FieldOfViewDot;

    private void Start()
    {
        FieldOfViewDot = 1 - Remap(FieldOfView * 0.5f, 0, 90, 0, 1f);
    }

    private float Remap(float value, float originalStart, float originalEnd, float targetStart, float targetEnd) 
    {
        return targetStart + (value - originalStart) * (targetEnd - targetStart) / (originalEnd - originalStart);
    }

    protected override bool HasDetected(Detectable detectable) 
    {
        return IsInVisibleArea(detectable) && IsNotOccluded(detectable); 
    }

    private bool IsInVisibleArea(Detectable detectable)
    {
        float distance = Vector3.Distance(detectable.transform.position, this.transform.position);

        return distance <= Distance && Vector3.Dot(Direction(detectable.transform.position, this.transform.position), this.transform.forward) >= FieldOfViewDot;
    }

    private Vector3 Direction(Vector3 from, Vector3 to) 
    {
        return (from - to).normalized;
    }

    private bool IsNotOccluded(Detectable detectable)
    {
        if (Physics.Raycast(transform.position, detectable.transform.position - transform.position, out RaycastHit hit, Distance)) 
        {
            return hit.collider.gameObject.Equals(detectable.gameObject);
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = DebugDrawColor;
        for(float angle = -FieldOfView * 0.5f; angle <= FieldOfView * 0.5f; angle += 5f) 
        {
            Vector3 lineEnd = RotatePointAroundPivot(transform.position + transform.forward * Distance, transform.position, angle);
            Gizmos.DrawLine(transform.position, lineEnd);
        }
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angles)
    {
        return Quaternion.Euler(0, angles, 0) * (point - pivot) + pivot;
    }
#endif
}