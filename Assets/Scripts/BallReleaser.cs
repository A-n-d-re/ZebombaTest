using UnityEngine;
using System.Collections;

public class BallReleaser : MonoBehaviour
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameObject[] ballPrefabs;
    [SerializeField] private float ballCreatingDelay = 0.3f; 

    private GameObject ball;

    private int ballCount;

    private bool isCreatingBall = false; 

    private void OnEnable()
    {
        StartCoroutine(CreateBallWithDelay());
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isCreatingBall)
        {
            ball.GetComponent<Rigidbody2D>().isKinematic = false;
            ball.transform.SetParent(canvasTransform);

            StartCoroutine(CreateBallWithDelay());
        }
    }

    private IEnumerator CreateBallWithDelay()
    {
        isCreatingBall = true;

        yield return new WaitForSeconds(ballCreatingDelay); 

        CreateBall(); 

        isCreatingBall = false;
    }

    private void CreateBall()
    {
        if (ballCount < ballPrefabs.Length)
        {
            InstantiateBall();
        }
        else
        {
            ballCount = 0;
            InstantiateBall();
        }

        ballCount++;
    }

    private void InstantiateBall()
    {
        ball = Instantiate(ballPrefabs[ballCount], transform.position, transform.rotation, transform);
    }
}
