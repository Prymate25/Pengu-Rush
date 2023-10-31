using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{   public GameObject player1;
    public GameObject player2;

    public Button button1;
    public Button button2;

    private int selection;
    void Start(){
        selection=0;
        player1.SetActive(true);
        player2.SetActive(false);

        button1.onClick.AddListener(SelectPlayerOne);
        button2.onClick.AddListener(SelectPlayerTwo);

    }
    public void SelectPlayerOne()
    {   
        selection=0;
        // Set the selected player to Player 1
        //GameManager.Instance.isPlayerOneActive = true;
        player1.SetActive(true);
        player2.SetActive(false);
        //SceneManager.LoadScene("Game"); // Load the game scene
    }

    public void SelectPlayerTwo()
    {   selection=1;
        // Set the selected player to Player 2
        //GameManager.Instance.isPlayerOneActive = false;
        player2.SetActive(true);
        player1.SetActive(false);
        //SceneManager.LoadScene("Game");// Load the game scene
    }
}
