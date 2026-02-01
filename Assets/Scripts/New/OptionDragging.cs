using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class OptionDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TextMeshProUGUI OptionOne;
    public TextMeshProUGUI OptionTwo;
    public TextMeshProUGUI OptionThree;
    public TextMeshProUGUI OptionFour;
    public TextMeshProUGUI feedbackText;   // added
    [Header("UI")]
    public GameObject Dialog1;
    public GameObject Dialog2;
    public GameObject PlayerDia;

    public GameObject DropPanel;

    private RectTransform draggingOption;
    private Vector3 originalPosition;
    private Transform originalParent;
    private bool dropLocked = false;

    [Header("UI")]
    public GameObject MainUI;

    void Start(){
        Dialog1.SetActive(true);
        Dialog2.SetActive(false);
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.TryGetComponent(out RectTransform rt)) return;

        draggingOption = rt;
        originalPosition = draggingOption.position;
        originalParent = draggingOption.parent;

        draggingOption.SetAsLastSibling(); // Bring to front
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingOption != null)
        {
            draggingOption.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dropLocked)
        {
            ReturnToOriginalPosition();
            return;
        }

        // Check if dropped over DropPanel
        if (RectTransformUtility.RectangleContainsScreenPoint(
            DropPanel.GetComponent<RectTransform>(), Input.mousePosition, eventData.enterEventCamera))
        {
            HandleDrop();
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    private void HandleDrop()
    {
        string name = draggingOption.name;

        draggingOption.SetParent(DropPanel.transform);

        if (name == OptionTwo.name)
        {
            // ✅ Correct Option
            draggingOption.GetComponent<TextMeshProUGUI>().color = new Color(0f, 0.5f, 0f);

            dropLocked = true;
            feedbackText.text = "Correct answer";
            Dialog2.SetActive(true);
            Dialog1.SetActive(false);
            // ❌ Disable all further dragging (raycast disable)
            DisableAllDragging();
            StartCoroutine(DisablePlayerDiaAfterDelay(2f));
        }
        else
        {
            // ❌ Incorrect Option
            draggingOption.GetComponent<TextMeshProUGUI>().color = Color.red;
            StartCoroutine(ResetAfterDelay(draggingOption.gameObject));
            feedbackText.text = "Incorrect answer";
        }
    }
    private IEnumerator DisablePlayerDiaAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerDia.SetActive(false);
        FindObjectOfType<HandAnimation>().HandAnim();
    }

private void DisableAllDragging()
    {
        // Disable raycast so no options are interactive
        OptionOne.raycastTarget = false;
        OptionTwo.raycastTarget = false;
        OptionThree.raycastTarget = false;
        OptionFour.raycastTarget = false;
    }


    private IEnumerator ResetAfterDelay(GameObject droppedOption)
    {
        yield return new WaitForSeconds(2f);
        // droppedOption.GetComponent<TextMeshProUGUI>().color = Color.black;
        droppedOption.transform.SetParent(originalParent);
        droppedOption.transform.position = originalPosition;
    }

    private void ReturnToOriginalPosition()
    {
        draggingOption.position = originalPosition;
        draggingOption.SetParent(originalParent);
    }
}


