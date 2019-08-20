using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodPlace : MonoBehaviour, IEatingPlace, IHouse { 

    public List<GameObject> guests;
    private int capacity;
    private int foodQuality;

    public int FoodQuality { get { return foodQuality; } set { foodQuality = value; } }

    public int Capacity { get { return capacity; } set { capacity = value; } }

    public void AddGuest(GameObject guest)
    {
        if(!guests.Contains(guest))
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
            if (guest.GetComponent<Human>().HungryLevel >= 100)
            {
                guest.GetComponent<Human>().HungryLevel = 100;
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
            if(other.gameObject.GetComponent<Human>().CurrentNeed == (int)HumanNeeds.HUNGRY)
            {
                AddGuest(other.gameObject); 
            }
            
        }

 
    }
    private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Human>().CurrentNeed == (int)HumanNeeds.HUNGRY)
                AddGuest(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Human>().CurrentNeed != (int)HumanNeeds.HUNGRY)
            {
                RemoveGuest(other.gameObject);
            }
        }
    }

    void Start()
    {
        guests = new List<GameObject>();
        FoodQuality = 25;
        Capacity = 5;
    }

    void Update()
    {

        if (guests != null && guests.Count > 0)
        {
            RemoveGuest();
            int i = 1;
            foreach(GameObject guest in guests.ToArray())
            {
                if (i < capacity)
                {
                    guest.GetComponent<Human>().HungryLevel += foodQuality * Time.deltaTime;

                }
                else
                {
                    break;
                }
                i++;

            }
        }
    }

}
