

using UnityEngine;

public class FridgeDetection : MonoBehaviour
{
    public GameObject Player;
    public GameObject Fridge;
    public float detectionRange = 5.0f;

    public GameObject PlayerCam;
    public GameObject SwitchCam;

    [SerializeField] private TrollyAttachment PlayerDis;

    public Animator FridgeAnim;

    [Header("Guiding UI")]
    public GameObject PressEUI;
    public GameObject NavigatingBar;

    [Header("Flags")]
    bool isPlayerInFridgeRange = false;
    // Start is called before the first frame update
    void Start()
    {
        PressEUI.SetActive(false);
        SwitchCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, Fridge.transform.position);
        if (distance <= detectionRange && isPlayerInFridgeRange == false)
        {
            PressEUI.SetActive(true);
            NavigatingBar.SetActive(false);
            Debug.Log("Player is in range of the fridge.");
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                PressEUI.SetActive(false);
                PlayerDis.DisablePlayerControls();
                isPlayerInFridgeRange = true;
                PlayerCam.SetActive(false);
                SwitchCam.SetActive(true);
                FridgeAnim.SetTrigger("FridgeDoorAnim");
                Player.SetActive(false);
            }

        }
    }
}
