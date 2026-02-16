using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Postprocessing : MonoBehaviour
{
    public Volume volume;
    Vignette vignette;
    //public Animator animation;
    public Transform car;

    void Start()
    {
        volume.profile.TryGet(out vignette);
        vignette.intensity.value = 0.42f;
        //animation.SetTrigger("ChangeCam");
    }

    void LateUpdate()
    {
        // Follow only Y rotation
        Vector3 camRot = transform.eulerAngles;
        camRot.y = car.eulerAngles.y;
        transform.eulerAngles = camRot;

        // Always look at car
        transform.LookAt(car);
    }

    public void PlayerStartedEffect()
    {
               vignette.intensity.value = 0.3f;
    }
}
