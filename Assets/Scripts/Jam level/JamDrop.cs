
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class JamDrop : MonoBehaviour, IDropHandler
{
    public string correctFruit;
    private static int correctCounter = 0; // shared counter
    public GameObject BgImage; // shared background image
    public GameObject button; // shared button

    void Start()
    {
        button.SetActive(false); // hide the button at the start
    }

    public void OnDrop(PointerEventData eventData)
    {

        JamDrag item = eventData.pointerDrag.GetComponent<JamDrag>();
        if (item == null) return;

        if (item.DiaName == correctFruit && !item.isDropped)
        {
            item.transform.position = transform.position;
            item.isDropped = true;

            correctCounter++;
            Debug.Log("Correct Counter: " + correctCounter);
            if (correctCounter == 10)
            {
                button.SetActive(false);
                Debug.Log("All fruits placed!");
                button.SetActive(true);
            }
        }
        else
        {
            item.isDropped = false;
            Debug.Log("Wrong fruit! Try again.");
        }
    }

    public void CorrectButton()
    {
        BgImage.SetActive(false);
    }
}
