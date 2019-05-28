using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Controller : MonoBehaviour
{
    public GameObject _startMenu;
    public GameObject _setting;


    public void Play()
    {
        _startMenu.SetActive(false);
    }

    public void _pauseMenu()
    {
        _startMenu.SetActive(true);
    }

    public void SettingsMenu()
    {
        _setting.SetActive(true);

    }
    public void exitSettings()
    {
        _setting.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
