using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tepScript : MonoBehaviour
{

    public Button[] all;
    public Button robot;

    private void Start()
    {

        robot.onClick = null;
        BaseEventData baseEvent = new BaseEventData(EventSystem.current);
        ExecuteEvents.Execute(robot.gameObject, baseEvent, ExecuteEvents.submitHandler);

       // ExecuteEvents.Execute(robot, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
        // robot.onClick.Invoke();
        //all = GetComponentsInChildren<Button>();
        // StartCoroutine(sdjfh(2.5f));
    }


    IEnumerator sdjfh(float time)
    {
        for (int i = 0; i < all.Length; i++)
        {
            yield return new WaitForSeconds(time);
            all[i].Select();
            yield return new WaitForSeconds(time);
            all[i].onClick.Invoke();

            yield return new WaitForSeconds(time);
            all[i].interactable = false;


        }

    }

}
