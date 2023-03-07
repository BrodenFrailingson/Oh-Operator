using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private AudioClip Tutorial;
    [SerializeField] private AudioSource m_Player;
    [SerializeField] private AudioClip[] audioClips; // Array of audio clips for levels
    [SerializeField] private string[] Input; // list of wanted inputs and outputs to complete each part
    [SerializeField] private string[] Output;
    [SerializeField] private int PassMark;
    [SerializeField] private GameObject AuxInput, AuxOutput, Button; // gameobject reference to the aux cable
    [SerializeField] private AudioClip failMessage, winMessage; // failure and victory audio clips
    private string nextlevel;
     // general variables for controling win/lose conditions and progress through each level
    private int Score = 0;
    private int index = 0;
    private int Called = 0;

    private bool Answered() => Called != 0;

    private void Start()
    {
        m_Player.volume = 0.8f;

        int Level_Index = SceneManager.GetActiveScene().buildIndex;

        nextlevel = (Level_Index == 2 || Level_Index == 3) ? "Main Menu" : "Level 2";

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            m_Player.PlayOneShot(Tutorial);
        }
        
        Button.GetComponent<HoverButton>().onButtonDown.AddListener(OnCall);

    }

    private void OnCall(Hand hand)
    {
        StartCoroutine(CheckAux());
        StopCoroutine(CheckAux());
    }

    private IEnumerator CheckAux()
    {
        if (!Answered())
            yield break;
        // retrieving tag of aux input
        string inputAnswer = AuxInput.GetComponent<AuxCord>().GetTag();
        string outputAnswer = AuxOutput.GetComponent<AuxCord>().GetTag();

        Debug.Log(inputAnswer);
        Debug.Log(outputAnswer);
        //mesuring recieved tags against desired answers
        if (index < audioClips.Length && ((inputAnswer == Input[index] || inputAnswer == Output[index]) && (outputAnswer == Input[index] || outputAnswer == Output[index]) && (inputAnswer != outputAnswer)))
        {
            // increases pass mark
            Score++;
        }

        // moves to the next audio clip and set of answers in arrays
        index++;
        // testing win/lose conditions
        if (index >= audioClips.Length && Score < PassMark)
        {
            // play fail message and reload scene
            m_Player.PlayOneShot(failMessage);
            yield return new WaitForSeconds(5);
            string level = SceneManager.GetActiveScene().name;
            SteamVR_LoadLevel.Begin(level);

            yield break;


        }
        else if (index >= audioClips.Length && Score >= PassMark)
        {
            //play victory message and load next level
            m_Player.PlayOneShot(winMessage);
            yield return new WaitForSeconds(5);

            SteamVR_LoadLevel.Begin(nextlevel);

            yield break;

        }

        //Allow the call to only be heard once
        Called = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // plays audioclip at position index and only plays it once
        if (!other.gameObject.CompareTag("MainCamera"))
            return;

        if (Called < 3 && !m_Player.isPlaying)
        {
            m_Player.PlayOneShot(audioClips[index]);
            Called++;
        }
    }


}
