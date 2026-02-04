using UnityEngine;
using TMPro;
using System.Collections;
using StarterAssets;

public class FrndConvo : MonoBehaviour
{
    [Header("Camera")]
    public GameObject PlayerCam;
    public GameObject FrndCam;

    [Header("Dialogue Text")]
    public TextMeshProUGUI FrndFirstDia;
    public TextMeshProUGUI frndSecondDia;
    public TextMeshProUGUI PlayerFirstDia;
    public GameObject UI;

    [Header("Multiple Dialogues")]
    [TextArea] public string[] playerLines;
    [TextArea] public string[] friendLines;

    [Header("Player and Friend models")]
    public Transform PlayerModel;
    public Transform FrndModel;

    [Header("Player Name Input")]
    public Conversation PlayerName;

    [Header("Controller")]
    public ThirdPersonController playerController;

    [Header("Typing Animation")]
    public float typingSpeed = 0.02f;
    public float floatHeight = -5f;
    public float floatSpeed = 18f;

    bool isNearFrnd = false;

    [Header("Animation")]
    public Animator PlayerIdl;

    [Header("Press E to open UI")]
    public GameObject PressE;
    public GameObject MainInstruction;
    public GameObject FrndToTroley;

    void Start()
    {
        FrndCam.SetActive(false);
        UI.SetActive(false);

        FrndFirstDia.text = "";
        frndSecondDia.text = "";
        PlayerFirstDia.text = "";

        FrndFirstDia.alpha = 1;
        PressE.SetActive(false);
        FrndToTroley.SetActive(false);

    }

    void Update()
    {
        
        float distance = Vector3.Distance(PlayerModel.position, FrndModel.position);

        if (distance <= 2f && !isNearFrnd)
        {
            PressE.SetActive(true);
            PressE.transform.LookAt(PlayerCam.transform);
            PressE.transform.Rotate(0, 180f, 0);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerIdl.SetTrigger("stopwalk");
                isNearFrnd = true;

                UI.SetActive(true);
                FrndCam.SetActive(true);
                PlayerCam.SetActive(false);

                playerController.enabled = false;
                StartCoroutine(StartConversation());
                MainInstruction.SetActive(false);
            }
        }
        else
        {
            PressE.SetActive(false);
        }
    }

    IEnumerator StartConversation()
    {
        string newName = PlayerName.userInput;

        yield return StartCoroutine(TypeFloatingText(
            FrndFirstDia, "Hey " + newName + ", how are you?"
        ));

        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOutText(FrndFirstDia, 0.5f));

        int max = Mathf.Max(playerLines.Length, friendLines.Length);

        for (int i = 0; i < max; i++)
        {
            if (i < playerLines.Length)
            {
                yield return StartCoroutine(TypeFloatingText(PlayerFirstDia, playerLines[i]));
                yield return new WaitForSeconds(0.25f);
            }

            if (i < friendLines.Length)
            {
                yield return StartCoroutine(TypeFloatingText(frndSecondDia, friendLines[i]));
                yield return new WaitForSeconds(0.25f);
            }
        }

        // ⭐ AFTER CONVERSATION END
        yield return new WaitForSeconds(2f);

        FrndCam.SetActive(false);
        PlayerCam.SetActive(true);
        UI.SetActive(false);

        playerController.enabled = true;
        FrndToTroley.SetActive(true);
    }

    IEnumerator TypeFloatingText(TextMeshProUGUI target, string line)
    {
        target.text = "";

        for (int i = 0; i < line.Length; i++)
        {
            target.text += line[i];
            target.ForceMeshUpdate();

            TMP_TextInfo textInfo = target.textInfo;
            int charIndex = textInfo.characterCount - 1;
            if (charIndex < 0) continue;

            TMP_CharacterInfo charInfo = textInfo.characterInfo[charIndex];
            if (!charInfo.isVisible) continue;

            int meshIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;
            Vector3[] vertices = textInfo.meshInfo[meshIndex].vertices;

            Vector3 offset = new Vector3(0, floatHeight, 0);
            for (int j = 0; j < 4; j++)
                vertices[vertexIndex + j] += offset;

            target.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);

            float t = 0;
            while (t < 1)
            {
                float move = Mathf.Lerp(floatHeight, 0, t);
                Vector3 animOffset = new Vector3(0, move, 0);

                for (int j = 0; j < 4; j++)
                    vertices[vertexIndex + j] =
                        textInfo.meshInfo[meshIndex].vertices[vertexIndex + j] - offset + animOffset;

                target.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                t += Time.deltaTime * floatSpeed;
                yield return null;
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        target.ForceMeshUpdate();
        target.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    IEnumerator FadeOutText(TextMeshProUGUI text, float duration)
    {
        float time = 0;
        text.alpha = 1;

        while (time < duration)
        {
            text.alpha = Mathf.Lerp(1, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        text.gameObject.SetActive(false);
    }
}
