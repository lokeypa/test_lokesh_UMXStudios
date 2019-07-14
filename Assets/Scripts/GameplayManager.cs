using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager s_Instance;

    // including turn base multiplayer.

    public PlayerTurn m_currentPlayerTurn = PlayerTurn.red;
    public SpriteRenderer DiceSpriteRenderar;
    public TextMesh playerIndecatortextMesh;


    private void Awake()
    {
        if (s_Instance == null) {
            s_Instance = this;
        }
    }


    public void StartExecution(int diceValue)
    {
        switch (m_currentPlayerTurn)
        {
            case PlayerTurn.red :
                {
                    StartCoroutine(GameObject.FindGameObjectWithTag("PlayerRed").GetComponent<PlayerMovement>().StartExecution(diceValue));
                }
                break;
            case PlayerTurn.blue:
                {
                    StartCoroutine(GameObject.FindGameObjectWithTag("PlayerBlue").GetComponent<PlayerMovement>().StartExecution(diceValue));
                }
                break;
            case PlayerTurn.yellow:
                {
                    StartCoroutine(GameObject.FindGameObjectWithTag("PlayerYellow").GetComponent<PlayerMovement>().StartExecution(diceValue));
                }
                break;
            case PlayerTurn.green:
                {
                    StartCoroutine(GameObject.FindGameObjectWithTag("PlayerGreen").GetComponent<PlayerMovement>().StartExecution(diceValue));
                }
                break;
        }

        m_currentPlayerTurn = SwitchTurns(m_currentPlayerTurn);

    }

    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    public PlayerTurn SwitchTurns(PlayerTurn currentPlayerturn)
    {
        PlayerTurn tempturn = currentPlayerturn == PlayerTurn.green ? PlayerTurn.red : (PlayerTurn)((int)currentPlayerturn + 1);

        switch (tempturn)
        {
            case PlayerTurn.red:
                {
                    DiceSpriteRenderar.color = Color.red;
                    playerIndecatortextMesh.text = "Red's\nTurn";
                }
                break;
            case PlayerTurn.blue:
                {
                    DiceSpriteRenderar.color = Color.blue;
                    playerIndecatortextMesh.text = "Blue's\nTurn";
                }
                break;
            case PlayerTurn.yellow:
                {
                    DiceSpriteRenderar.color = Color.yellow;
                    playerIndecatortextMesh.text = "Yellow's\nTurn";
                }
                break;
            case PlayerTurn.green:
                {
                    DiceSpriteRenderar.color = Color.green;
                    playerIndecatortextMesh.text = "Green's\nTurn";
                }
                break;
        }
        return tempturn;
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
    back
}


public enum PlayerTurn
{
    red,
    blue,
    yellow,
    green
}