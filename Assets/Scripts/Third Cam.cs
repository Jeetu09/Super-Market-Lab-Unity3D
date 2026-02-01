using UnityEngine;
using UnityEngine.UI;

public class ThirdCam : MonoBehaviour
{
    public Image DarkImage;
    public float DarkSpeed = 0.5f;

    void Update()
    {
        if (DarkImage.color.a > 0)
        {
            Color c = DarkImage.color;
            c.a -= DarkSpeed * Time.deltaTime;
            c.a = Mathf.Clamp01(c.a);
            DarkImage.color = c;
        }
    }
}
