using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] AudioSource ambientSound;

    [SerializeField] Sprite pauseUI;
    [SerializeField] Sprite continueUI;

    [SerializeField] bool isPause;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TogglePauseGame()
    {
        if (!isPause)
        {
            ambientSound.Pause();
            GetComponent<Image>().sprite = continueUI;

            Time.timeScale = 0;

            isPause = true;
        }
        else
        {
            ambientSound.Play();

            GetComponent<Image>().sprite = pauseUI;

            Time.timeScale = 1;

            isPause = false;
        }
    }
}
