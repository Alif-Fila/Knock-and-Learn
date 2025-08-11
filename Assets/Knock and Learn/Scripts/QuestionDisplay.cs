using UnityEngine;
using TMPro;

public class QuestionDisplay : MonoBehaviour
{
    public TextMeshProUGUI questionText;

    void Start()
    {
        ShowQuestion("What is 3 + 3?");
    }

    public void ShowQuestion(string question)
    {
        questionText.text = question;
        questionText.gameObject.SetActive(true);
    }

    public void HideQuestion()
    {
        questionText.gameObject.SetActive(false);
    }
}
