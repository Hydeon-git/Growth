using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    // PAUSE
    public static bool GameIsPaused;
    public GameObject Pause;
    // ABILITY
    public static bool AbilityInfoIsActive;
    // GAVE LIVES MANAGER
    public int GaveLives = 3;
    // MUSIC
    public AudioSource normal;
    public AudioSource diablo;
    public bool diabloIsOn;

    private void Start()
    {
        normal.Play();
    }
    void Update ()
    {
        // ESC or P to enter pause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Continue();
                if (!diabloIsOn)
                {
                    normal.UnPause();
                }
                else if (diabloIsOn)
                {
                    diablo.UnPause();
                }
            }
            else
            {
                PauseGame();
                if (!diabloIsOn)
                {
                    normal.Pause();
                }
                else if (diabloIsOn)
                {
                    diablo.Pause();
                }
            }
        }
    }
    // Continue and PauseGame methods for enter and exit Pause Menu
    public void Continue()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void PauseGame()
    {
        Pause.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }    
    // Method that --GaveLives when its called
    public void DownGaveLives()
    {
        GaveLives--;
        Debug.Log(GaveLives);
    }
    public void UpGaveLives()
    {
        if (GaveLives < 3)
        {
            GaveLives++;
            Debug.Log(GaveLives);
        }
        else
        {

        }
        
    }
    // Restarts the scene with a delay
    public void RestartScene()
    {              
            SceneManager.LoadScene("GrowthGame");
    }
    public void StartDiabloMusic()
    {
        normal.Stop();
        diablo.Play();
    }
    public void StopDiabloMusic()
    {        
        diablo.Stop();
        normal.Play();
    }

}
