// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;


// public class DraggableWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//    private RectTransform rectTransform;
//     private CanvasGroup canvasGroup;
//     public Transform originalParent;

//     void Awake()
//     {
//         rectTransform = GetComponent<RectTransform>();
//         canvasGroup = GetComponent<CanvasGroup>();
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         originalParent = transform.parent;
//         canvasGroup.blocksRaycasts = false;
//         transform.SetParent(originalParent.parent); 
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         rectTransform.anchoredPosition += eventData.delta;
//     }

//     public void OnEndDrag(PointerEventData eventData)
//     {
//         canvasGroup.blocksRaycasts = true;

       
//         if (transform.parent == originalParent.parent)
//         {
//             transform.SetParent(originalParent);
//         }
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // ✅ Required for TextMeshProUGUI


public class DraggableWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Transform originalParent;
    public TextMeshProUGUI feedbackText;   // added

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(originalParent.parent); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;       //added
        canvasGroup.blocksRaycasts = true;

        TextMeshProUGUI droppedText = droppedObject.GetComponentInChildren<TextMeshProUGUI>(); // added
       
        if (transform.parent == originalParent.parent)
        {
            transform.SetParent(originalParent);
            Debug.Log("elsewhere");                             //added
            feedbackText.text = "Incorrect position";
        }
    }
}

