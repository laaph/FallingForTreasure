using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        if(score != 0)
        {
            scoreText.text = $"Last score: {score}";
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    public void OnButtonPress(int sceneToLoad)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
