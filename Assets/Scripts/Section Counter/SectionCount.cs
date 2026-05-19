
using UnityEngine;

public class SectionCount : MonoBehaviour
{
    [SerializeField] bool FruitSectionCount = false;
    [SerializeField] bool JamSectionCount = false;
    [SerializeField] bool BekarySectionCount = false;
    public void IncreaseFruitSectionCount()
    {
        FruitSectionCount = true;
        Debug.Log("Section Completed " + FruitSectionCount);
    }

    public void IncreaseJamSectionCount()
    {
        JamSectionCount = true;
        Debug.Log("Section Completed " + JamSectionCount);
    }

    public void IncreaseBekarySectionCount()
    {
        BekarySectionCount = true;
        Debug.Log("Section Completed " + BekarySectionCount);
    }
}
