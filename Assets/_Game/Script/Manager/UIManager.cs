using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainmenuUI;
    public GameObject finishUI;


    public void OpenMainMenu()
    {
        mainmenuUI.SetActive(true);
        finishUI.SetActive(false);
    }
    public void OpenFinishUI()
    {
        mainmenuUI.SetActive(false);
        finishUI.SetActive(true);
    }
    public void PlayButton()
    {
        mainmenuUI.SetActive(false);
        LevelManager.Instance.OnStart();
        
    }

    public void RetryButton()
    {
        LevelManager.Instance.LoadLevel();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        OpenMainMenu();
    }

    public void NextButton()
    {
        LevelManager.Instance.NextLevel();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        OpenMainMenu();
    }
}
