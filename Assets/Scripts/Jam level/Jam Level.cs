
using UnityEngine;

public class JamLevel : MonoBehaviour
{
    [Header("Distance Detection Objects")]
    public Transform Player, DistanceDetector;

    [Header("Trolley Code Manager")]
    [SerializeField] private TrollyAttachment TrolleyCodeManager;

    [Header("Counter Manager")]
    bool isJamRange = false;

    [Header("Jame Cam Switch")]
    public GameObject JamCam;
    public GameObject MainCamera;

    void Start()
    {
        JamCam.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(Player.position, DistanceDetector.position);
        if (distance < 3 && isJamRange == false)
        {
            Debug.Log("Player is within 3 units of the Distance Detector.");
            if (Input.GetKeyDown(KeyCode.E))
            {
                TrolleyCodeManager.DisablePlayerControls();
                isJamRange = true;
            }
        }
    }

}
