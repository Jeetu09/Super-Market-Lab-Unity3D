using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    private CanvasGroup canvasGroup;
 

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); // Move to top of canvas
        canvasGroup.blocksRaycasts = false;  // Allow raycast to detect drop targets
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // If dropped somewhere invalid, return to original position
        if (transform.parent == transform.root)
        {
            transform.SetParent(originalParent);

        }
    }

    public void LockItem()
    {
        Debug.Log("LockItem called");
        canvasGroup.blocksRaycasts = false;
        Destroy(this); // Optionally disable further dragging
    }
}
