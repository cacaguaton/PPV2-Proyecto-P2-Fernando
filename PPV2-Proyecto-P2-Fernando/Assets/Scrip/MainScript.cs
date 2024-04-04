using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public static MainScript instance;
    public string SelectedLesson = "dummy";

    private void Awake()
    {
        if (instance != null)
        {
            return;

        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    public void SetSelectedLesson(string lesson)
    {
        SelectedLesson = lesson;
        PlayerPrefs.SetString("SelectedLesson", SelectedLesson);
    }

    // Update is called once per frame
    public void BeginGame()
    {
        SceneManager.LoadScene("Lesson");
    }
}
