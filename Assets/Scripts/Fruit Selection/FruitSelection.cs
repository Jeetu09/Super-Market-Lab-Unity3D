
using UnityEngine;

public class FruitSelection : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera clickCamera; // drag the active camera

    [Header("UI")]
    [SerializeField] private GameObject bgImage;

    [Header("Fruits")]
    [SerializeField] private GameObject[] apples;
    [SerializeField] private GameObject[] bananas;

    private bool canClick = false;

    private int appleCount = 0;
    private int bananaCount = 0;

    void Start()
    {
        if (clickCamera == null)
            Debug.LogError("Click Camera is NOT assigned!");

        if (bgImage == null)
            Debug.LogError("BgImage is NOT assigned!");
    }

    void Update()
    {
        // Allow clicking once BG is disabled
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

                if (IsApple(clicked))
                {
                    appleCount++;
                    clicked.SetActive(false);
                    Debug.Log("Apples Selected: " + appleCount);
                }
                else if (IsBanana(clicked))
                {
                    bananaCount++;
                    clicked.SetActive(false);
                    Debug.Log("Bananas Selected: " + bananaCount);
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
        if (appleCount == apples.Length && bananaCount == bananas.Length)
        {
            Debug.Log("✅ All fruits collected!");
        }
    }
}
