using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum HumanStates
{
    IDLE,
    WALKING,
    RUNNING

}

enum HumanNeeds
{
    HUNGRY,
    TIRED,
    WANDER

}



public interface IHuman 
{

    #region variables
    float TirednesslLevel { get; set; }
    float HungryLevel { get; set; }
    int CurrentState { get; set; }
    int   HungerPriority { get; set; }
    int   TirednessPriority { get; set; }

    List<int> GetNeeds();
    void AddNeeds(int value);
    #endregion

    void MoveTo(Vector3 target);

    void HungerTick(float multiplier);

    void TirednessTick(float multiplier);

    void GetFood();
    void GetRest();







}
