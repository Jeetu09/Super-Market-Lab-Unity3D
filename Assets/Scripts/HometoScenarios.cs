
using UnityEngine;
using UnityEngine.SceneManagement;

public class HometoScenarios : MonoBehaviour
{
    public void changeScene()
    {
        SceneManager.LoadScene("Scenarios");
    }

    public void MainGame()
    {
        SceneManager.LoadScene("Super Market");
    }


}
