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
    }



    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay); // current load delay is 2 seconds

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;


        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);

    }
}
