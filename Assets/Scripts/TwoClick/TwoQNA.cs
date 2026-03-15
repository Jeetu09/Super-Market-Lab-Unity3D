using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TwoQNA : MonoBehaviour
{
    [System.Serializable]
    public class QuestionElement
    {
        public GameObject panel;
        public TMP_Text questionText;
        public TMP_Text indicatorText;
        public Button submitButton;
        public Animator funFactAnimator;
        public string triggerName = "Play";

        public AnswerButton[] answerButtons;
    }

    [System.Serializable]
    public class AnswerButton
    {
        public Button button;
        public bool isCorrect;
    }

    public QuestionElement[] questions;


    int selectedAnswer = -1;
    int currentQuestion = 0;

    void Start()
    {
        SetupButtons();

        // Enable only first question
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].panel.SetActive(i == 0);
        }
    }

    void SetupButtons()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            int qIndex = i;

            for (int j = 0; j < questions[i].answerButtons.Length; j++)
            {
                int aIndex = j;

                questions[i].answerButtons[j].button.onClick.AddListener(() =>
                {
                    SelectAnswer(qIndex, aIndex);
                });
            }

            questions[i].submitButton.onClick.AddListener(() =>
            {
                SubmitAnswer(qIndex);
            });
        }
    }

    void SelectAnswer(int qIndex, int aIndex)
    {
        selectedAnswer = aIndex;
        currentQuestion = qIndex;
    }

    void SubmitAnswer(int qIndex)
    {
        if (selectedAnswer == -1) return;

        bool isCorrect = questions[qIndex].answerButtons[selectedAnswer].isCorrect;

        if (isCorrect)
        {
            questions[qIndex].indicatorText.text = "Correct Answer";
            questions[qIndex].funFactAnimator.SetTrigger(questions[qIndex].triggerName);

            StartCoroutine(NextQuestionFlow(qIndex));
        }
        else
        {
            questions[qIndex].indicatorText.text = "Incorrect Answer";
        }
    }

    IEnumerator NextQuestionFlow(int qIndex)
    {
        yield return new WaitForSeconds(14f);

        questions[qIndex].panel.SetActive(false);

        if (qIndex + 1 < questions.Length)
        {
            questions[qIndex + 1].panel.SetActive(true);
            selectedAnswer = -1;
        }
        else
        {
            Debug.Log("All questions completed!");
        }
    }
}