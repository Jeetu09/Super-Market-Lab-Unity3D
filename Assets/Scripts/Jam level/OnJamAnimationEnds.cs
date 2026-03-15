
using UnityEngine;

public class OnJamAnimationEnds : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject JamFunFact;

    void Start()
    {
        JamFunFact.SetActive(false);
        objectToEnable.SetActive(false);
    }
    public void OnAnimationEnd()
    {
        objectToEnable.SetActive(true);

    }

    public void OnAninmationTotalEnds()
    {
        JamFunFact.SetActive(false);
    }


}