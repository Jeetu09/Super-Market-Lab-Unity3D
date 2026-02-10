

using UnityEngine;
using UnityEngine.UI;

public class FruitSelection : MonoBehaviour
{

    TrollyAttachment trollyAttachment;
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

    private bool canClick = false;

    private int appleCount = 0;
    private int bananaCount = 0;

    private const int appleLimit = 1;
    private const int bananaLimit = 4;

    [Header("Congratulations Text")]
    public GameObject conganimation;

    void Start()
    {
        if (greenApple != null) greenApple.gameObject.SetActive(false);
        if (greenBanana != null) greenBanana.gameObject.SetActive(false);

        if (clickCamera == null)
            Debug.LogError("Click Camera is NOT assigned!");

        if (bgImage == null)
            Debug.LogError("BgImage is NOT assigned!");
    }

    void Update()
    {
        if (!canClick && bgImage != null && !bgImage.activeSelf)
            canClick = true;

        if (!canClick || clickCamera == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = clickCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clicked = hit.collider.gameObject;

                // ✅ Apple
                if (appleCount < appleLimit && IsApple(clicked))
                {
                    appleCount++;
                    clicked.SetActive(false);
                    Debug.Log("Apples Selected: " + appleCount);

                    if (appleCount == appleLimit && greenApple != null)
                        greenApple.gameObject.SetActive(true);
                }

                // ✅ Banana
                else if (bananaCount < bananaLimit && IsBanana(clicked))
                {
                    bananaCount++;
                    clicked.SetActive(false);
                    Debug.Log("Bananas Selected: " + bananaCount);

                    if (bananaCount == bananaLimit && greenBanana != null)
                        greenBanana.gameObject.SetActive(true);
                }

                CheckAllFruits();
            }
        }
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
        if (appleCount >= appleLimit && bananaCount >= bananaLimit)
        {
            Debug.Log("✅ Task Completed!");
            GuidingText.SetActive(false);
            Invoke("ResumeControls", 2f);
        }
    }

    public void ResumeControls()
    {
        trollyAttachment = FindObjectOfType<TrollyAttachment>();
        trollyAttachment.EnablePlayerControls();
        trollyAttachment.SwitchBackToMainCamera();
        conganimation.SetActive(true);
    }
}
