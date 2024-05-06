using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{


    void Awake()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
#pragma warning restore CS0618 // Type or member is obsolete

        if (numScenePersists > 1)
        {
            Destroy(gameObject); // check xem nếu có nhiều ScenePersist thì xoá cái này đi 

        }
        else
        {
            DontDestroyOnLoad(gameObject); // nếu chỉ có một thì giữ nguyên
        }
    }
    public void ResetScenePersist()
    {
        Destroy(gameObject); // huỷ scenePersist của màn chơi đã xong đi 
    }
}
