using System.Collections;
using UnityEngine;
using TMPro;   // ✅ Correct namespace for TextMeshPro

public class RunningText : MonoBehaviour
{
    [Header("Assign TMP Text here")]
    public TMP_Text InstructionOne;

    [Header("Typing Settings")]
    [TextArea] public string fullText;   // The full message to display
    public float typingSpeed = 0.05f;    // Delay between letters

    private void Start()
    {
        InstructionOne.text = "";  // start empty
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            InstructionOne.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
