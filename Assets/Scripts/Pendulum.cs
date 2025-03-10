using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 1f;
    [SerializeField] private float maxAngle = 45f;

    private Rigidbody2D pendulumRigidbody;

    private float angle;

    private void Start()
    {
        pendulumRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        angle = maxAngle * Mathf.Sin(Time.time * swingSpeed);

        pendulumRigidbody.MoveRotation(Quaternion.Euler(0f, 0f, angle));
    }
}
