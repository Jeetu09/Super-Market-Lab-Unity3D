using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DairyLevelQue : MonoBehaviour
{
    [System.Serializable]
    public class QuestionElement
    {
        public GameObject mainPanel;

        public TextMeshProUGUI indicatorText;
        public TextMeshProUGUI counterText;

        public Button submitButton;

        public Animator funFactAnimator;
        public string triggerName;

        public LayerMask clickableLayer;

        public GameObject guidanceUI;

        public int counter = 0;
        public int maxCounter = 3;
    }

    public Camera clickCamera;

    public QuestionElement[] questions;

    int currentQuestion = 0;

    bool canClickObject = false;
    bool answerSelected = false;
    bool isAnswerCorrect = false;

    public GameObject QNAPanel;

    void Start()
    {
        QNAPanel.SetActive(false);

        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].mainPanel.SetActive(false);

            if (questions[i].guidanceUI != null)
                questions[i].guidanceUI.SetActive(false);

            int index = i;
            questions[i].submitButton.onClick.AddListener(() => SubmitAnswer(index));

            UpdateCounterUI(i);
        }
    }

    public void OpenQuestion()
    {
        QuestionElement q = questions[currentQuestion];

        q.mainPanel.SetActive(true);
        q.indicatorText.text = "";

        answerSelected = false;
        isAnswerCorrect = false;
        canClickObject = false;
    }

    public void AnswerButton(bool isCorrect)
    {
        answerSelected = true;
        isAnswerCorrect = isCorrect;

        if (isCorrect)
            questions[currentQuestion].indicatorText.text = "This is correct";
        else
            questions[currentQuestion].indicatorText.text = "Wrong answer";
    }

    void SubmitAnswer(int index)
    {
        if (!answerSelected) return;

        if (!isAnswerCorrect)
        {
            questions[currentQuestion].indicatorText.text = "Select the correct answer to continue";
            return;
        }

        StartCoroutine(PlayAnimation(index));
    }

    IEnumerator PlayAnimation(int index)
    {
        QuestionElement q = questions[index];

        if (q.funFactAnimator != null && q.triggerName != "")
            q.funFactAnimator.SetTrigger(q.triggerName);

        yield return new WaitForSeconds(14f);

        q.mainPanel.SetActive(false);

        if (q.guidanceUI != null)
            q.guidanceUI.SetActive(true);

        canClickObject = true;
    }

    void Update()
    {
        if (!canClickObject) return;

        if (Input.GetMouseButtonDown(0))
        {
            QuestionElement q = questions[currentQuestion];

            if (q.counter >= q.maxCounter) return;

            Ray ray = clickCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, q.clickableLayer))
            {
                hit.collider.gameObject.SetActive(false);

                q.counter++;

                UpdateCounterUI(currentQuestion);

                if (q.counter >= q.maxCounter)
                {
                    canClickObject = false;

                    if (q.guidanceUI != null)
                        q.guidanceUI.SetActive(false);

                    StartCoroutine(StartNextQuestion());
                }
            }
        }
    }

    IEnumerator StartNextQuestion()
    {
        yield return new WaitForSeconds(2f);

        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            OpenQuestion();
        }
        else
        {
            Debug.Log("All elements done");
            QNAPanel.SetActive(true);
        }
    }

    void UpdateCounterUI(int index)
    {
        QuestionElement q = questions[index];

        if (q.counterText != null)
            q.counterText.text = q.counter + " / " + q.maxCounter;
    }
}