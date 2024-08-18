using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public int numOfTotalTasks;
    public int numOfTasksCompleted;
    public enum ListOfTotalTasks
    {

        // Note: - All tasks that have a '1' in their name need to be completed before '2' can be completed.
              // - Fix wiring tasks will only be done assigned to one location per round.

        // Admin Tasks
        cardSwipeAdmin,
        fixWiringAdmin,
        uploadDataAdmin,



        // Cafeteria Tasks
        emptyGarbageCaf,
        fixWiringCaf,
        uploadDataCaf,



        // Electrical
        fixWiringElec,
        calibrateDistributorElec,
        divertPowerElec,
        uploadDataElec,

        // Storage
        fixWiringStorage,
        fuelEngines1Storage,
        fuelEngines2Storage,
        emptyChuteStorage,
        emptyGarbageStorage,

        // Navigation
        fixWiringNav,
        chartCourseNav,
        divertPowerNav,
        stabilizeSteeringNav,
        uploadDataNav,

        // Security
        fixWiringSec,
        divertPowerSec,


        // Upper Engine
        alignEngineUpperEng,
        fuelEnginesUpperEng,
        divertPowerUpperEng,

        // Lower Engine
        alignEngineLowerEng,
        fuelEnginesLowerEng,
        divertPowerLowerEng,

        // Medbay
        inspectSampleMedbay,
        submitScanMedbay,

        // Reactor
        startReactor,
        unlockManifoldsReactor,

        // Weapons
        clearAsteroidsWeapons,
        divertPowerWeapons,
        uploadDataWeapons,

        // O2
        emptyChuteO2,
        cleanFilterO2,

        // Communications
        divertPowerComms,
        uploadDataComms,
        
        // Shields
        divertPowerShields,
        primeShields,




    }

    public enum ListOfAssignedTasks
    {
        // This will take a set number of the total tasks based on how many players are in the lobby
    }

    


     void Start()
    {
        numOfTasksCompleted = 0;
    }

     void Update()
    {
        if (numOfTasksCompleted >= numOfTotalTasks)
        {
            Debug.Log("All Tasks Completed, Crewmates win");
        }
    }
}
