using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml;

public class CsUIControll : MonoBehaviour
{

    public Text TextScore;

    public Text TextGoal;

    public Text level;

    public Text money;

    public Text timer;

    public Text lastMoney;

    //시간 관련



    public Slider timeBar;

    public GameObject Go_Fail;

    public GameObject Go_Clear;

    public GameObject pauseButton;

    public GameObject pausePannel;

    public int bestMoney = -10;
    

    bool isPause = false;
    void Start()
    {
        LoadBestMoney();
       
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void setTimeBar(float max, float current)
    {
        timeBar.value = current / max;
    }
    
    public void GameFail()
    {
        Go_Fail.SetActive(true);

    }

    public void GameClear()
    { 
        Go_Clear.SetActive(true);
    }

    public void setScore(int score)
    {
        TextScore.text = score.ToString();
    }

    public void setGoal(int goal)
    {
        TextGoal.text = goal.ToString();
    }

    public void setMoney(int _money)
    {
        money.text = _money.ToString();
    }

    public void setLevel(int _level)
    {
        level.text = "Lv." + _level.ToString();
    }
    public void PressAgainButton()
    {
        Time.timeScale = 1;

        CsGameManager.instance.LoadingStart();

        Go_Fail.SetActive(false);

        Go_Clear.SetActive(false);

        pausePannel.SetActive(false);
    }

    public void PressTitleButton()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("TitleScene");
    }

    public void PressPuaseButton()
    {
        if(isPause == false)
        {
            isPause = true;

            pausePannel.SetActive(true);

            Time.timeScale = 0;
        }
        else
        {

            isPause = false;

            pausePannel.SetActive(false);

            Time.timeScale = 1;

        }
    }

    public void setLastMoney(int _money)
    {
        LoadBestMoney();
        lastMoney.text = "Best\n" + bestMoney + "\nCurrent\n" + _money;
    }
    public void setTimer(float _maxTime,float _time)
    {
       
        timer.text = (int)_time + "/" + (int)_maxTime;
    }

    void LoadBestMoney()
    {
       
        
        bestMoney = PlayerPrefs.GetInt("bestMoney");
       
    }
    public void SaveBestMoney(int _bestMoney)
    {
        
        PlayerPrefs.SetInt("bestMoney", _bestMoney);
    }
   


}
