using UnityEngine;

public class VisibleMouse : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
