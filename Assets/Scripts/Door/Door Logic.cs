
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public Animator DoorAnimator; // Animator on the actual door

    public float triggerDistance = 3f;
    private bool hasPlayed = false;

    void OnCollisionEnter(Collision collinfo)
    {
        if(collinfo.collider.name == "Mat")
        {
            DoorAnimator.SetTrigger("EntryDoorAnimation");
            Debug.Log("Hello");
        }
    }

    //void Update()
    //{
    //    float distance = Vector3.Distance(Player.transform.position, DoorMat.transform.position);

    //    if (distance <= triggerDistance && !hasPlayed)
    //    {
    //        DoorAnimator.SetTrigger("EntryDoorAnimation");
    //        hasPlayed = true;
    //    }
    //}
}
