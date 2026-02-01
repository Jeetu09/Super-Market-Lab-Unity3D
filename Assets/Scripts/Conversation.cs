using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour
{
    public TextMeshProUGUI SecurityGuardText;
    public TextMeshProUGUI PlayerText;
    public TextMeshProUGUI CarText;

    public string securityGuardLine1;
    public string securityGuardLine2;
    public string playerLine;
    public string carLine;

    public TMP_InputField inputField;
    public Button submitButton;
    public float typingSpeed = 0.02f;

    public GameObject PlayeBox;

    private bool carPlayed = false;

    [Header("New Cam")]
    public GameObject SecondCam;
    public GameObject ThirdCam;
    public Animator MainGate;
    public Animator CarAnimator;

    // ✅ Reference to CarAnimEnd
    public CarAnimEnd carAnimEnd;

    void Start()
    {
        ThirdCam.SetActive(false);
        PlayeBox.SetActive(false);

        SecurityGuardText.text = "";
        PlayerText.text = "";
        CarText.text = "";

        inputField.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);

        StartCoroutine(DialogueSequence());
    }

    IEnumerator DialogueSequence()
    {
        yield return StartCoroutine(TypeLine(SecurityGuardText, securityGuardLine1));

        PlayeBox.SetActive(true);

        yield return StartCoroutine(TypeLine(PlayerText, playerLine));

        inputField.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(true);
    }

    public void OnButtonClick()
    {
        if (carPlayed) return;

        if (inputField.text.Length > 2)
        {
            carPlayed = true;
            StartCoroutine(CarAndFinalGuard());
        }
    }

    IEnumerator CarAndFinalGuard()
    {
        yield return StartCoroutine(TypeLine(CarText, carLine));
        yield return StartCoroutine(TypeLine(SecurityGuardText, securityGuardLine2));

        yield return new WaitForSeconds(2f);

        ThirdCam.SetActive(true);
        SecondCam.SetActive(false);

        MainGate.SetTrigger("MainGateOpen");

        // ✅ Start wheel rotation
        carAnimEnd.rotateWheels = true;

        CarAnimator.SetTrigger("GateToPark");
    }

    IEnumerator TypeLine(TextMeshProUGUI target, string text)
    {
        target.text = "";

        foreach (char c in text)
        {
            target.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
