using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHouse
{
    List<GameObject> GetGuests();
    void AddGuest(GameObject guest);

    void RemoveGuest();

    int Capacity { get; set; }

}
