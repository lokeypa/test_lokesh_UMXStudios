using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager s_Instance;


    private void Awake()
    {
        if (s_Instance == null) {
            s_Instance = this;
        }
    }

    public void StartExecution()
    {
            StartCoroutine(PlayerMovement.s_Instance.StartExecution(1.25f));
    }

    public void StartExecution(int diceValue)
    {
        StartCoroutine(PlayerMovement.s_Instance.StartExecution(diceValue));
    }

    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void AddMotionToList(MotionType motionType)
    {
        //lst_MotionQueue.Add(motionType);
    }


}


public enum MotionType
{
    left,
    right,
    fwd,
    back,
}
