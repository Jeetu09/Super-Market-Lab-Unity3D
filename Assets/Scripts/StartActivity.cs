using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartActivity : MonoBehaviour
{

    public GameObject Homepane;
    public GameObject selectscene;

    void Start()
    {
        selectscene.SetActive(false);
    }

    public void StartActivityBtn()
    {
        // SceneManager.LoadScene("selectscenario");

        Homepane.SetActive(false);
        selectscene.SetActive(true);
    }
}
