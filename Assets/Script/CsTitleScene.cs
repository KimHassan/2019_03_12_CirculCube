using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class CsTitleScene : MonoBehaviour
{
    public GameObject howToPannel;

    public GameObject backButton;

    private void Awake()
    {
        Screen.SetResolution(Screen.width, Screen.width / 9 * 16, true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressStartButton()
    {
        SceneManager.LoadScene("InGameScene");
    }
    public void PressHowToButton()
    {
        howToPannel.SetActive(true);

    }

    public void PressBackButton()
    {
        howToPannel.SetActive(false);
       
    }
}
