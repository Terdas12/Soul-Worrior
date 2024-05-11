using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    private int current_char;
    [SerializeField] GameObject[] Players;
    public int SceneNumber;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void ButtonNext()
    {
        if (current_char == Players.Length-1)
        {
            return;
        }

        Players[current_char].SetActive(false);
        current_char++;
        Players[current_char].SetActive(true);
    }

    public void ButtonPrev() 
    {
        if(current_char == 0)
        {
            return;
        }
        Players[current_char].SetActive(false);
        current_char--;
        Players[current_char].SetActive(true);
    }

    public void Transition()
    {
        SceneManager.LoadScene(SceneNumber);
    }

}
