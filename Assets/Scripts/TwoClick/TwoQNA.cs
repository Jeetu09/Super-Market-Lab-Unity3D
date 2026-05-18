
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject SwitchCam;
    public GameObject MainCam;
    public GameObject Player;
    public GameObject DairyTrolleyObj;

    [SerializeField] private TrollyAttachment PlayerDis;

    void Start()
    {
        DairyTrolleyObj.SetActive(false);
        SetupButtons();

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

            if (questions[qIndex].funFactAnimator != null)
                questions[qIndex].funFactAnimator.SetTrigger(questions[qIndex].triggerName);
        }
        else
        {
            questions[qIndex].indicatorText.text = "Incorrect Answer";
        }
    }

    // Fun Fact button will call this
    public void ContinueAfterAnimation()
    {
        questions[currentQuestion].panel.SetActive(false);

        if (currentQuestion + 1 < questions.Length)
        {
            currentQuestion++;
            questions[currentQuestion].panel.SetActive(true);
            selectedAnswer = -1;
        }
        else
        {
            Debug.Log("All questions completed!");

            SwitchCam.SetActive(false);
            MainCam.SetActive(true);
            Player.SetActive(true);

            PlayerDis.EnablePlayerControls();
            DairyTrolleyObj.SetActive(true);
        }
    }
}