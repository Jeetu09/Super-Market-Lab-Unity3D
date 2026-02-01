
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public GameObject relocationObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == relocationObject)
        {
            Debug.Log(" Player collided with relocationObject");
        }
    }
}
