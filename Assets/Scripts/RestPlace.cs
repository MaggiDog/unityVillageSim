using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RestPlace : MonoBehaviour, IRestPlace
{

    public List<GameObject> guests;
    private int capacity;
    private int restQuality;

    public int RestQuality { get { return restQuality; } set { restQuality = value; } }

    public int Capacity { get { return capacity; } set { capacity = value; } }

    public void AddGuest(GameObject guest)
    {
        if (guests.Count < capacity)
        {
            guests.Add(guest);
            Debug.Log("We've got new guest");
        }

    }

    public List<GameObject> GetGuests()
    {
        return guests;
    }

    public void RemoveGuest()
    {
        foreach (GameObject guest in guests)
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
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Human>().CurrentNeed == (int)HumanNeeds.TIRED)
                AddGuest(other.gameObject);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
            foreach (GameObject guest in guests)
            {
                guest.GetComponent<Human>().TirednesslLevel += restQuality * Time.deltaTime;

            }
        }
    }

}
