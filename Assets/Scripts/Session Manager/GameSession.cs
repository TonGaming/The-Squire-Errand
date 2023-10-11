using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 2f;
    [SerializeField] float playerLives = 3;
    [SerializeField] float playerCoin = 0;



    // Awake sẽ được gọi when this script is brought to life(push the play button) or when we reload the scene
    void Awake()
    {
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

    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            MinusLives();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void ProcessPlayerCoin()
    {
        playerCoin++; 

        
    }

    void MinusLives()
    {
        // trừ một máu
        playerLives --;

        StartCoroutine(ReloadLevels());

       
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
        playerLives = 3;
        playerCoin = 0;
    }
}
