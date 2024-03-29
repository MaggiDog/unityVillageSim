﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RestPlace : MonoBehaviour, IRestPlace, IHouse {

    public List<GameObject> guests;
    private int capacity;
    private int restQuality;

    public int RestQuality { get { return restQuality; } set { restQuality = value; } }

    public int Capacity { get { return capacity; } set { capacity = value; } }

    public void AddGuest(GameObject guest)
    {
        if (!guests.Contains(guest))
            guests.Add(guest);
    }

    public List<GameObject> GetGuests()
    {
        return guests;
    }

    public void RemoveGuest()
    {
        foreach (GameObject guest in guests.ToArray())
        {
            if (guest.GetComponent<Human>().TirednesslLevel >= 100)
            {
                guest.GetComponent<Human>().TirednesslLevel = 100;
                guests.Remove(guest);
            }
        }

    }

    public void RemoveGuest(GameObject obj)
    {

        guests.Remove(obj);



    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Human>().CurrentNeed == (int)HumanNeeds.TIRED)
                AddGuest(other.gameObject);
        }


    }

   private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Human>().CurrentNeed == (int)HumanNeeds.TIRED)
                AddGuest(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Human>().CurrentNeed != (int)HumanNeeds.TIRED)
                RemoveGuest(other.gameObject);
        }
    }

    void Start()
    {
        guests = new List<GameObject>();
        restQuality = 25;
        Capacity = 5;
    }

    void FixedUpdate()
    {
        if (guests != null && guests.Count > 0)
        {
            RemoveGuest();
            int i = 1;
            foreach(GameObject guest in guests.ToArray())
            {
                if (i < capacity)
                {
                    guest.GetComponent<Human>().TirednesslLevel += restQuality * Time.deltaTime;
                    
                }
                i++;

            }
        }

    }
}
