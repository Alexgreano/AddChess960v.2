using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour
{
    private string randomChess = "Chess960";
    private string traditionalChess = "ChessTraditional";

    public void Load960()
    {
        SceneManager.LoadScene(randomChess);
    }

    public void LoadTraditional()
    {
        SceneManager.LoadScene(traditionalChess);
    }
}
