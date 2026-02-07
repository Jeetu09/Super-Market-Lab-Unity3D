using System.Collections;
using UnityEngine;
using TMPro;

public class DoorOpeningScript : MonoBehaviour
{
    [Header("References")]
    public GameObject Player;
    public GameObject Door;
    public GameObject Mat;
    public Animator animator;  // Door left
    public Animator animator2; // Door right

    [Header("Distances")]
    public float triggerDistance = 3f;
    public float triggerDistance2 = 1f;
    private bool hasPlayed = false;

    private void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, Door.transform.position);
        float distance2 = Vector3.Distance(Player.transform.position, Mat.transform.position);

        if (distance < triggerDistance)
        {
            animator.SetTrigger("Open");   // Trigger name in Animator
            animator2.SetTrigger("OpenR"); // Trigger name in Animator
        }

        if (distance2 < triggerDistance2 && !hasPlayed)
        {
            animator.SetTrigger("Close");   // Trigger name in Animator
            animator2.SetTrigger("CloseR"); // Trigger name in Animator
            hasPlayed = true;
        }
    }
}
