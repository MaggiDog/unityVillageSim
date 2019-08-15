using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRestPlace { 

    List<GameObject> GetGuests();
    int RestQuality { get; set; }

    void AddGuest(GameObject guest);

    void RemoveGuest();

    int Capacity { get; set; }
}
