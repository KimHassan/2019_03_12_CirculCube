using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsTileManager : MonoBehaviour
{

    public GameObject GO_tile;

    public GameObject player;


    public int width;

    public int height;

    public GameObject[,] tiles;

    int[] rand;

    public int startX;
    public int startY;

    public int levelScale = 1;
    // Start is called before the first frame update
    private void Awake()
    {
       

        tiles = new GameObject[width + 2, height + 2];

        this.transform.position = new Vector3(-2, 2);

        rand = new int[width * height];

        Respawn();

        setRandom();

        InitTiles();

        SetTiles();

    }
    void Start()
    {

        player.GetComponent<CsPlayer>().setUp(startX,startY);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitTiles()
    {
        for (int i = 0; i < height + 2; i++)
        {
            for (int j = 0; j < width + 2; j++)
            {
                //tiles[x,y]
                tiles[j, i] = Instantiate(GO_tile, transform.position + new Vector3(j, -i), Quaternion.identity) as GameObject;
                tiles[j, i].transform.parent = this.transform;
                tiles[j, i].transform.name = "tile[" + j + "," + i + "]";


                tiles[j, i].GetComponent<CsTile>().tileX = j;
                tiles[j, i].GetComponent<CsTile>().tileY = i;
            }
        }

    }

    void SetTiles()
    {
        for (int i = 0; i < height + 2; i++)
        {
            for (int j = 0; j < width + 2; j++)
            {

                if (j == 0 || i == 0 || j == width + 1 || i == height + 1)
                {
                    tiles[j, i].GetComponent<CsTile>().setTile(0);
                }
                else
                {
                    tiles[j, i].GetComponent<CsTile>().setTile(1);
                    tiles[j, i].GetComponent<CsTile>().SetNum(rand[(i - 1) * width + j - 1]);
                }
            }



        }
    }

    void Respawn()
    {
        int a = Random.Range(1, 4);

        int b = Random.Range(0, 4);

        switch (b)
        {
            case 0:
                startX = 0;
                startY = a;
                break;
            case 1:
                startX = 4;
                startY = a;
                break;
            case 2:
                startX = a;
                startY = 0;
                break;
            case 3:
                startX = a;
                startY = 4;
                break;
        }
    }

    void setRandom()
    {
        int max = width * height;
        for (int i=0;i<max;i++)
        {
            rand[i] = i + 1;
        }
        for (int i = 0; i < max/2; i++)
        {
            int rd = Random.Range(0, max);

            int temp = rand[rd];

            rand[rd] = rand[i];

            rand[i] = temp;
        }

    }

    public void ResetTiles()
    {
        tiles[startX, startY].GetComponent<CsTile>().setTile(0);

        for (int i=1;i<4;i++)
        {
            for(int j=1;j<4;j++)
            {
                tiles[i, j].GetComponent<CsTile>().ResetTile();
            }
        }
    }

    public void GameFail()
    {
        
        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                tiles[i, j].GetComponent<CsTile>().setTile(0);
            }
        }
    }

    public void SetInit()
    {
        Respawn();

        setRandom();

        SetTiles();

        player.GetComponent<CsPlayer>().setUp(startX, startY);
    }
}
