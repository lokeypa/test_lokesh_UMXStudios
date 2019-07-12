using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private GameObject groundUnit;
    public Material mat_cubeBoarder , mat_cubeBoarderNew;

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
        for (int i = 0; i < n; i++)
        {
            List<GameObject> lst_GroundUnits = new List<GameObject>();
            for (int j = 0; j < n; j++)
            {
                lst_GroundUnits.Add(Instantiate(groundUnit,transform));
                lst_GroundUnits[j].GetComponent<MeshRenderer>().material = mat_cubeBoarder;
                lst_GroundUnits[j].tag = "NonWalkable";

                //if (i < 2)
                //{
                //    lst_GroundUnits[j].GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
                //}

                //if(j == n/2 || j == n / 2 - 1)
                //{
                //    lst_GroundUnits[j].GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
                //}
                //if(i == 3)
                //{
                //    lst_GroundUnits[j].GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
                //    //HSVToRGB(Random.Range(0, 361f), Random.Range(0, 1f), Random.Range(0, 1f));
                //}
                lst_GroundUnits[j].transform.position = new Vector3(j, 0, i);
            }
            lst2D_GroundUnits.Add(lst_GroundUnits);
        }
        DestroyImmediate(groundUnit);
    }

    public void ChangeMaterialOfCube(int x)
    {
        
        Debug.Log(x / 10 +"   "+ x % 10 + "  "+ lst2D_GroundUnits[0].Count);
        lst2D_GroundUnits[x/10][x%10].GetComponent<MeshRenderer>().material = mat_cubeBoarderNew;
    }
}
