using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private GameObject groundUnit;
    public Material mat_cubeBoarder , mat_cubeBoarderBlue, mat_cubeBoarderRed;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGridOfSize(8);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                if(i== 0 && j == 4)
                {
                    lst_GroundUnits[j].GetComponent<MeshRenderer>().material = mat_cubeBoarderBlue;
                    cip.isAssending = true;
                    cip.factor = 1;
                }

                if (i == 2 && j == 5)
                {
                    lst_GroundUnits[j].GetComponent<MeshRenderer>().material = mat_cubeBoarderBlue;
                    cip.isAssending = true;
                    cip.factor = 2;
                }

                if (i == 3 && j == 3)
                {
                    lst_GroundUnits[j].GetComponent<MeshRenderer>().material = mat_cubeBoarderRed;
                    cip.isDesending = true;
                    cip.factor = 1;
                }
                if (i == 5 && j == 3)
                {
                    lst_GroundUnits[j].GetComponent<MeshRenderer>().material = mat_cubeBoarderBlue;
                    cip.isAssending = true;
                    cip.factor = 1;
                }
                if (i == 5 && j == 4)
                {
                    lst_GroundUnits[j].GetComponent<MeshRenderer>().material = mat_cubeBoarderRed;
                    cip.isDesending = true;
                    cip.factor = 1;
                }
                if (i == 7 && j == 2)
                {
                    lst_GroundUnits[j].GetComponent<MeshRenderer>().material = mat_cubeBoarderRed;
                    cip.isDesending = true;
                    cip.factor = 2;
                }

                lst_GroundUnits[j].transform.position = new Vector3(j, 0, i);
            }
            lst2D_GroundUnits.Add(lst_GroundUnits);
            isMovingRight = !isMovingRight;
        }
        DestroyImmediate(groundUnit);
    }

    public void ChangeMaterialOfCube(int x)
    {
        
        Debug.Log(x / 10 +"   "+ x % 10 + "  "+ lst2D_GroundUnits[0].Count);
      //  lst2D_GroundUnits[x/10][x%10].GetComponent<MeshRenderer>().material = mat_cubeBoarderNew;
    }
}
