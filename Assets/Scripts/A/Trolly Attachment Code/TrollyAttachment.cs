using StarterAssets; // This lets you access ThirdPersonController class
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TrollyAttachment : MonoBehaviour
{

    public Transform trolleyPlayer;
    [Header("Trolley Settings")]
    public GameObject Trolley;
    public Transform TrolleyReffObj;
    public float triggerDistance = 2f;
    public Vector3 offset;
    public Vector3 attachRotation; // ✅ Added for attach rotation


    [Header("Detach Settings")]
    public GameObject ShopOneZone;
    public Vector3 detachPosition;
    public Vector3 detachRotation; // ✅ Added for detach rotation

    [Header("Rig Settings")]
    public TwoBoneIKConstraint LeftHand;
    public TwoBoneIKConstraint RightHand;
    public float weightChangeSpeed = 2f;

    [Header("Player & Camera")]
    public GameObject Player;
    public CharacterController Playercontroller;
    public GameObject MainCamera;
    public GameObject AlternateCamera;
    public ThirdPersonController playerController;

    [Header("Animation Control")]
    public Animator animationidle;
    public bool hasPlayed = false;
    public bool isAttached = false;

    [Header("Player Relocation")]
    public Vector3 newPlayerPosition;
    public Vector3 newPlayerRotation;

    public HandAnimation handAnimation;

    [Header("Press E popup")]
    public GameObject popupUI;      // UI element (Canvas/Image)
    public GameObject popupUIShop;      // UI element (Canvas/Image)

    public GameObject Arrow;
    public GameObject ArrowShop;


    public GameObject FrndToTrolleyOff;




    public void OnGrabAppleAnimationStart()
    {
        if (handAnimation != null)
            handAnimation.OnGrabAppleAnimationStart();
    }

 

    void Start()
    {
        Arrow.SetActive(true);
        ArrowShop.SetActive(false);
         popupUI.SetActive(true); // Hide at start
        LeftHand.weight = 0f;
        RightHand.weight = 0f;

        if (AlternateCamera != null)
            AlternateCamera.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked; // lock to center
        Cursor.visible = false; // hide cursor
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Trolley.transform.position);
        float shopDistance = Vector3.Distance(transform.position, ShopOneZone.transform.position);

        if (distance < triggerDistance && !isAttached)
{
            
            popupUI.transform.LookAt(MainCamera.transform);
            popupUI.transform.Rotate(0, 180, 0); // Fix reversed look

            if (!hasPlayed && Input.GetKeyDown(KeyCode.E))
            {
                Trolley.transform.SetParent(null);
                Trolley.transform.SetParent(TrolleyReffObj);
                Trolley.transform.localPosition = offset;
                Trolley.transform.localRotation = Quaternion.Euler(attachRotation);

                Debug.Log("Trolley Attached");
                Playercontroller.center = new Vector3(0f, 0.98f, 1f);
                // Playercontroller.radius = 1f;
                isAttached = true;
                hasPlayed = true;

                popupUI.SetActive(false); // hide once attached
                Arrow.SetActive(false);
                ArrowShop.SetActive(true);

                Playercontroller.enabled = false;  
                Player.transform.position = new Vector3(-2.218f, 0.1192473f, 12.579f);  
                Playercontroller.enabled = true;

    }
}



        // Detach trolley near shop
        if (shopDistance < 1f && isAttached)
        {
            popupUIShop.SetActive(true);
            popupUIShop.transform.LookAt(MainCamera.transform);
            popupUIShop.transform.Rotate(0, 180, 0); // Fix reversed look


            if (Input.GetKeyDown(KeyCode.E))
            {
                ArrowShop.SetActive(false);
                //Trolley.transform.position = detachPosition;
                //Trolley.transform.rotation = Quaternion.Euler(detachRotation); // ✅ Apply detach rotation
                DisablePlayerControls();
                SwitchCamera();
                popupUIShop.SetActive(false);
                

                Debug.Log("Trolley Detached near shop");
                isAttached = false;
            }

        }

        else
        {
            popupUIShop.SetActive(false);
        }

        // Animate IK hands
        if (isAttached)
        {
            LeftHand.weight = Mathf.MoveTowards(LeftHand.weight, 1f, Time.deltaTime * weightChangeSpeed);
            RightHand.weight = Mathf.MoveTowards(RightHand.weight, 1f, Time.deltaTime * weightChangeSpeed);
        }
    }

    public void DisablePlayerControls()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //LeftHand.weight = 0f;
        //RightHand.weight = 0f;

        if (playerController != null)
            playerController.enabled = false;

        if (animationidle != null)
        {
            animationidle.SetFloat("Speed", 0f);
            animationidle.SetTrigger("idle");
        }

        if (Player != null)
        {
            Player.transform.position = newPlayerPosition;
            //Player.transform.rotation = Quaternion.Euler(newPlayerRotation);
        }

        // ❌ Removed disabling all MainCamera scripts
        // Let StarterAssets handle camera as usual
    }

    public void DisablePlayerControlsFirDairy()
    {
        
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            LeftHand.weight = 0f;
            RightHand.weight = 0f;

            if (playerController != null)
                playerController.enabled = false;

            if (animationidle != null)
            {
                animationidle.SetFloat("Speed", 0f);
                //animationidle.SetTrigger("idle");
            }
            // Let StarterAssets handle camera as usual
        

    }

    // ✅ Opposite of DisablePlayerControls
    public void EnablePlayerControls()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        LeftHand.weight = 1f;
        RightHand.weight = 1f;

        if (playerController != null)
            playerController.enabled = true;

        if (animationidle != null)
        {
            RightHand.weight = 1f;
            animationidle.SetFloat("Speed", 1f);
            // animationidle.SetTrigger("idle");
        }
    }

    public void SwitchCamera()
    {
        if (MainCamera != null) MainCamera.SetActive(false);
        if (AlternateCamera != null) AlternateCamera.SetActive(true);
    }

    // ✅ Opposite of SwitchCamera
    public void SwitchBackToMainCamera()
    {
        //RightHand.weight = 1f;
        if (AlternateCamera != null) AlternateCamera.SetActive(false);
        if (MainCamera != null) MainCamera.SetActive(true);

        //Debug.Log("Switched back to Main Camera.");

        //Trolley.transform.SetParent(null);
        //Trolley.transform.SetParent(TrolleyReffObj);
        //Trolley.transform.localPosition = offset;
        //Trolley.transform.localRotation = Quaternion.Euler(attachRotation); // ✅ Apply attach rotation


        //Debug.Log("Trolley Attached Agaon");
        //popupUIShop.SetActive(false);

    }
}
