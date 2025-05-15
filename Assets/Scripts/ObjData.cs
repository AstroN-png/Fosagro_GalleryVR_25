using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjData : MonoBehaviour
{
    public static ObjData instance;
    
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject resultMenu;
    public GameObject descriptionPanel;
    public GameObject gamePanel;
    public GameObject[] filialPanel;



    private void Awake()
    {
        instance = this;
    }

   public void OpenMainMenu(bool condition)
    {
        AllDisable();
        mainMenu.SetActive(condition);
    }

    public void OpenResultPanel()
    {
        AllDisable();
        resultMenu.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        AllDisable();
        pauseMenu.SetActive(true);
       // GameController.instance.GamePause(true);
    }

    public void OpenMainMenu()
    {
        AllDisable();
        mainMenu.SetActive(true);
    }

    public void OpenGamePanel()
    {
        AllDisable();
        gamePanel.SetActive(true);
    }

    public void OpenFilialPanel(int id)
    {
        AllDisable();
        filialPanel[id].SetActive(true);
        
    }

    public void OpenPanel()
    {
        if (GameController.instance.newGame)
        {
            OpenMainMenu(true);
        }
        else
        {
            OpenPauseMenu();
        }
    }

    public void OpenGamePanel(bool condition)
    {
        AllDisable();
        gamePanel.SetActive(condition);
        

    }

    public void OpenDescriptionPanel()
    {
        descriptionPanel.SetActive(true);
        Invoke("CloseDescriptionPanel", 10);
    }

    void CloseDescriptionPanel()
    {
    descriptionPanel.SetActive(false); 
    }

    void AllDisable()
    {
        mainMenu.SetActive(false);
        descriptionPanel.SetActive(false);
        gamePanel.SetActive(false);
        pauseMenu.SetActive(false);
        resultMenu.SetActive(false);
        for (int i = 0; i < filialPanel.Length; i++)
        {
            filialPanel[i].SetActive(false);
        }
        
    }
}
