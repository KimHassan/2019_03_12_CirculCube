using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsTouch : MonoBehaviour
{
    GameObject target;

    public GameObject player;

    public CsTileManager tileManager;

    bool isClick = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CsGameManager.instance.isTimeFinished == true)
            return;

       

#if (UNITY_EDITOR || UNITY_STANDALONE)
        {
            RayCastUpdate();
        }
#elif (UNITY_ANDROID)
        {
        RayCastTouchUpdate();
        }
#endif
    }

    void PlayerMove()
    {
        int tileX = target.GetComponent<CsTile>().tileX;
        int tileY = target.GetComponent<CsTile>().tileY;

        int currentTileX = player.GetComponent<CsPlayer>().tileX;
        int currentTileY = player.GetComponent<CsPlayer>().tileY;

        if ((currentTileX == tileX + 1 && currentTileY == tileY) ||
            (currentTileX == tileX - 1 && currentTileY == tileY) ||
            (currentTileY == tileY + 1 && currentTileX == tileX) ||
            (currentTileY == tileY - 1 && currentTileX == tileX)) // 대각선 방지

        {


            if (tileManager.tiles[tileX, tileY].GetComponent<CsTile>().state == 0) // 막혀있을 때
            {

                return;
            }
            else if (tileManager.tiles[tileX, tileY].GetComponent<CsTile>().state == 1) // 뚫려있을 때
            {
                if (tileManager.tiles[currentTileX, currentTileY].GetComponent<CsTile>().state == 0)
                {
                    player.GetComponent<CsPlayer>().setUp(tileX, tileY);

                    CsGameManager.instance.ChangeScore(tileManager.tiles[tileX, tileY].GetComponent<CsTile>().num);

                }
                else
                {
                    
                    player.GetComponent<CsPlayer>().setGone();

                    player.GetComponent<CsPlayer>().going++;

                    player.GetComponent<CsPlayer>().setUp(tileX, tileY);

                    CsGameManager.instance.ChangeScore(tileManager.tiles[tileX, tileY].GetComponent<CsTile>().num);
                }

            }
            else if (tileManager.tiles[tileX, tileY].GetComponent<CsTile>().state == 2) // 이미 왔을 때
            {
                if (tileManager.tiles[tileX, tileY].GetComponent<CsTile>().goingNum
                    == player.GetComponent<CsPlayer>().going - 1)
                {
                    CsGameManager.instance.ChangeScore(-tileManager.tiles[currentTileX, currentTileY].GetComponent<CsTile>().num);

                    player.GetComponent<CsPlayer>().going--;


                    player.GetComponent<CsPlayer>().setUp(tileX, tileY);

                    tileManager.tiles[tileX, tileY].GetComponent<CsTile>().ResetTile();
                }
                else
                {
                    return;
                }
            }


        }
    }

    void RayCastUpdate()
    {
      
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
        }

        if (Input.GetMouseButton(0))
        {
            if (target == null)
                return;
            if (target.transform.tag == "Player")
            {
                isClick = true;
            }
        }
        else
        {

            isClick = false;
        }

        if (isClick == true)
        {
            if (target.transform.tag == "Tile")
            {

                PlayerMove();

            }
        }

    }

    void RayCastTouchUpdate()
    {
        Touch touch = Input.GetTouch(0);

        Ray ray = Camera.main.ScreenPointToRay(touch.position);

        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
        }

        switch(touch.phase)
        {
            case TouchPhase.Began:
                if (target == null)
                    return;
                if (target.transform.tag == "Player")
                {
                    isClick = true;
                }
                break;
            case TouchPhase.Moved:
                if (isClick == true)
                {
                    if (target.transform.tag == "Tile")
                    {

                        PlayerMove();

                    }
                }
                break;
            case TouchPhase.Ended:
                isClick = false;
                break;
            case TouchPhase.Canceled:
                isClick = false;
                break;
        }
        
    }
}
