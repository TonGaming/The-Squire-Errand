using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<GameSession>().Length;

        if (numScenePersists > 1)
        {
            Destroy(gameObject); // check xem nếu có nhiều ScenePersist thì xoá cái này đi 

        }
        else
        {
            DontDestroyOnLoad(gameObject); // nếu chỉ có một thì giữ nguyên
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
