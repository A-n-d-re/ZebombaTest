using System.Collections;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Slot[] slots;

    [SerializeField] private float checkingInterval = 0.5f;

    private void OnEnable()
    {
        StartCoroutine(CheckSlotsStatus());
    }

    private IEnumerator CheckSlotsStatus()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkingInterval);

            bool isOneSlotNotFull = false;

            foreach (Slot slot in slots)
            {
                if (!slot.isSlotFull)
                {
                    isOneSlotNotFull = true;
                    break;
                }
            }

            if (!isOneSlotNotFull)
            {
                DeleteAllBalls();

                gameScreen.SetActive(false);
                gameOverScreen.SetActive(true);
            }
        }
    }

    public static void DeleteAllBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }
}
