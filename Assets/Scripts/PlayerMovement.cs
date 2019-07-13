using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement s_Instance;
    public GameObject finalPanel;
    public MotionType collisionMotionType = MotionType.right;
    public PathGenerator pathGeneratorScript = null;

    private void Awake()
    {
        if(s_Instance == null)
        {
            s_Instance = this;
        }
    }

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

        //move player position as dice count and keep track of the board.
        for (int i = 0; i < diceValue; i++)
        {
            Vector3 playerPosition = transform.position;
            RaycastHit hit;
            if(Physics.Raycast(playerPosition,Vector3.down, out hit))
            {
                cubeItemProperties cip = hit.transform.GetComponent<cubeItemProperties>();
                if (!cip.isFinal)
                {
                    yield return StartCoroutine(DoMotionOfPlayer(cip.cubeCurrentMotion));
                }
            }
        }
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, Vector3.down, out hit2))
        {
            cubeItemProperties cip = hit2.transform.GetComponent<cubeItemProperties>();
            if (cip.isAssending)
            {
                for (int i = 0; i < cip.factor; i++)
                {
                    yield return StartCoroutine(DoMotionOfPlayer(MotionType.fwd));
                }
            }
            else if (cip.isDesending)
            {
                for (int i = 0; i < cip.factor; i++)
                {
                    yield return StartCoroutine(DoMotionOfPlayer(MotionType.back));
                }
            }
            else if (cip.isFinal)
            {
                Debug.Log("You fucking won");
            }
        }

        Dice.isDiceRolling = false;
    }


    IEnumerator DoMotionOfPlayer(MotionType motionType)
    {
        yield return new WaitForSeconds(1.25f);
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
