using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
    public GameObject gameOverGameObject;
    public GameObject playAgainButton;
    public GameObject winGameObject;
    public int transitionDuration = 3;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }


    public IEnumerator EndGame()
    {

        gameOverGameObject.SetActive(true);
        gameOverGameObject.GetComponent<Image>().DOFade(1, transitionDuration);
        yield return new WaitForSeconds(transitionDuration);
        playAgainButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator  WinGame()
    {
        winGameObject.SetActive(true);
        winGameObject.GetComponent<Image>().DOFade(1, transitionDuration);
        yield return new WaitForSeconds(transitionDuration);
        playAgainButton.SetActive(true);
        Time.timeScale = 0;

    }
}
