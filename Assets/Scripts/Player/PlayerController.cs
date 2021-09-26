using UnityEngine;

[RequireComponent(typeof(Detectable))]
public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 6f;
    public float RotationSpeed = 100f;

    private Detectable detectable;

    private void Start()
    {
        detectable = GetComponent<Detectable>();
    }

    private void Update()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        detectable.CanBeHear = verticalAxis > 0f;

        transform.Translate(Vector3.forward * verticalAxis * MoveSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * horizontalAxis * RotationSpeed * Time.deltaTime);
    }
}