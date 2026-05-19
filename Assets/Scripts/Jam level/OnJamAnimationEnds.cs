
using UnityEngine;

public class OnJamAnimationEnds : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject JamFunFact;
    SectionCount sectionCount;

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
        FindObjectOfType<SectionCount>().IncreaseJamSectionCount();
    }


}