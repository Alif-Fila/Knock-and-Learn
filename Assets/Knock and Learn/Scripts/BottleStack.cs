using UnityEngine;

public class BottleStack : MonoBehaviour
{
    private bool isCorrect;
    private int answerNumber;
    private BottleStackManager manager;

    public void Initialize(bool correct, int number, BottleStackManager stackManager)
    {
        isCorrect = correct;
        answerNumber = number;
        manager = stackManager;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        var dragScript = collision.gameObject.GetComponent<DragAndRelease>();
        if (dragScript != null && dragScript.hasResolvedCollision) return; // ignore extra hits

        // Check answer
        manager.CheckAnswer(isCorrect, answerNumber);

        // Play yay or aww
        if (isCorrect)
        {
            SoundManager.Instance.PlayYay();
            ScoreManager.Instance.AddScore(1);
        }
        else
        {
            SoundManager.Instance.PlayAww();
        }

        // Stop ball movement and disable dragging
        if (dragScript != null)
        {
            dragScript.DisableDragAfterThrow();
            dragScript.ResolveThrow();
        }
    }

}
