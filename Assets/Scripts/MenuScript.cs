using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject Play, Tut, Quit;
    // load level 1
    public void Start()
    {
        Play.GetComponent<HoverButton>().onButtonDown.AddListener(LoadGame);
        Tut.GetComponent<HoverButton>().onButtonDown.AddListener(LoadTutorial);
        Quit.GetComponent<HoverButton>().onButtonDown.AddListener(QuitGame);
    }
    public void LoadGame(Hand hand)
    {

        SteamVR_LoadLevel.Begin("Level 1");
    }

    public void LoadTutorial(Hand hand)
    {
        SteamVR_LoadLevel.Begin("Tutorial");
    }
    //quit the application
    public void QuitGame(Hand hand)
    {
        Application.Quit();
    }
}
