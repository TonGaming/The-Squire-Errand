using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    BoxCollider2D doorBoxCollider;



    [SerializeField] float LevelLoadDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        doorBoxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        OnExit();

    }

    void OnExit()
    {
        if (doorBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {

            StartCoroutine(LoadNextLevel());
        }
        else
        {
            return;
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay); // current load delay is 2 seconds

        FindAnyObjectByType<ScenePersist>().ResetScenePersist();  // Xoá ScenePersist đi để load ra cái mới 

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // lấy ra index của scene hiện tại

        int nextSceneIndex = currentSceneIndex + 1;  // index của scene tiếp theo sẽ bằng scene hiện tại + 1


        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // nếu index scene tiếp bằng tổng số lượng scene trong build setting, chẳng hạn có 5 levels mà nextSceneIndex = 5 -> thì reset lại về 0
        {
            FindAnyObjectByType<GameSession>().KillGameSession();
            SceneManager.LoadScene(0); // khi reset lại scene index = 0 thì sẽ load lại map đầu tiên 

        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex); // Loadlevel kế dựa trên currentSceneIndex + 1

        }

    }

}
