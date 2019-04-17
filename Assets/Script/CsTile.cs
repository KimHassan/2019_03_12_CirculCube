using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsTile : MonoBehaviour
{
    public enum STATE
    {
        BLOCK_TILE,
        NORMAL_TILE,
        GONE_TILE,
        END_TILE
    }

    public int num = 0;

    public int tileX;
    public int tileY;

    public SpriteRenderer outSprite;

    public SpriteRenderer inSprite;

    public GameObject text;

    public int state;

    public int goingNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNum(int _num)
    {
        num = _num;
        text.GetComponent<TextMesh>().text = num.ToString();
    }

    public void setTile(int _state)
    {

        state = _state;

        switch((STATE)state)
        {
            case STATE.BLOCK_TILE:
                {
                    outSprite.color = Color.gray;
                    inSprite.color = Color.gray;

                    text.SetActive(false);
                    break;
                }

            case STATE.NORMAL_TILE:
                {
                    outSprite.color = Color.black;
                    inSprite.color = Color.white;

                    text.SetActive(true);
                    break;
                }

            case STATE.GONE_TILE:
                {
                    Color c = new Color(0, goingNum * 0.15f, 1);
                    outSprite.color = Color.black;
                    inSprite.color = c;
                    Debug.Log(inSprite.color.g);
                    Debug.Log(goingNum);
                    break;
                }
            case STATE.END_TILE:
                {
                    outSprite.color = Color.black;
                    inSprite.color = Color.black;
                    break;
                }
        }
    }

    public void ResetTile()
    {
        goingNum = 0;
        setTile(1);
       
    }
}
