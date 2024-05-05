using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] sprites;
    public float[] chance;
    private int xLength = 42;
    private int yLength = 22;
    public float scrollSpeed = 0.1f;
    
    public GameObject[,] grid;

    void Start()
    {
        grid = new GameObject[xLength, yLength];
        Debug.Log(sprites[0]);
        //Instantiate(sprites[0], new Vector3(10.0f, 10.0f, 0.0f), Quaternion.identity, transform);

        float totalRatio = 0.0f;
        for(int i = 0; i < chance.Length; i++){
            totalRatio += chance[i];
        }

        for (int x = 0; x < xLength; x++){
            for (int y = 0; y < yLength; y++){

                var ran = Random.Range(0,totalRatio);

                for (int c = 0; c < chance.Length; c++){
                    if ((ran -= chance[c]) < 0) // Test for A
                    {
                        grid[x,y] = Instantiate(sprites[c], transform, false);
                        grid[x,y].transform.Translate((float)x* 0.5f, (float)y* 0.5f, 0.0f);
                        break;
                    }

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < xLength; x++){
            for (int y = 0; y < yLength; y++){
                grid[x,y].transform.Translate(-1.0f * scrollSpeed*Time.fixedDeltaTime, 0.0f, 0.0f);
            }
        }
    }


    
}
