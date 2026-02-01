using UnityEngine;
using StarterAssets;

public class TrolleyCollision : MonoBehaviour
{
    private CharacterController playerController;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("wall"))
        {
            Debug.Log("Trolley hit wall!");
            playerController.enabled = false; // stop movement
        }
    }

}
