using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeItemProperties : MonoBehaviour
{
    public MotionType cubeCurrentMotion = MotionType.right;
    public bool isShortcut = false;
    public Vector2Int exitPoint = Vector2Int.zero; 
    public bool isPitfall = false;
    public bool isInitial = false;
    public bool isFinal = false;

}


//            for (int i = 0; i<diceValue; i++)
//            {
//                Vector3 playerPosition = transform.position;
//RaycastHit hit;
//        {
//            //move player position as dice count and keep track of the board.
//                if (Physics.Raycast(playerPosition, Vector3.down, out hit))
//                {
//                    cubeItemProperties cip = hit.transform.GetComponent<cubeItemProperties>();
//                    if (!cip.isFinal)
//                    {
//                        yield return StartCoroutine(DoMotionOfPlayer(cip.cubeCurrentMotion));
//                    }
//                }
//            }
//            RaycastHit hit2;
//             //if (Physics.Raycast(transform.position, Vector3.down, out hit2))
//            //{
//            //    cubeItemProperties cip = hit2.transform.GetComponent<cubeItemProperties>();
//            //    if (cip.isShortcut)
//            //    {
//            //        for (int i = 0; i < cip.factor; i++)
//            //        {
//            //            yield return StartCoroutine(DoMotionOfPlayer(MotionType.fwd));
//            //        }
//            //    }
//            //    else if (cip.isPitfall)
//            //    {
//            //        for (int i = 0; i < cip.factor; i++)
//            //        {
//            //            yield return StartCoroutine(DoMotionOfPlayer(MotionType.back));
//            //        }
//            //    }
//            //    else if (cip.isFinal)
//            //    {
//            //        //this player has won the game
//            //        finalPanel.SetActive(true);
//            //        Text tmptext = finalPanel.transform.GetChild(0).GetComponent<Text>();
//            //        tmptext.text = gameObject.name + " has Won The match!!!"; 
//            //    }
//            //}
//        }