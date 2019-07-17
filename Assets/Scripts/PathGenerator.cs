using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private GameObject groundUnit;
    public Material mat_cubeBoarder , mat_cubeBoarderBlueEntry, mat_cubeBoarderRedEntry, mat_cubeBoarderBlueExit, mat_cubeBoarderRedExit;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGridOfSize(8);
        GenerateShortcut();
        GeneratePitFalls();
    }


    public List<List<GameObject>> lst2D_GroundUnits = new List<List<GameObject>>();
    private void GenerateGridOfSize(int n)
    {
        groundUnit = GameObject.CreatePrimitive(PrimitiveType.Cube);
        groundUnit.name = "GroundUnit";
        bool isMovingRight = true;
        for (int i = 0; i < n; i++)
        {
            List<GameObject> lst_GroundUnits = new List<GameObject>();
            for (int j = 0; j < n; j++)
            {
                lst_GroundUnits.Add(Instantiate(groundUnit,transform));
                lst_GroundUnits[j].GetComponent<MeshRenderer>().material = mat_cubeBoarder;
                lst_GroundUnits[j].tag = "NonWalkable";
                cubeItemProperties cip = lst_GroundUnits[j].AddComponent<cubeItemProperties>();

                if(i==0 && j == 0)
                {
                    cip.isInitial = true;
                }

                else if(i==7 && j == 0)
                {
                    cip.isFinal = true;
                }
                
                if (isMovingRight)
                {
                    cip.cubeCurrentMotion = MotionType.right;
                    if (j == n - 1)
                    {
                        cip.cubeCurrentMotion = MotionType.fwd;
                    }
                }
                else
                {
                    cip.cubeCurrentMotion = MotionType.left;
                    if (j == 0)
                    {
                        cip.cubeCurrentMotion = MotionType.fwd;
                    }
                }


                // special abilityies cubes.  
                // two shortcuts and two pitfalls

               
                lst_GroundUnits[j].transform.position = new Vector3(j, 0, i);
            }
            lst2D_GroundUnits.Add(lst_GroundUnits);
            isMovingRight = !isMovingRight;
        }
        DestroyImmediate(groundUnit);
    }


    public void GenerateShortcut()
    {
        int randomRow = Random.Range(0, 3);
        int randomColumn = Random.Range(1, 7);
        GameObject entryCube = lst2D_GroundUnits[randomRow][randomColumn];
        entryCube.GetComponent<MeshRenderer>().material = mat_cubeBoarderBlueEntry;

        //exitPoint should be greated then previous row or colomn in samae row

        int exitRandomRow = Random.Range(randomRow, 5);
        if (randomRow == exitRandomRow) randomColumn += 1;
        int exitRandomColumn = Random.Range(randomColumn, 8);
        lst2D_GroundUnits[exitRandomRow][exitRandomColumn].GetComponent<MeshRenderer>().material = mat_cubeBoarderBlueExit;
        entryCube.GetComponent<cubeItemProperties>().exitPoint = new Vector2Int(exitRandomRow, exitRandomColumn);
        entryCube.GetComponent<cubeItemProperties>().isShortcut = true;

        //second shortcut.

        randomRow = Random.Range(randomRow, 7);
        randomColumn = Random.Range(0, 7);
        entryCube = lst2D_GroundUnits[randomRow][randomColumn];
        entryCube.GetComponent<MeshRenderer>().material = mat_cubeBoarderBlueEntry;

        ////exitPoint should be greated then previous row or colomn in samae row

        exitRandomRow = Random.Range(randomRow, 7);
        if (randomRow == exitRandomRow) randomColumn += 1;
        exitRandomColumn = Random.Range(randomColumn, 8);
        lst2D_GroundUnits[exitRandomRow][exitRandomColumn].GetComponent<MeshRenderer>().material = mat_cubeBoarderBlueExit;
        entryCube.GetComponent<cubeItemProperties>().exitPoint = new Vector2Int(exitRandomRow, exitRandomColumn);
    }

    public void GeneratePitFalls()
    {

        //second shortcut.

        int randomRow = Random.Range(7, 8);
        int randomColumn = Random.Range(1, 8);
        GameObject entryCube = lst2D_GroundUnits[randomRow][randomColumn];
        entryCube.GetComponent<MeshRenderer>().material = mat_cubeBoarderRedEntry;

        ////exitPoint should be greated then previous row or colomn in samae row

        int exitRandomRow = Random.Range(2, randomRow-1);
        int exitRandomColumn = Random.Range(0, 7);
        if (lst2D_GroundUnits[exitRandomRow][exitRandomColumn].GetComponent<cubeItemProperties>().isShortcut)
        {
            exitRandomColumn += 1;
        }
        lst2D_GroundUnits[exitRandomRow][exitRandomColumn].GetComponent<MeshRenderer>().material = mat_cubeBoarderRedExit;
        entryCube.GetComponent<cubeItemProperties>().exitPoint = new Vector2Int(exitRandomRow, exitRandomColumn);
        entryCube.GetComponent<cubeItemProperties>().isPitfall = true;



         randomRow = Random.Range(1, 7);
         randomColumn = Random.Range(1, 7);
        if (lst2D_GroundUnits[randomRow][randomColumn].GetComponent<cubeItemProperties>().isShortcut)
        {
            randomColumn++;
        }
         entryCube = lst2D_GroundUnits[randomRow][randomColumn];
        entryCube.GetComponent<MeshRenderer>().material = mat_cubeBoarderRedEntry;

        //exitPoint should be greated then previous row or colomn in samae row

         exitRandomRow = Random.Range(randomRow, 5);
        if (randomRow == 1) exitRandomRow = 0;
         exitRandomColumn = Random.Range(0, 7);
        if (lst2D_GroundUnits[randomRow][randomColumn].GetComponent<cubeItemProperties>().isShortcut)
        {
            exitRandomColumn++;
        }
        lst2D_GroundUnits[exitRandomRow][exitRandomColumn].GetComponent<MeshRenderer>().material = mat_cubeBoarderRedExit;
        entryCube.GetComponent<cubeItemProperties>().exitPoint = new Vector2Int(exitRandomRow, exitRandomColumn);
        entryCube.GetComponent<cubeItemProperties>().isPitfall = true;
    }

    public void ChangeMaterialOfCube(int x)
    {
        
        Debug.Log(x / 10 +"   "+ x % 10 + "  "+ lst2D_GroundUnits[0].Count);
      //  lst2D_GroundUnits[x/10][x%10].GetComponent<MeshRenderer>().material = mat_cubeBoarderNew;
    }
}
