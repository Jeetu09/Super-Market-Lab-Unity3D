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

        public int counter = 0;
        public int maxCounter = 3;
    }

    public Camera clickCamera;   // common camera (drag in inspector)

    public QuestionElement[] questions;

    int currentQuestion = 0;

    bool canClickObject = false;
    bool answerSelected = false;

    void Start()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].mainPanel.SetActive(false);

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
        canClickObject = false;
    }

    public void AnswerButton(bool isCorrect)
    {
        if (isCorrect)
            questions[currentQuestion].indicatorText.text = "This is correct";
        else
            questions[currentQuestion].indicatorText.text = "Wrong answer";

        answerSelected = true;
    }

    void SubmitAnswer(int index)
    {
        if (!answerSelected) return;

        StartCoroutine(PlayAnimation(index));
    }

    IEnumerator PlayAnimation(int index)
    {
        QuestionElement q = questions[index];

        if (q.funFactAnimator != null && q.triggerName != "")
        {
            q.funFactAnimator.SetTrigger(q.triggerName);
        }

        // wait 14 seconds
        yield return new WaitForSeconds(14f);

        // close panel after animation
        q.mainPanel.SetActive(false);

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
        }
    }

    void UpdateCounterUI(int index)
    {
        QuestionElement q = questions[index];

        if (q.counterText != null)
            q.counterText.text = q.counter + " / " + q.maxCounter;
    }
}