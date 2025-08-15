using UnityEngine;

public class BottleStackManager : MonoBehaviour
{
    public GameObject[] numberPrefabs = new GameObject[9];
    public Vector3[] stackPositions = new Vector3[]
    {
        new Vector3(-5f, -3f, 0f),
        new Vector3(-0.35f, -3f, 0f),
        new Vector3(4.3f, -3f, 0f)
    };

    private GameObject[] spawnedStacks = new GameObject[3];
    private int[] currentStackNumbers = new int[3];
    private bool[] currentCorrectFlags = new bool[3];

    public void SpawnStacks(bool reusePrevious = false)
    {
        ClearStacks();

        var question = QuestionManager.Instance.GetCurrentQuestion();
        int correctAnswer = question.correctAnswer;
        int correctIndex = Random.Range(0, stackPositions.Length);

        for (int i = 0; i < stackPositions.Length; i++)
        {
            int displayNumber;
            bool isCorrect;

            if (reusePrevious)
            {
                displayNumber = currentStackNumbers[i];
                isCorrect = currentCorrectFlags[i];
            }
            else
            {
                displayNumber = (i == correctIndex) ? correctAnswer : GetRandomWrongAnswer(correctAnswer);
                isCorrect = (displayNumber == correctAnswer);

                currentStackNumbers[i] = displayNumber;
                currentCorrectFlags[i] = isCorrect;
            }

            GameObject prefab = numberPrefabs[displayNumber - 1];
            GameObject stack = Instantiate(prefab, stackPositions[i], Quaternion.identity);
            spawnedStacks[i] = stack;

            var stackScripts = stack.GetComponentsInChildren<BottleStack>();
            foreach (var stackComp in stackScripts)
                stackComp.Initialize(isCorrect, displayNumber, this);

            stack.name = "BottleStack_" + displayNumber;
        }
    }

    private int GetRandomWrongAnswer(int correct)
    {
        int randomValue;
        do { randomValue = Random.Range(1, numberPrefabs.Length + 1); } while (randomValue == correct);
        return randomValue;
    }

    public void ClearStacks()
    {
        for (int i = 0; i < spawnedStacks.Length; i++)
        {
            if (spawnedStacks[i] != null)
                Destroy(spawnedStacks[i]);
            spawnedStacks[i] = null;
        }
    }

    public void CheckAnswer(bool isCorrect, int chosenAnswer)
    {
        var qDisplay = FindObjectOfType<QuestionDisplay>();
        if (qDisplay == null) return;

        if (isCorrect)
            qDisplay.ShowResult("Correct!\n<color=white>Press SPACE to go to the next level</color>", Color.green);
        else
            qDisplay.ShowResult("Incorrect!\n<color=white>Press SPACE to retry</color>", Color.red);

        QuestionManager.Instance.lastAnswerCorrect = isCorrect;
    }
}
