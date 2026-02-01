using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class DetectorCollision : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public GameObject relocationObject;
    public MultiAimConstraint HeadIk;

    public string animationTriggerName = "Grab Fruit";
    public float targetWeight = 1f;
    public float weightChangeSpeed = 2f;
    public float animationDuration = 2f;

    public GameObject fruit;
    public Transform handIKTarget;
    public Transform trollyContainer;

    private bool canRelocate = false;
    private bool isAnimating = false;
    private bool reduceIK = false;
    private bool hasPlayedOnce = false; // ✅ Prevent animation replay

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && PlayerToTrolly.flag == 1)
        {
            canRelocate = true;
            Debug.Log("🔵 Player ENTERED trigger zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            canRelocate = false;
            Debug.Log("🔴 Player EXITED trigger zone");
        }
    }

    void Update()
    {
        if (canRelocate && Input.GetKeyDown(KeyCode.E) && PlayerToTrolly.flag == 1 && !hasPlayedOnce)
        {
            // Relocate player
            player.transform.position = relocationObject.transform.position;
            player.transform.rotation = relocationObject.transform.rotation;
            Debug.Log("📍 Player relocated");

            // Start animation
            animator.SetTrigger(animationTriggerName);
            hasPlayedOnce = true; // ✅ Only once
            isAnimating = true;
            reduceIK = false;

            // Start fruit routines
            StartCoroutine(AttachFruitAfterDelay(2f));                        // Attach after 2s
            StartCoroutine(DetachFruitBeforeEnd(animationDuration - 1.5f));  // Detach 1.5s before end
        }

        // Increase IK weight
        if (isAnimating && !reduceIK)
        {
            HeadIk.weight = Mathf.MoveTowards(HeadIk.weight, targetWeight, Time.deltaTime * weightChangeSpeed);
        }

        // Decrease IK weight
        if (reduceIK)
        {
            HeadIk.weight = Mathf.MoveTowards(HeadIk.weight, 0f, Time.deltaTime * weightChangeSpeed);

            if (HeadIk.weight == 0f)
            {
                isAnimating = false;
                reduceIK = false;
                Debug.Log("🔁 Head IK reset to 0");
            }
        }
    }

    IEnumerator AttachFruitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        fruit.transform.SetParent(handIKTarget);
        fruit.transform.localPosition = Vector3.zero;
        fruit.transform.localRotation = Quaternion.identity;
        Debug.Log("🍎 Fruit attached to hand after delay");
    }

    IEnumerator DetachFruitBeforeEnd(float delay)
    {
        yield return new WaitForSeconds(delay);

        fruit.transform.SetParent(trollyContainer);
        fruit.transform.localPosition = Vector3.zero;
        fruit.transform.localRotation = Quaternion.identity;
        reduceIK = true;
        Debug.Log("🛒 Fruit moved to trolly before animation end");
    }
}
