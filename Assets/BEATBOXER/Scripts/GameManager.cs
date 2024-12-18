﻿using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//This script needs to be placed on the 'GameManager' game object in the hierarchy.
//Allows you to set a pre-defined number of cubes for your level.
//Also is informed when the Level has Completed. 


public class GameManager : MonoBehaviour
{
    
    [SerializeField] private int boxesInLevel = 100; //This value needs to be setup on the Game Manager object prior to game start.
    [SerializeField] private CubeSpawner cubeSpawner; ////access to the CubeSpawner class so you may access its OnSpawningComplete event

    public int BoxesInLevel => boxesInLevel; //get property

    private int cubeCount; //keeps a count of the number of cubes in the level. If cubeCount is zero, all cube blocks have been spawned. 
    private bool allCubesSpawned = false; //maintais the state of whether all cubes have been spawned. Only if all cubes have finished spawning, will you begin executing the code within Update() below.This ensures you don't run the code in update every frame while cubes are still being spawned.

    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject playerHUD;
    
    [SerializeField] private Text boxesHit;
    [SerializeField] private Text boxesMissed;
    [SerializeField] private Text boxesHitText;
    [SerializeField] private Text boxesMissedText;
    
    
    



    private void OnEnable()
    {
        cubeSpawner.OnSpawningComplete += AllCubesInLevelSpawned;
    }

    private void OnDisable()
    {
        cubeSpawner.OnSpawningComplete -= AllCubesInLevelSpawned;
    }



    private void Update()
    {
        if (!allCubesSpawned) //You dont need to unnecessarily count the number of cubes in the level each Update if all Cubes have not been spawned yet.
            return;

        int blueCubeCount = GameObject.FindGameObjectsWithTag("Blue Cube").Length;
        int redCubeCount = GameObject.FindGameObjectsWithTag("Red Cube").Length;

        cubeCount = blueCubeCount + redCubeCount;

        if (cubeCount <= 0) //if no active Cube in the level, raise  the OnLevelComplete event.
        {
            OnLevelComplete();
        }

    }


    void AllCubesInLevelSpawned()
    {
        allCubesSpawned = true;
    }

    void OnLevelComplete()
    {
        boxesHit.text = ("Boxes Hit: " + boxesHitText.GetComponent<Text>().text);
        boxesMissed.text = ("Boxes Missed: " + boxesMissedText.GetComponent<Text>().text);
        playerHUD.SetActive(false);
        winText.SetActive(true);
        Debug.Log("LEVEL, COMPLETED");

    }

}
