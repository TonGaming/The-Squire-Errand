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
    [SerializeField] float playerStartingHealth = 3f;
    [SerializeField] float playerStartingCoin = 0f;

    // Số máu trong game Session
    [SerializeField] float playerCurrentLives;
    [SerializeField] float playerCurrentScore;

    // Số máu trong game Session
    [SerializeField] Image healthBar;
    // Số coin trong game Session
    [SerializeField] TextMeshProUGUI coinText;

    //[SerializeField] Image playerHealthBarUI;

    // Awake sẽ được gọi when this script is brought to life(push the play button) or when we reload the scene
    void Awake()
    {
        // Singleton Pattern

        playerCurrentLives = playerStartingHealth;
        playerCurrentScore = playerStartingCoin;

        // FindObjectsOfType sẽ là tìm ra một mảng tổng hợp tất cả những object đó
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        if (numGameSession > 1)
        {
            Destroy(gameObject); // có nhiều gameSession hơn thì xoá đi 

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Hiện UI thanh máu dựa trên số playerCurrentLives trong Game Session
        healthBar.fillAmount = playerCurrentLives * 0.1f;


        // Hiện UI số coin = playerCurrentScore
        coinText.text = "0" + playerCurrentScore.ToString();
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

    public void ProcessPlayerScore()
    {


        if (playerCurrentScore < 10)
        {
            // để đoạn này vào trong hàm thì sẽ performant hơn là để ở update
            coinText.text = "000" + playerCurrentScore.ToString();

        }
        else if (playerCurrentScore < 100)
        {
            coinText.text = "0" + playerCurrentScore.ToString();

        }
        else
        {
            // chỉ chạy một lần khi được gọi tới chứ nếu để trong update thì nó chạy theo frame
            coinText.text = playerCurrentScore.ToString();
        }

    }

    public void AddToScore(int pointsToAdd)
    {
        playerCurrentScore += pointsToAdd;
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

        // lấy ra index của scene hiện tại
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneBuildIndex);

        playerCurrentLives = playerStartingHealth;
        playerCurrentScore = playerStartingCoin;

        healthBar.fillAmount = playerCurrentLives * 0.1f;

        if (playerCurrentScore < 10)
        {
            // để đoạn này vào trong hàm thì sẽ performant hơn là để ở update
            coinText.text = "00" + playerCurrentScore.ToString();

        }
        else if (playerCurrentScore < 100)
        {
            coinText.text = "0" + playerCurrentScore.ToString();

        }
        else
        {
            // chỉ chạy một lần khi được gọi tới chứ nếu để trong update thì nó chạy theo frame
            coinText.text = playerCurrentScore.ToString();
        }
    }
}
