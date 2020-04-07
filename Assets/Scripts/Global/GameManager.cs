using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameSettings))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public GameSettings Settings;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
