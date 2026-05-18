
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string fruitName;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 startPos;

    [HideInInspector] public bool isDropped = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition; // store start position
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDropped) return;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDropped) return;

        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!isDropped)
        {
            rectTransform.anchoredPosition = startPos; // return to start
            Debug.Log("Wrong placement!");
        }
    }
}