using System.Collections;
using UnityEngine;

public class JamCarousel : MonoBehaviour
{
    public RectTransform[] jams;

    public float sideScale = 0.7f;
    public float centerScale = 1.2f;

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

    public void Next()
    {
        if (currentIndex < jams.Length - 1)
        {
            currentIndex++;
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

        // move slightly
        for (int i = 0; i < jams.Length; i++)
            targetPositions[i] += new Vector2(direction * bounceAmount, 0);

        yield return new WaitForSeconds(bounceTime);

        // move back
        UpdateTargets();

        isBouncing = false;
    }

    public void BuyItem()
    {
        Debug.Log("Item Added: " + jams[currentIndex].name);
    }

    void UpdateTargets()
    {
        for (int i = 0; i < jams.Length; i++)
        {
            int offset = i - currentIndex;
            targetPositions[i] = new Vector2(offset * spacing, 0);

            if (i == currentIndex)
                targetScales[i] = Vector3.one * centerScale;
            else
                targetScales[i] = Vector3.one * sideScale;
        }
    }
}
