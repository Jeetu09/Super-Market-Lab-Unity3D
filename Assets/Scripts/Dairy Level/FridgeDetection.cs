
using UnityEngine;

public class FridgeDetection : MonoBehaviour
{
    public GameObject Player;
    public GameObject Fridge;
    public float detectionRange = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, Fridge.transform.position);
        if (distance <= detectionRange)
        {
            Debug.Log("Fridge detected!");
            // You can add additional logic here, such as opening the fridge or triggering an animation
        }
    }
}
