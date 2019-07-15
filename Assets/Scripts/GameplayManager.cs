using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager s_Instance;

    // including turn base multiplayer.

    public PlayerTurn m_currentPlayerTurn = PlayerTurn.PlayerRed;
    public SpriteRenderer DiceSpriteRenderar;
    public TextMesh playerIndecatortextMesh;

    public GameObject SkipPanel = null;
    public GameObject startPanel = null;


    private int m_presentedDiceValue = 0;

    private void Awake()
    {
        if (s_Instance == null) {
            s_Instance = this;
        }
        HideSkipPanel();
    }

    public void StartTheGame()
    {
        startPanel.SetActive(false);
    }

    public void SkipCheckBeforeExecute(int dicevalue)
    {
        m_presentedDiceValue = dicevalue;
        PlayerMovement currentPlayerScript = GameObject.FindGameObjectWithTag(m_currentPlayerTurn.ToString()).GetComponent<PlayerMovement>();
        if (m_presentedDiceValue == 6)
        {
            if (currentPlayerScript.previousRecordedValue == 6)
            {
                //skip this turn do  not move
                m_currentPlayerTurn = SwitchTurns(m_currentPlayerTurn);
                return;
            }
        }

        if (!currentPlayerScript.hasAlreadySkipped)
        {
            if (currentPlayerScript.hasSkippedPreviousTurn)
            {
                m_presentedDiceValue += currentPlayerScript.previousRecordedValue;
                currentPlayerScript.hasAlreadySkipped = true;
            }
            //showpanel for skipth thing and stat coroutine for this after some time let say 2 seconds and we will give a single second to skip the turn.
            SkipPanel.SetActive(true);
            Invoke("HideSkipPanel", 1.5f);
        }
        //show button to skip that thing.
        StartCoroutine(StartExecution(m_presentedDiceValue, 2f));
    }

    public void HideSkipPanel()
    {
        SkipPanel.SetActive(false);
    }

    //code for skipbutton.
    public void ShouldStopAllCoroutines(bool forSkippingTurn)
    {
        if (forSkippingTurn)
        {
            PlayerMovement currentPlayerScript = GameObject.FindGameObjectWithTag(m_currentPlayerTurn.ToString()).GetComponent<PlayerMovement>();
            currentPlayerScript.hasSkippedPreviousTurn = true;
            currentPlayerScript.previousRecordedValue = m_presentedDiceValue;
        }
        HideSkipPanel();
        m_currentPlayerTurn = SwitchTurns(m_currentPlayerTurn);
        StopCoroutine(StartExecution(0,0));
    }

    IEnumerator StartExecution(int diceValue , float time)
    {
        yield return new WaitForSecondsRealtime(time);

        switch (m_currentPlayerTurn)
        {
            case PlayerTurn.PlayerRed :
                {
                   yield return StartCoroutine(GameObject.FindGameObjectWithTag("PlayerRed").GetComponent<PlayerMovement>().StartExecution(diceValue));
                }
                break;
            case PlayerTurn.PlayerBlue:
                {
                    yield return StartCoroutine(GameObject.FindGameObjectWithTag("PlayerBlue").GetComponent<PlayerMovement>().StartExecution(diceValue));
                }
                break;
            case PlayerTurn.PlayerYellow:
                {
                    yield return StartCoroutine(GameObject.FindGameObjectWithTag("PlayerYellow").GetComponent<PlayerMovement>().StartExecution(diceValue));
                }
                break;
            case PlayerTurn.PlayerGreen:
                {
                    yield return StartCoroutine(GameObject.FindGameObjectWithTag("PlayerGreen").GetComponent<PlayerMovement>().StartExecution(diceValue));
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
        PlayerTurn tempturn = currentPlayerturn == PlayerTurn.PlayerGreen ? PlayerTurn.PlayerRed : (PlayerTurn)((int)currentPlayerturn + 1);

        switch (tempturn)
        {
            case PlayerTurn.PlayerRed:
                {
                    DiceSpriteRenderar.color = Color.red;
                    playerIndecatortextMesh.text = "Red's\nTurn";
                }
                break;
            case PlayerTurn.PlayerBlue:
                {
                    DiceSpriteRenderar.color = Color.blue;
                    playerIndecatortextMesh.text = "Blue's\nTurn";
                }
                break;
            case PlayerTurn.PlayerYellow:
                {
                    DiceSpriteRenderar.color = Color.yellow;
                    playerIndecatortextMesh.text = "Yellow's\nTurn";
                }
                break;
            case PlayerTurn.PlayerGreen:
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
    PlayerRed,
    PlayerBlue,
    PlayerYellow,
    PlayerGreen
}