
using UnityEngine;
using UnityEngine.EventSystems;

public class JamDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string DiaName;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private Vector3 startPos;

    [HideInInspector] public bool isDropped = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDropped) return;

        // Save position at drag start (VERY IMPORTANT)
        startPos = rectTransform.position;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDropped) return;

        rectTransform.position = eventData.position;  // Your original logic
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!isDropped)
        {
            rectTransform.position = startPos; // Reset correctly
            Debug.Log("Wrong placement!");
        }
    }
}
