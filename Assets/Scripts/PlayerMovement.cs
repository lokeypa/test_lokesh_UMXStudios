using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //public static PlayerMovement s_Instance;
    public GameObject finalPanel;
    public MotionType collisionMotionType = MotionType.right;
    private PathGenerator pathGeneratorScript = null;
    public bool hasSkippedPreviousTurn = false;
    public bool hasAlreadySkipped = false;

    public int previousRecordedValue = 0;

    private void Start()
    {
        pathGeneratorScript = GameObject.FindObjectOfType<PathGenerator>().GetComponent<PathGenerator>();
        
    }
    public IEnumerator StartExecution(float timeDelay)
    {
        for (int i = 0; i < pathGeneratorScript.lst2D_GroundUnits.Count; i++)
        {
            for (int j = 0; j < pathGeneratorScript.lst2D_GroundUnits[i].Count; j++)
            {
                cubeItemProperties cip = pathGeneratorScript.lst2D_GroundUnits[i][j].GetComponent<cubeItemProperties>();
                if (!cip.isFinal)
                {
                    yield return StartCoroutine(DoMotionOfPlayer(cip.cubeCurrentMotion));
                }
            }
        }
    }

    public IEnumerator StartExecution(int diceValue)
    {
        
        List<List<GameObject>> lst2D_GroundUnits = FindObjectOfType<PathGenerator>().GetComponent<PathGenerator>().lst2D_GroundUnits;

        for (int i = 0; i < diceValue; i++)
        {
            // get the player positin find corrosponding cube extract the data and then move according to the data.
            Vector2Int initialPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);
            cubeItemProperties cip = lst2D_GroundUnits[initialPosition.y][initialPosition.x].transform.GetComponent<cubeItemProperties>();
            if (cip.isFinal)
            {
                //this player has won the game
                finalPanel.SetActive(true);
                Text tmptext = finalPanel.transform.GetChild(0).GetComponent<Text>();
                tmptext.text = gameObject.name + " has Won The match!!!";
            }
            else
            {
                yield return StartCoroutine(DoMotionOfPlayer(cip.cubeCurrentMotion));
            }
        }


        Vector2Int initialPosition2 = new Vector2Int((int)transform.position.x, (int)transform.position.z);
        cubeItemProperties cip2 = lst2D_GroundUnits[initialPosition2.y][initialPosition2.x].transform.GetComponent<cubeItemProperties>();
        if(cip2.isPitfall || cip2.isShortcut){
            yield return StartCoroutine(MovePlayerPosition(cip2.exitPoint));
        }
        Dice.isDiceRolling = false;
    }


    public IEnumerator MovePlayerPosition(Vector2Int endPoint)
    {
        Vector2Int initialPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);

        while (endPoint.y != initialPosition.y)
        {
            if(Mathf.Abs(endPoint.y - (initialPosition.y+1) ) < Mathf.Abs( endPoint.y - initialPosition.y))
            {
               yield return StartCoroutine(DoMotionOfPlayer(MotionType.fwd));
                // should move up 
                initialPosition.y++;
            }
            else
            {
                yield return StartCoroutine(DoMotionOfPlayer(MotionType.back));
                // move down.
                initialPosition.y--;
            }
        }

        while (endPoint.x != initialPosition.x)
        {
            if (Mathf.Abs(endPoint.x - (initialPosition.x + 1)) < Mathf.Abs(endPoint.x - initialPosition.x))
            {
                yield return StartCoroutine(DoMotionOfPlayer(MotionType.right));
                // should move up 
                initialPosition.x++;
            }
            else
            {
                yield return StartCoroutine(DoMotionOfPlayer(MotionType.left));
                // move down.
                initialPosition.x--;
            }
        }
    }


    IEnumerator DoMotionOfPlayer(MotionType motionType)
    {
        yield return new WaitForSeconds(0.5f);
        switch (motionType)
        {
            case MotionType.left:
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + (-Vector3.right), 2);
                }
                break;

            case MotionType.right:
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, 2);
                }
                break;

            case MotionType.fwd:
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.forward, 2);
                }
                break;
            case MotionType.back:
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + (-Vector3.forward), 2);
                }
                break;
        }   
    }
    private void OnCollisionEnter(Collision collision)
    {
      //  collisionMotionType = collision.gameObject.GetComponent<cubeItemProperties>().cubeCurrentMotion;
      //  Debug.Log("status changed " + statusCount + collisionMotionType.ToString());
      //  statusCount++;
        //    if (collision.gameObject.CompareTag("NonWalkable"))
        //    {
        //        //los the game.
        //        Debug.Log("you lost!");
        //        finalPanel.SetActive(true);
        //        finalPanel.transform.GetChild(0).GetComponent<Text>().text = "You Lost!!!";

        //    }

        //    else if(collision.gameObject.CompareTag("Goal"))
        //    {
        //        //win the game.
        //        Debug.Log("you won!");
        //        finalPanel.SetActive(true);
        //        finalPanel.transform.GetChild(0).GetComponent<Text>().text = "You Won!!!";
        //    }
    }
}
