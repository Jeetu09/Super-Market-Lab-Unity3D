using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerToTrolly : MonoBehaviour
{
    public GameObject Player;
    public GameObject Trolly;
    public GameObject Fruit;
    public Transform HandIKTarget;
    public Transform TrollyFruitSlot;

    public TwoBoneIKConstraint leftHandIK;
    public TwoBoneIKConstraint rightHandIK;

    public Animator animator;
    public string animationTriggerName = "Grab Fruit";

    public GameObject relocationObject;

    public static int flag = 0;  // ✅ make it public
                                 // 0 = detached, 1 = attached
    private bool canRelocate = false;
    private bool isColliding = false;

    public float attachDistance = 3f;
    public float trollyOffsetZ = 1.5f;
    public float trollyOffsetY = 0.5f;

    private float targetWeight = 0f;
    public float weightChangeSpeed = 2f;

    private bool isAnimating = false;
    private float animationDuration = 2f;
    private float animationTimer = 0f;
    private bool moveFruit = false;

    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, Trolly.transform.position);

        // Attach trolly
        if (distance < attachDistance && Input.GetMouseButtonDown(0) && flag == 0)
        {
            Vector3 offset = -Player.transform.forward * trollyOffsetZ + Vector3.up * trollyOffsetY;
            Trolly.transform.position = Player.transform.position + offset;
            Trolly.transform.rotation = Player.transform.rotation;
            Trolly.transform.SetParent(Player.transform);

            targetWeight = 1f;
            flag = 1;
            Debug.Log("🛒 Trolly attached");
        }
        // Detach trolly
        //else if (flag == 1 && Input.GetMouseButtonDown(0))
        //{
        //    Trolly.transform.SetParent(null);
        //    targetWeight = 0f;
        //    flag = 0;
        //    Debug.Log("🛒 Trolly detached");
        //}

        // Start animation if inside trigger and trolly is attached
        if (canRelocate && flag == 1 && !isColliding && Input.GetKeyDown(KeyCode.E))
        {
            Player.transform.position = relocationObject.transform.position;
            Player.transform.rotation = relocationObject.transform.rotation;

            animator.SetTrigger(animationTriggerName);
            isAnimating = true;
            animationTimer = animationDuration;
            Debug.Log("🎬 Animation triggered");
        }

        // Smooth IK blend
        leftHandIK.weight = Mathf.Lerp(leftHandIK.weight, targetWeight, Time.deltaTime * weightChangeSpeed);
        rightHandIK.weight = Mathf.Lerp(rightHandIK.weight, targetWeight, Time.deltaTime * weightChangeSpeed);

        // Attach fruit to hand after animation ends
        if (isAnimating)
        {
            animationTimer -= Time.deltaTime;
            if (animationTimer <= 0f)
            {
                isAnimating = false;

                // Only attach fruit if trolly is still attached
                if (flag == 1)
                {
                    Fruit.transform.SetParent(HandIKTarget);
                    Fruit.transform.localPosition = Vector3.zero;
                    Fruit.transform.localRotation = Quaternion.identity;
                    moveFruit = true;
                    Debug.Log("🍎 Fruit attached to hand");
                }
            }
        }

        // Move fruit to trolly (manual trigger)
        if (moveFruit && Input.GetKeyDown(KeyCode.Space) && flag == 1)
        {
            Fruit.transform.SetParent(TrollyFruitSlot);
            Fruit.transform.localPosition = Vector3.zero;
            Fruit.transform.localRotation = Quaternion.identity;
            moveFruit = false;
            Debug.Log("🍎 Fruit moved to trolly");
        }
    }

    public void EnableRelocationTrigger()
    {
        canRelocate = true;
    }

    public void DisableRelocationTrigger()
    {
        canRelocate = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}
