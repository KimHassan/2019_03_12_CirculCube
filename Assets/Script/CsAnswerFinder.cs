using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsAnswerFinder : MonoBehaviour
{
    public CsTileManager tileManager;
    // Start is called before the first frame update

    int goingMax = 0;

    int going = 0;

    public int tileX;
    public int tileY;

    int[] moveStep = new int[4];

    public int stepNum = 0;

    public bool isFindAnswer = false;

    int[] total;

    int max;

    private void Awake()
    {
        max = tileManager.GetComponent<CsTileManager>().width * tileManager.GetComponent<CsTileManager>().height; // 최대 이용횟수

        goingMax = Random.Range(max - 2, max); // 최대로 이동할 횟수

        moveStep[0] = 0; // 상하좌우로 갈 순서
        moveStep[1] = 1;
        moveStep[2] = 2;
        moveStep[3] = 3;

        total = new int[10];

    }
    void Start()
    {
        setInit();
    }
    

    // Update is called once per frame
    void Update()
    {


    }

    public void FindAnswer()
    {


        int c = moveStep[stepNum]; // 움직일 방향 0~3

        int nextTileX = tileX; //현재타일
        int nextTileY = tileY;



        switch (c)
        {
            case 0:
                nextTileX = tileX + 1;
                break;

            case 1:
                nextTileX = tileX - 1;
                break;

            case 2:
                nextTileY = tileY + 1;
                break;

            case 3:
                nextTileY = tileY - 1;
                break;
        }

        if (nextTileX < 0 ||
            nextTileX > 4 ||
            nextTileY < 0 ||
            nextTileY > 4) // 넥스트 타일이 끝에 있으면
        {
            stepNum++; // 다음 넥스트 타일로 넘어감 상하좌우
        }
        else
        {

            if (tileManager.tiles[nextTileX, nextTileY].GetComponent<CsTile>().state == 1) // 이동할 수 있는 타일이면
            {

                Move(nextTileX, nextTileY);
            }
            else
            {
                stepNum++;
            }
        }

        if (stepNum == 4) // 4방향 전부 이동하지 못할 때
        {
            if (CsGameManager.instance.level < 15)
            {
                if (going <= 6)
                {
                    setReverse(); // 못간 타일을 답으로 지정
                    return;
                }
            }
            else
            {
                setCorrectAnswer(); // 8일때 막다는 길로 갈수도있으므로
                return;
            }
        }

        if (going >= goingMax) // 정답 지정
        {
            setCorrectAnswer();

        } 
    }


    void setReverse()
    {

        if (going == 6 && (tileX == 2 || tileY == 2))
        {
            CsGameManager.instance.ChangeGoal(45 - total[going - 1] - total[going - 2]); // 가기 전 2개의 타일을 뺸 뒤 간 곳을 최대값에서 뺌
            
        }
        else
        {
            CsGameManager.instance.ChangeGoal(45 - total[going - 1]);
            
        }

        tileManager.GetComponent<CsTileManager>().ResetTiles();

        isFindAnswer = true;
    }

    void setCorrectAnswer()
    {
        int goal = 0;

        for(int i=0;i<going;i++)
        {
            goal += total[i]; // 지나온 모든 답을 넣어둠
        }

        CsGameManager.instance.ChangeGoal(goal); // 목적 설정

        tileManager.GetComponent<CsTileManager>().ResetTiles();

        isFindAnswer = true;
    }

    void ResetMoveStep() // 넥스트 타일을  갈 순서 다시 정함
    {
        for(int i=0;i<3;i++)
        {
            int rand = Random.Range(0, 4);

            int temp = moveStep[rand];

            moveStep[rand] = moveStep[i];

            moveStep[i] = temp;
        }
    }



    void Move(int _tileX, int _tileY)
    {

        ResetMoveStep();

        stepNum = 0;

        tileManager.tiles[tileX, tileY].GetComponent<CsTile>().setTile(2);

        tileManager.tiles[tileX, tileY].GetComponent<CsTile>().goingNum = going;


        tileX = _tileX;
        tileY = _tileY;

        transform.position = tileManager.tiles[_tileX, _tileY].transform.position;

        total[going] = tileManager.tiles[tileX, tileY].GetComponent<CsTile>().num;

        going++;


        if (going >= goingMax)
        {

            setCorrectAnswer();
        }

    }

    public void setInit() // 초기화
    {

        tileX = tileManager.startX;
        tileY = tileManager.startY;

        transform.position = tileManager.tiles[tileX, tileY].transform.position;

        goingMax = Random.Range(max - 2, max);

        going = 0;

        stepNum = 0;

        ResetMoveStep();

        isFindAnswer = false;
    }
}
