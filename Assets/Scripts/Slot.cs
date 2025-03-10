using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private GameObject[] balls = new GameObject[3];

    private int ballCount;

    public bool isSlotFull { get; private set; }

    private void OnEnable()
    {
        ClearSlot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsBallInSlot(collision.gameObject))
        {
            AddBallToSlot(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsBallInSlot(collision.gameObject))
        {
            AddBallToSlot(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(DelayedBallRemoval(collision.gameObject));
    }

    private void AddBallToSlot(GameObject ball)
    {
        if (ballCount >= 3) return;

        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] == null)
            {
                balls[i] = ball;
                ballCount++;

                break;
            }
        }

        isSlotFull = (ballCount == 3);

        if (isSlotFull)
        {
            ResetSlot();
        }
    }

    private bool IsBallInSlot(GameObject ball)
    {
        foreach (var existingBall in balls)
        {
            if (existingBall == ball)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator DelayedBallRemoval(GameObject ball)
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] == ball)
            {
                balls[i] = null;
                ballCount--;

                isSlotFull = (ballCount == 3);

                break;
            }
        }

        CleanupMissingReferences();
    }

    private void CleanupMissingReferences()
    {
        int validBalls = 0;
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] == null || balls[i].ToString() == "null")
            {
                balls[i] = null;
            }
            else
            {
                validBalls++;
            }
        }
        ballCount = validBalls;
    }

    private bool AreAllBallsSame()
    {
        if (ballCount < 3) return false;

        string firstBallType = balls[0]?.name.Replace("(Clone)", "");

        for (int i = 1; i < balls.Length; i++)
        {
            if (balls[i] == null || balls[i].name.Replace("(Clone)", "") != firstBallType)
            {
                return false;
            }
        }

        return true;
    }

    private void ResetSlot()
    {
        if (ballCount < 3) return;

        if (AreAllBallsSame())
        {
            string ballType = balls[0].name.Replace("(Clone)", "");
            scoreCounter.AddScore(ballType);

            foreach (var ball in balls)
            {
                if (ball != null)
                {
                    ball.GetComponent<Image>().enabled = false;
                    ball.GetComponent<Collider2D>().enabled = false;
                    ball.GetComponent<Rigidbody2D>().simulated = false;

                    ParticleSystem destroyParticle = ball.GetComponentInChildren<ParticleSystem>();
                    destroyParticle.Play();

                    StartCoroutine(DestroyAfterParticles(ball, destroyParticle));
                }
            }
        }
    }

    private IEnumerator DestroyAfterParticles(GameObject ball, ParticleSystem destroyParticle)
    {
        yield return new WaitWhile(() => destroyParticle.isPlaying);

        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] == ball)
            {
                balls[i] = null;
                break;
            }
        }

        Destroy(ball);
        CleanupMissingReferences();
    }

    private void ClearSlot()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i] = null;
        }

        ballCount = 0;
        isSlotFull = false;
    }
}
