using System.Collections;
using TMPro;
using UnityEngine;

public class BillingDialogue : MonoBehaviour
{
    public TMP_Text staffText;
    public TMP_Text playerText;

    public float typingSpeed = 0.04f;

    [Header("Buttons")]
    public GameObject scanButton;
    public GameObject grammarButtons;

    [Header("Trolley Obj")]
    public GameObject AllObj;

    [SerializeField]
    private BillManager billmanage;

    [Header("Trolley Obj")]
    public GameObject trolleyObj;
    public GameObject trolleyObjBag;

    [Header("Animatio")]
    public Animator FFAnimation;
    public Animator RightDoor;
    public Animator LeftDoor;

    string[] staffDialogues =
    {
        "Hello! Did you get all the things.",
        "Ok then, please put all your taken items for scanning.",
        "Before scanning the items, please tell me the size of carry bag you like to take.",
        "Ok.",
        "How would you like to pay the bill, card or cash? Paying by card is more convenient.",
        "Ok.",
        "Here are your purchased items, card and bill.",
        "Do visit again. Have a nice day."
    };

    string[] playerDialogues =
    {
        "Yes.",
        "Sure.",
        "I'll take smaller one.",
        "I ____ prefer card.",
        "Thanks."
    };

    void Start()
    {
        trolleyObjBag.SetActive(false);
        AllObj.SetActive(false);
        // Clear texts
        staffText.text = "";
        playerText.text = "";

        // Disable buttons initially
        scanButton.SetActive(false);
        grammarButtons.SetActive(false);
    }

    // Call this function to start conversation
    public void StartChat()
    {
        StopAllCoroutines();

        staffText.text = "";
        playerText.text = "";

        StartCoroutine(StartChatDelay());
    }

    IEnumerator StartChatDelay()
    {
        yield return null; // wait one frame (fix first letter bug)
        StartCoroutine(StartConversation());
    }

    IEnumerator StartConversation()
    {
        yield return TypeText(staffDialogues[0], staffText);
        yield return TypeText(playerDialogues[0], playerText);

        yield return TypeText(staffDialogues[1], staffText);
        yield return TypeText(playerDialogues[1], playerText);

        // Enable scan button
        scanButton.SetActive(true);
    }

    public void ScanButtonClicked()
    {
        scanButton.SetActive(false);
        StartCoroutine(ScanConversation());
    }

    IEnumerator ScanConversation()
    {
        yield return TypeText(staffDialogues[2], staffText);
        yield return TypeText(playerDialogues[2], playerText);

        yield return TypeText(staffDialogues[3], staffText);

        yield return TypeText(staffDialogues[4], staffText);
        yield return TypeText(playerDialogues[3], playerText);

        // Enable grammar buttons
        grammarButtons.SetActive(true);
    }

    public void WillButtonClicked()
    {
        grammarButtons.SetActive(false);
        StartCoroutine(CardConversation());
    }

    IEnumerator CardConversation()
    {
        FFAnimation.Play("New Animation");
        yield return new WaitForSeconds(11);

        yield return TypeText(staffDialogues[5], staffText);
        yield return TypeText(staffDialogues[6], staffText);

        yield return TypeText(playerDialogues[4], playerText);

        yield return TypeText(staffDialogues[7], staffText);

        ExitState();
    }

    void ExitState()
    {
        Debug.Log("Conversation Ends");
        AllObj.SetActive(false);
        billmanage.EnablePlayer();
        trolleyObj.SetActive(false);
        trolleyObjBag.SetActive(true);
        RightDoor.Play("Right Door");
        LeftDoor.Play("Left Door Animation");
    }

    IEnumerator TypeText(string sentence, TMP_Text textBox)
    {
        textBox.text = "";

        foreach (char letter in sentence)
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(1f);
    }
}