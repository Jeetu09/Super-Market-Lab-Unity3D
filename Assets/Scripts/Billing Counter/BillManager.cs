
using UnityEngine;

public class BillManager : MonoBehaviour
{
    [Header("Distance Counter")]
    public Transform Player;
    public Transform Counter;
    public float DistanceThreshold = 2f;
    [SerializeField] private TrollyAttachment PlayerDis;

    bool isPlayerClose = false;

    [Header("Cam Switch")]
    public GameObject Cam1;
    public GameObject MainCam;
    public GameObject MainPlayer;

    void Start()
    {
        Cam1.SetActive(false);
    }


    void Update()
    {
        float distance = Vector3.Distance(Player.position, Counter.position);
        if (distance <= DistanceThreshold  && isPlayerClose == false)
        {
            Debug.Log("Player is close enough to the counter.");
            // You can add additional logic here, such as enabling interaction or displaying a message.
            if(Input.GetKeyDown(KeyCode.E))
            {
                DiablePlayer();
            }
        }

    }

    public void DiablePlayer()
    {
        MainPlayer.SetActive(false);
        PlayerDis.DisablePlayerControls();
        isPlayerClose = true;
        Cam1.SetActive(true);
        MainCam.SetActive(false);
    }

    public void EnablePlayer()
    {
        MainPlayer.SetActive(true);
        PlayerDis.EnablePlayerControls();
        isPlayerClose = false;
        Cam1.SetActive(false);
        MainCam.SetActive(true);
    }
}
