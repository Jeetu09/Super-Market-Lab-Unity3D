
using UnityEngine;

public class SecondLevel : MonoBehaviour
{
    public Transform player;
    public CharacterController playerController;

    public GameObject trolley;
    public Transform shopZone;

    public Vector3 newPlayerPosition;
    public Vector3 newPlayerRotation;

    public Vector3 detachPosition;
    public Vector3 detachRotation;

    public float triggerDistance = 1.5f;

    public GameObject PlayerCam;
    public GameObject FruitCam;


    void Update()
    {
        float distance = Vector3.Distance(player.position, shopZone.position);
        

        if (distance < triggerDistance
            &&
        GetComponent<TrollyAttachment>().isAttached == true)
        {
            RelocatePlayerAndTrolley();
        }
    }

    void RelocatePlayerAndTrolley()
    {
        playerController.enabled = false;

        player.position = newPlayerPosition;
        player.rotation = Quaternion.Euler(newPlayerRotation);

        trolley.transform.position = detachPosition;
        trolley.transform.rotation = Quaternion.Euler(detachRotation);

        playerController.enabled = true;

        GetComponent<TrollyAttachment>().DisablePlayerControlsFirDairy();
        PlayerCam.SetActive(false);
        FruitCam.SetActive(true);
    }
}
