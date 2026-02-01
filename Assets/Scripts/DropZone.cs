using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro; // ✅ Required for TextMeshProUGUI

public class DropZone : MonoBehaviour, IDropHandler
{
    public string correctAnswer = "1 kilo apples";
    public TextMeshProUGUI feedbackText; // Optional: For UI 
    public DraggableWord drag;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null)
        {
            // Re-parent the dropped object to this drop zone
            droppedObject.transform.SetParent(transform);

            // Check if the text on the dropped object is correct
            TextMeshProUGUI droppedText = droppedObject.GetComponentInChildren<TextMeshProUGUI>();

            if (droppedText != null)
            {
                if (droppedText.text.Trim().ToLower() == correctAnswer.Trim().ToLower())
                {
                    Debug.Log("Correct answer dropped!");
                    droppedText.color = Color.green;
                    if (feedbackText != null) feedbackText.text = "✅ Correct!";
                    drag.enabled = false;
                    
                }
                else
                {
                    Debug.Log("Wrong answer.");
                    droppedText.color = Color.red;
                    if (feedbackText != null) feedbackText.text = "❌ Wrong!";
                }
            }
        }
    }
}



// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
// using TMPro;

// public class DropZone : MonoBehaviour, IDropHandler
// {
//     public string correctAnswer = "1 kilo apples";
//     public TextMeshProUGUI feedbackText;

//     public void OnDrop(PointerEventData eventData)
//     {
//         GameObject droppedObject = eventData.pointerDrag;

//         if (droppedObject != null)
//         {
//             TextMeshProUGUI droppedText = droppedObject.GetComponentInChildren<TextMeshProUGUI>();
//             DragItem dragItem = droppedObject.GetComponent<DragItem>();

//             if (droppedText != null && dragItem != null)
//             {
//                 if (droppedText.text.Trim().ToLower() == correctAnswer.Trim().ToLower())
//                 {
//                     Debug.Log("Correct answer dropped!");
//                     droppedText.color = Color.green;

//                     droppedObject.transform.SetParent(transform); // Snap to DropZone
//                     dragItem.LockItem(); // Prevent further dragging

//                     if (feedbackText != null) feedbackText.text = "✅ Correct!";
//                 }
//                 else
//                 {
//                     Debug.Log("Wrong answer.");
//                     droppedText.color = Color.red;

//                     // Send back to original position
//                     droppedObject.transform.SetParent(dragItem.originalParent);

//                     if (feedbackText != null) feedbackText.text = "❌ Wrong!";
//                 }
//             }
//         }
//     }
// }
