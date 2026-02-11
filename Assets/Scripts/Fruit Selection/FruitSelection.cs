
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruitSelection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TrollyAttachment trollyAttachment;
    public GameObject GuidingText;

    [Header("Camera")]
    [SerializeField] private Camera clickCamera;

    [Header("UI")]
    [SerializeField] private GameObject bgImage;

    [Header("Fruits")]
    [SerializeField] private GameObject[] apples;
    [SerializeField] private GameObject[] bananas;

    [Header("Green Indicators")]
    [SerializeField] private Image greenApple;
    [SerializeField] private Image greenBanana;

    [Header("Congratulations Text")]
    public GameObject conganimation;

    [Header("Jam UI")]
    public GameObject JamUI;

    [Header("Fruit counter")]
    public TextMeshProUGUI appleCounterText;
    public TextMeshProUGUI bananaCounterText;

    private bool canClick = false;
    private bool taskCompleted = false; // ⭐ prevents multiple triggers

    private int appleCount = 0;
    private int bananaCount = 0;

    private const int appleLimit = 1;
    private const int bananaLimit = 4;

    void Start()
    {
        JamUI.SetActive(false);
        conganimation.SetActive(false);

        if (greenApple) greenApple.gameObject.SetActive(false);
        if (greenBanana) greenBanana.gameObject.SetActive(false);

        UpdateCounterUI();
    }

    void Update()
    {
        if (!canClick && bgImage != null && !bgImage.activeSelf)
            canClick = true;

        if (!canClick || clickCamera == null || taskCompleted)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = clickCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clicked = hit.collider.gameObject;

                // 🍎 Apple
                if (appleCount < appleLimit && IsApple(clicked))
                {
                    appleCount++;
                    clicked.SetActive(false);

                    if (appleCount == appleLimit && greenApple)
                    {
                        greenApple.gameObject.SetActive(true);
                        appleCounterText.gameObject.SetActive(false);
                    }

                    UpdateCounterUI();
                }

                // 🍌 Banana
                else if (bananaCount < bananaLimit && IsBanana(clicked))
                {
                    bananaCount++;
                    clicked.SetActive(false);

                    if (bananaCount == bananaLimit && greenBanana)
                    {
                        greenBanana.gameObject.SetActive(true);
                        bananaCounterText.gameObject.SetActive(false);
                    }

                    UpdateCounterUI();
                }

                CheckAllFruits();
            }
        }
    }

    void UpdateCounterUI()
    {
        appleCounterText.text = appleCount + "/" + appleLimit;
        bananaCounterText.text = bananaCount + "/" + bananaLimit;
    }

    bool IsApple(GameObject obj)
    {
        foreach (GameObject apple in apples)
        {
            if (apple != null && obj == apple && apple.activeSelf)
                return true;
        }
        return false;
    }

    bool IsBanana(GameObject obj)
    {
        foreach (GameObject banana in bananas)
        {
            if (banana != null && obj == banana && banana.activeSelf)
                return true;
        }
        return false;
    }

    void CheckAllFruits()
    {
        if (!taskCompleted && appleCount >= appleLimit && bananaCount >= bananaLimit)
        {
            taskCompleted = true; // ⭐ stops repeating

            Debug.Log("✅ Task Completed!");
            GuidingText.SetActive(false);

            Invoke(nameof(ResumeControls), 2f);
        }
    }

    public void ResumeControls()
    {
        CancelInvoke(); // ⭐ extra safety

        if (trollyAttachment != null)
        {
            trollyAttachment.EnablePlayerControls();
            trollyAttachment.SwitchBackToMainCamera();
        }

        conganimation.SetActive(true);

        Invoke(nameof(JamEvent), 10f);
    }

    public void JamEvent()
    {
        JamUI.SetActive(true);

        if (trollyAttachment != null)
            trollyAttachment.DisablePlayerControls();
    }

    public void JamNoButton()
    {
        JamUI.SetActive(false);

        if (trollyAttachment != null)
            trollyAttachment.EnablePlayerControls();
    }
}
