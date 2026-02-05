
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public string correctFruit;

    public void OnDrop(PointerEventData eventData)
    {
        DragItem item = eventData.pointerDrag.GetComponent<DragItem>();

        if (item == null) return;

        if (item.fruitName == correctFruit)
        {
            item.transform.position = transform.position;
            item.isDropped = true;

            Debug.Log("Correct placement!");
        }
        else
        {
            item.isDropped = false;
            item.transform.position = item.GetComponent<RectTransform>().position;
            Debug.Log("Wrong fruit! Try again.");
        }
    }
}
