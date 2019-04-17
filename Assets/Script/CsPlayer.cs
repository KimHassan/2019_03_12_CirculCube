using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsPlayer : MonoBehaviour
{
    public CsTileManager tileManager;

    public int tileX;
    public int tileY;

    public int going = 0;

    int total = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    public void setGone()
    {
        tileManager.tiles[tileX, tileY].GetComponent<CsTile>().goingNum = going; // 밟은 타일에게 현재 몇번 밟았는지 전달해줌
        tileManager.tiles[tileX, tileY].GetComponent<CsTile>().setTile(2);



    }
    public void setUp(int _tileX,int _tileY)
    {
        tileX = _tileX;
        tileY = _tileY;

        transform.position = tileManager.tiles[_tileX, _tileY].transform.position;

    }

}
