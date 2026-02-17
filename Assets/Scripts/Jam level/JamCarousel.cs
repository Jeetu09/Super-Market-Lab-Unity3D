using System.Collections;
using TMPro;
using UnityEngine;

public class JamCarousel : MonoBehaviour
{
    public RectTransform[] jams;

    [Header("UI Text")]
    public TextMeshProUGUI resultText;

    [Header("Center Scale")]
    public float centerWidth = 1.2f;
    public float centerHeight = 1.2f;

    [Header("Side Scale")]
    public float sideWidth = 0.8f;
    public float sideHeight = 0.8f;

    public float spacing = 350f;
    public float smoothSpeed = 8f;

    public float bounceAmount = 80f;
    public float bounceTime = 0.15f;

    private int currentIndex = 0;

    private Vector2[] targetPositions;
    private Vector3[] targetScales;
    private bool isBouncing = false;

    void Start()
    {
        targetPositions = new Vector2[jams.Length];
        targetScales = new Vector3[jams.Length];

        resultText.text = "";   // Clear at start
        UpdateTargets();
    }

    void Update()
    {
        for (int i = 0; i < jams.Length; i++)
        {
            jams[i].anchoredPosition =
                Vector2.Lerp(jams[i].anchoredPosition,
                             targetPositions[i],
                             Time.deltaTime * smoothSpeed);

            jams[i].localScale =
                Vector3.Lerp(jams[i].localScale,
                             targetScales[i],
                             Time.deltaTime * smoothSpeed);
        }
    }

    public void BuyItem()
    {
        string selectedName = jams[currentIndex].name;

        if (selectedName == "Ergo Jam")
        {
            Debug.Log("Correct Product");
            //resultText.text = "Correct Product";
        }
        else
        {
            Debug.Log("Wrong Jam");
            resultText.text = "This is not the Jam that I want.";
        }
    }

    public void Next()
    {
        if (currentIndex < jams.Length - 1)
        {
            currentIndex++;
            resultText.text = "";   // Remove wrong text
            UpdateTargets();
        }
        else
        {
            StartCoroutine(Bounce(-1));
        }
    }

    public void Previous()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            resultText.text = "";   // Remove wrong text
            UpdateTargets();
        }
        else
        {
            StartCoroutine(Bounce(1));
        }
    }

    IEnumerator Bounce(int direction)
    {
        if (isBouncing) yield break;
        isBouncing = true;

        for (int i = 0; i < jams.Length; i++)
            targetPositions[i] += new Vector2(direction * bounceAmount, 0);

        yield return new WaitForSeconds(bounceTime);

        UpdateTargets();
        isBouncing = false;
    }

    void UpdateTargets()
    {
        for (int i = 0; i < jams.Length; i++)
        {
            int offset = i - currentIndex;

            targetPositions[i] = new Vector2(offset * spacing, 0);

            if (i == currentIndex)
                targetScales[i] = new Vector3(centerWidth, centerHeight, 1f);
            else
                targetScales[i] = new Vector3(sideWidth, sideHeight, 1f);
        }
    }
}
