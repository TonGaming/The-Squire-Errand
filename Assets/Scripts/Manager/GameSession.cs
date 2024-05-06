﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 2f;

    // Số máu, số coin của người chơi lúc bắt đầu
    [SerializeField] int playerStartingHealth = 3;
    [SerializeField] int playerStartingCoin = 0;

    // Số máu trong game Session
    [SerializeField] int playerCurrentLives;
    [SerializeField] int playerCurrentScore;

    // Số máu trong game Session
    [SerializeField] Image healthBar;
    // Số coin trong game Session
    [SerializeField] TextMeshProUGUI coinText;

    //[SerializeField] Image playerHealthBarUI;

    // Awake sẽ được gọi when this script is brought to life(push the play button) or when we reload the scene
    [System.Obsolete]
    void Awake()
    {
        // Singleton Pattern

        // FindObjectsOfType sẽ là tìm ra một mảng tổng hợp tất cả những object đó
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        if (numGameSession > 1)
        {
            Destroy(gameObject); // có nhiều gameSession hơn thì xoá đi 
            gameObject.SetActive(false);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        playerCurrentLives = playerStartingHealth;
        playerCurrentScore = playerStartingCoin;

        // Hiện UI thanh máu dựa trên số playerCurrentLives trong Game Session
        healthBar.fillAmount = playerCurrentLives * 0.1f;


        // Hiện UI số coin = playerCurrentScore
        coinText.text = "000";
    }

    void Update()
    {


    }

    public void ProcessPlayerDeath()
    {
        if (playerCurrentLives > 1)
        {
            MinusLives();
        }
        else
        {
            // trừ nốt trái tim cuối cùng
            playerCurrentLives--;
            healthBar.fillAmount = playerCurrentLives * 0.1f;

            Invoke("ResetGameSession", reloadSceneDelay);
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        playerCurrentScore += pointsToAdd;

        if (playerCurrentScore < 100)
        {
            coinText.text = "0" + playerCurrentScore.ToString();
        }
        else
        {
            // chỉ chạy một lần khi được gọi tới chứ nếu để trong update thì nó chạy theo frame
            coinText.text = playerCurrentScore.ToString();
        }
    }

    public void AddHealth()
    {
        if (playerCurrentLives < 10)
        {
            IncreaseLives();
            return;
        }
        else
        {
            return;
        }
    }

    void MinusLives()
    {
        // chỉ thao tác với playerCurrentLives, UI sẽ tự thay đổi theo
        playerCurrentLives--;

        // Hiện UI thanh máu dựa trên số playerCurrentLives trong Game Session
        healthBar.fillAmount = playerCurrentLives * 0.1f;

        StartCoroutine(ReloadLevels());
    }

    void IncreaseLives()
    {
        playerCurrentLives++;
        healthBar.fillAmount = playerCurrentLives * 0.1f;

    }

    IEnumerator ReloadLevels()
    {
        yield return new WaitForSecondsRealtime(reloadSceneDelay);


        // lấy ra index của scene hiện tại
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        // load lại scene đó sau chừng 2s
        SceneManager.LoadScene(sceneBuildIndex);
    }

    void ResetGameSession()
    {

        FindAnyObjectByType<ScenePersist>().ResetScenePersist();

        // Sau khi đã huỷ game Session cũ đi thì load ra scene thua
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // assuming the first scene is always 0-indexed

        // Khi reset thì huỷ gameSession này đi để load lại cái mới ra,
        // k huỷ đi thì sẽ có 2 gameSession hoạt động cùng lúc và điều đó rất là không hay (lỗi)
        Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void KillGameSession()
    {
        FindAnyObjectByType<ScenePersist>().ResetScenePersist();
        Destroy(gameObject);
        gameObject.SetActive(false);

    }
}
