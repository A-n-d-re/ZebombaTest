using UnityEngine;
using UnityEngine.UI;

public class BouncingBall : MonoBehaviour
{
    [SerializeField] private string layerToJumpName;

    [SerializeField] private float jumpForce = 5f;

    private Rigidbody2D ballRigidbody;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(layerToJumpName))
        {
            ballRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
