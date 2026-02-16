
using UnityEngine;


public class CarAnimEnd : MonoBehaviour
{
    [Header("Car Player disable")]
    public GameObject CarPlayerDisable;

    [Header("Friend enable")]
    public GameObject frendEnable;

    [Header("Animation End Event")]
    public GameObject CamOne;
    public GameObject CamTwo;
    public GameObject CamtwoPanel;
    public GameObject ThirdCam;
    public GameObject MainPlayerEnable;

    [Header("Wheel Rotation")]
    public Transform WheelFL;
    public Transform WheelFR;
    public Transform WheelBL;
    public Transform WheelBR;
    public float wheelRotationSpeed = 360f;

    public Animator DarknessInn;

    public Postprocessing PostProcessingChange;

    // ✅ Start as false
    public bool rotateWheels = false;

    public Animator MainGatePoleAnimation;

    void Start()
    {
        CamtwoPanel.SetActive(false);
        frendEnable.SetActive(false);
    }

    void Update()
    {
        if (!rotateWheels) return;

        float rot = wheelRotationSpeed * Time.deltaTime;

        WheelFL.Rotate(rot, 0, 0);
        WheelFR.Rotate(rot, 0, 0);
        WheelBL.Rotate(rot, 0, 0);
        WheelBR.Rotate(rot, 0, 0);
    }

    public void OnAnimationEnd()
    {
        DarknessInn.SetTrigger("DarknessAgain");

        rotateWheels = false;

        CamOne.SetActive(false);
        CamTwo.SetActive(true);

        Debug.Log("Animation finished, wheels stopped");

        Invoke("PanelDelay", 2f);
    }

    public void EndOfMotionScene()
    {
        MainGatePoleAnimation.SetTrigger("MainGateClose");
        ThirdCam.SetActive(false);
        MainPlayerEnable.SetActive(true);
        CarPlayerDisable.SetActive(false);
        frendEnable.SetActive(true);
        PostProcessingChange.PlayerStartedEffect();
        rotateWheels = false;
    }

    public void PanelDelay()
    {
        CamtwoPanel.SetActive(true);
    }
}
