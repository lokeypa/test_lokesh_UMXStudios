﻿using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public static bool isDiceRolling = false;

    // Use this for initialization
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    private void OnMouseDown()
    {
        if (!isDiceRolling)
        {
            StartCoroutine("RollTheDice");
            isDiceRolling = true;
        }
    }

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;
        int finalSide = 0;

        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 5);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }
        
        finalSide = randomDiceSide + 1;
        
        GameplayManager.s_Instance.SkipCheckBeforeExecute(finalSide);
    }
}