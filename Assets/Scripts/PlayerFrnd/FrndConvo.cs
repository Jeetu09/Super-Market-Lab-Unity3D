using UnityEngine;
using TMPro;
using System.Collections;
using StarterAssets;

public class FrndConvo : MonoBehaviour
{
    public GameObject Camera;
    public GameObject MainCOnvoImage;
    public float AnimationSpeed = 0.05f;

    public TextMeshProUGUI FriendTextUI;
    public TextMeshProUGUI PlayerTextUI;

    [TextArea] public string[] FriendDialogues;
    [TextArea] public string[] PlayerDialogues;

    public ThirdPersonController PlayerArm;
    public Transform player;
    public Transform frnd;

    bool convoStarted = false;

    void Start()
    {
        MainCOnvoImage.SetActive(false);
        FriendTextUI.text = "";
        PlayerTextUI.text = "";
        Camera.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, frnd.position);

        if (distance < 3f && !convoStarted)
        {
            convoStarted = true;
            PlayerArm.enabled = false;
            MainCOnvoImage.SetActive(true);

            StartCoroutine(PlayConversation());
            Camera.SetActive(true);
        }
    }

    IEnumerator PlayConversation()
    {
        int max = Mathf.Max(FriendDialogues.Length, PlayerDialogues.Length);

        for (int i = 0; i < max; i++)
        {
            if (i < FriendDialogues.Length)
            {
                PlayerTextUI.text = ""; // hide player
                yield return StartCoroutine(TypeText(FriendTextUI, FriendDialogues[i]));
                yield return new WaitForSeconds(0.5f);
            }

            if (i < PlayerDialogues.Length)
            {
                FriendTextUI.text = ""; // hide friend
                yield return StartCoroutine(TypeText(PlayerTextUI, PlayerDialogues[i]));
                yield return new WaitForSeconds(0.5f);
            }
        }

        PlayerArm.enabled = true;
        MainCOnvoImage.SetActive(false);
    }

    IEnumerator TypeText(TextMeshProUGUI ui, string message)
    {
        ui.text = "";

        foreach (char c in message)
        {
            ui.text += c;
            yield return new WaitForSeconds(AnimationSpeed);
        }
    }
}
