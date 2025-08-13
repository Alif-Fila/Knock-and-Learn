using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string questionText;
    public int correctAnswer; // This will be the number on the correct bottle stack
}

public class QuestionManager : MonoBehaviour
{
    public QuestionData[] questions;
    public int currentQuestionIndex = 0;

    public static QuestionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public QuestionData GetCurrentQuestion()
    {
        return questions[currentQuestionIndex];
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex >= questions.Length)
            currentQuestionIndex = 0;
    }

    public void PreviousQuestion()
    {
        currentQuestionIndex--;
        if (currentQuestionIndex < 0)
            currentQuestionIndex = questions.Length - 1;
    }
}
