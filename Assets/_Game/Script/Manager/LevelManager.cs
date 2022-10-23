using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public PlayerMovement player;
    Level currentLevel;

    int level = 1;

    private void Start()
    {
        UIManager.Instance.OpenMainMenu();
        LoadLevel();
    }

    public void LoadLevel()
    {
        LoadLevel(level);
        OnInit();
    }

    public void LoadLevel(int indexLevel)
    {
        //neu dang o lv cu thi destroy
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        //gan level moi vao current
        currentLevel = Instantiate(levels[indexLevel - 1]);
    }

    public void OnInit()
    {
        player.transform.position = currentLevel.startPont.position;
        player.OnInit();
    }

    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }

    public void OnFinish()
    {
        UIManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public void NextLevel()
    {
        level++;
        LoadLevel();
    }
}
