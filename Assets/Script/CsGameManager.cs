using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsGameManager : MonoBehaviour
{
    static public CsGameManager instance;

    public CsTileManager tileManager;

    public CsPlayer player;

    public CsUIControll uiControll;

    public CsAnswerFinder answerFinder;


    public GameObject barrier;

    bool isClear = false;

    public int score = 0;

    public int goal = 0;

    int loadingStep = 1;


    // time

    float time;

    float maxTime = 10f;

    //

   public bool isTimeFinished = false;


    int money = 0;

    public int level = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        Screen.SetResolution(Screen.width, Screen.width / 9 * 16, true);
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadingStart();

        LevelUp();

        uiControll.setMoney(money);

        isTimeFinished = true;



    }

    // Update is called once per frame
    void Update()
    {
        TimeUpdate();
        LoadingUpdate();
    }

    void TimeUpdate()
    {
        if (isTimeFinished == false)
        {
            time += Time.deltaTime;

            uiControll.setTimeBar(maxTime, time);

            uiControll.setTimer(maxTime, time);

            if (time >= maxTime)
            {
                isTimeFinished = true;
                GameFail();
            }

        }
        
    }

    public void LoadingStart()
    {
        loadingStep = 1;
    }

    public void GameClear()
    {
        isClear = true;
        //tileManager.GameFail();

        uiControll.GameClear();

        takeMoney();

        uiControll.setMoney(money);

        isTimeFinished = true; 


    }

    public void GameFail()
    {
        isClear = false;   

        //tileManager.GameFail();

        uiControll.GameFail();

        CompareMoney();

        uiControll.setLastMoney(money);
    }

    void CompareMoney()
    {
        if(money > uiControll.bestMoney)
        {
            uiControll.SaveBestMoney(money);
        }
    }
    void CompareScore()
    {
        if (goal == 0)
            return;

        if (goal == score)
        {
            GameClear();
        }
    }


    public void ChangeScore(int _score)
    {
        score += _score;

        uiControll.setScore(score);

        CompareScore();
    }

    public void ChangeGoal(int _goal)
    {
        goal = _goal;

        uiControll.setGoal(goal);
    }

    public void InitScores()
    {
        goal = 0;

        score = 0;

        uiControll.setScore(score);

        uiControll.setGoal(goal);

    }
    public void LoadingUpdate()
    {
        
        switch (loadingStep)
        {
            case 0:
                break;
            case 1: // 화면이 커지는 부분
                {
                    
                    barrier.transform.localScale += new Vector3(0.2f, 0.2f);

                    if (barrier.transform.localScale.x > 5) // 다커졌을 때
                    {
                        barrier.transform.localScale = new Vector3(5f, 5f);

                        loadingStep = 2;
                    }
                    break;
                }
                
            case 2: // 초기화 되는 부분
                {
                    
                        tileManager.SetInit();

                        InitScores();

                    answerFinder.setInit();


                    loadingStep = 3;

                    //초기화가 완료되면 3
                    break;
                }
            case 3:
                {
                    if(answerFinder.isFindAnswer == false)
                     answerFinder.FindAnswer();
                    if (answerFinder.isFindAnswer == true)
                    {

                        loadingStep = 4;
                    }
                    
                    break;
                }
            case 4: // 화면이 작아지는 부분
                {
                    barrier.transform.localScale -= new Vector3(0.2f, 0.2f);
                    if (barrier.transform.localScale.x < 0) // 다커졌을 때
                    {
                        barrier.transform.localScale = new Vector3(0f, 0f);
                        loadingStep = 5;
                    }


                    break;
                }
                
            case 5: // 다시 시작하는 부분
                {

                    isTimeFinished = false;

                    if (isClear == false)
                    {
                        ResetMoneyGoal();
                    }
                    LevelUp();

                    setLevel();

                    time = 0;

                    loadingStep = 0;

                    isClear = false;

                    player.going = 0;
                }
                break;
        }
    }

    public void setLevel()
    {
        if(goal > 40)
        {
            maxTime = 10f;
        }
        else if(goal > 35)
        {
            maxTime = 20f;
        }
        else if(goal > 30)
        {
            maxTime = 30f;
        }
        else
        {
            maxTime = 40f;
        }
    }

    void takeMoney()
    {
        float num = (maxTime * 2) * (int)((maxTime - time) * 10);

        money += (int)num;

        uiControll.setMoney(money);

    }

    public int getLoadingStep()
    {
        return loadingStep;
    }

    void LevelUp()
    {
        level++;
        uiControll.setLevel(level);
    
    }

    void ResetMoneyGoal()
    {
        level = 0;

        money = 0;

        uiControll.setMoney(money);

        uiControll.setLevel(level);

    }
}
