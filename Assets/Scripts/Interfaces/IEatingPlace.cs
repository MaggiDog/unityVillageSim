using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEatingPlace
{
    List<GameObject> GetGuests();
    int FoodQuality { get; set; }

    void AddGuest(GameObject guest);

    void RemoveGuest();

    int Capacity { get; set; }


}
