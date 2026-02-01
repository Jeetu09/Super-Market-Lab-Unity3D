using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;

public class HandAnimation : MonoBehaviour
{
    [Header("References")]
    [Header("Trolley System")]
    public TrollyAttachment handAnimationTrolley;
    public bool ikzero = false;

    public Transform Player;
    public TwoBoneIKConstraint ShopHand;
    public TwoBoneIKConstraint PlayerHand;   // IK Control
    public MultiAimConstraint Head;

    [Header("Hand & Fruit")]
    public GameObject Fruit;
    public GameObject PlayerHandObj;         // Hand bone reference

    [Header("Animations")]
    public Animator PickAnimation;
    public Animator grabFruitAnimation;
    public Animator putapple;

    [Header("Settings")]
    public float WeightChangeSpeed = 1f;

    [Header("Trolley Fruit")]
    public GameObject TrolleyFruit;

    private bool handIK = false;
    private bool applePlaced = false;   // ✅ Prevent repeating
     public Animator FunFactUI;

    void Start()
    {
        ShopHand.weight = 0f;
        Head.weight = 0f;
        PlayerHand.weight = 0f;  // start disabled
        TrolleyFruit.SetActive(false);
    }

    public void HandAnim()
    {
        // Instantly enable ShopHand
        ShopHand.weight = 1f;

        // Smoothly increase Head weight

        Debug.Log("Picking process is started");
        PickAnimation.SetTrigger("GrabAnimationTrigger");
        Invoke("SmoothHeadWeight", 2.15f);
    }

    public void SmoothHeadWeight()
    {
       
            Head.weight = Mathf.MoveTowards(Head.weight, 1f, Time.deltaTime * WeightChangeSpeed);
            // Debug.Log("Head Weight is: " + Head.weight.ToString("F2"));

            if (    handIK == false)
            {
                Debug.Log("Completed");

                grabFruitAnimation.SetTrigger("GrabApple");
                handIK = true;
            }

        
    }

    // Called from Animation Event in GrabApple animation
    public void OnGrabAppleAnimationStart()
    {
        if (ikzero == false)
        {
            ikzero = true;
            
            Debug.Log("IK is 0");

            // Move fruit to PlayerHandObj position
            Invoke("FruitToHand", 1f);

            // Rotate player after 1 second
            Invoke("PlayerRotation", 1f);
        }
    }

    public void FruitToHand()
    {
         Fruit.transform.position = PlayerHandObj.transform.position;

            // Parent fruit to PlayerHandObj (so it follows hand movement)
            Fruit.transform.SetParent(PlayerHandObj.transform);
            Fruit.transform.localRotation = Quaternion.Euler(0, 115, 0);
    }

    public void PlayerRotation()
    {
        Head.weight = 1f;
        Player.transform.eulerAngles = new Vector3(0, 1, 0);
        Invoke("PutApple", 0.5f);
    }

    public void PutApple()
    {
        if (applePlaced) return;  // ✅ Only once!
        

        putapple.SetTrigger("PutFruit");


        // Start coroutine to delay fruit placement
        StartCoroutine(PlaceFruitAfterDelay(2f));
        
    }

    private IEnumerator PlaceFruitAfterDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);

        Fruit.SetActive(false);
        TrolleyFruit.SetActive(true);

        applePlaced = true;  
        Debug.Log("Fruit placed in trolley and stays there.");

        // ✅ Re-enable player controls & switch back camera
        if (handAnimationTrolley != null)
        {
            // PlayerHand.weight = 1f;
            handAnimationTrolley.EnablePlayerControls();
            handAnimationTrolley.SwitchBackToMainCamera();

            // ✅ Smoothly restore PlayerHand IK back to 1
            StartCoroutine(SmoothSetIK(PlayerHand, 10f, 5f));

            // ✅ Attach trolley again
            // TrolleyFruit.transform.SetParent(null);
            // TrolleyFruit.transform.SetParent(handAnimationTrolley.TrolleyReffObj);
            // TrolleyFruit.transform.localPosition = handAnimationTrolley.offset;
            // TrolleyFruit.transform.localRotation = Quaternion.Euler(handAnimationTrolley.attachRotation);

            Debug.Log("Trolley Re-attached after placing fruit.");
            FunFactUI.SetTrigger("funinfo");
        }
    }

    // ✅ Helper coroutine to smoothly move IK weight
    private IEnumerator SmoothSetIK(TwoBoneIKConstraint ik, float target, float speed)
    {
        while (Mathf.Abs(ik.weight - target) > 0.01f)
        {
            ik.weight = Mathf.MoveTowards(ik.weight, target, Time.deltaTime * speed);
            yield return null;
        }
        ik.weight = target; // final snap
    }
}
