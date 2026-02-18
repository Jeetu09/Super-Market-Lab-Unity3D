using UnityEngine;

public class JamLevel : MonoBehaviour
{
    [Header("Distance Detection Objects")]
    public Transform Player, DistanceDetector;

    [Header("Trolley Code Manager")]
    [SerializeField] private TrollyAttachment TrolleyCodeManager;

    bool isJamRange = false;

    [Header("Camera Switch")]
    public GameObject JamCam;
    public GameObject MainCamera;

    [Header("Arrow Object")]
    public GameObject Arrow;
    public GameObject PressEUI;

    void Start()
    {
        JamCam.SetActive(false);
        Arrow.SetActive(true);
        PressEUI.SetActive(false);
    }

    void Update()
    {
        float sqrDistance = (Player.position - DistanceDetector.position).sqrMagnitude;

        if (sqrDistance < 9f && !isJamRange)
        {
            if (!PressEUI.activeSelf)
            {
                PressEUI.SetActive(true);
                PressEUI.transform.LookAt(MainCamera.transform);
                PressEUI.transform.Rotate(0, 180, 0);
            }

            Arrow.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                JamCam.SetActive(true);
                MainCamera.SetActive(false);
                PressEUI.SetActive(false);
                Arrow.SetActive(false);
                TrolleyCodeManager.DisablePlayerControls();
                isJamRange = true;
            }
        }
        else
        {
            PressEUI.SetActive(false);
        }
    }

    public void ExitJam()
    {
        TrolleyCodeManager.EnablePlayerControls();
        JamCam.SetActive(false);
        MainCamera.SetActive(true);
    }
}
