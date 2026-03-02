using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DairyLevelQue : MonoBehaviour
{
    [Header("Question Panel")]
    public GameObject BreadQuestion;

    [Header("UI Elements")]
    public Button nextButton;       // Button to enable after correct answer
    public Text messageText;        // Text to show result message

    void Start()
    {
        BreadQuestion.SetActive(false);
        nextButton.interactable = false;   // Disable next button at start
        messageText.text = "";
    }

    public void OpenQuestion()
    {
        BreadQuestion.SetActive(true);
    }

    // Call this function from each option button
    // Pass true for correct option, false for wrong option
    public void CheckAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            messageText.text = "Answer is Correct!";
            nextButton.interactable = true;
        }
        else
        {
            messageText.text = "Check the Answer!";
        }
    }
}