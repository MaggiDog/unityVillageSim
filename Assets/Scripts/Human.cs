using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[System.Serializable]
public class Human : MonoBehaviour, IHuman
{
    
    public int ID;

    private NavMeshAgent agent;

    [SerializeField]
    private List<GameObject> foods;
    [SerializeField]
    private List<GameObject> rests;
    [SerializeField]
    private float wanderTime = 4;
    [SerializeField]
    private float wanderTimer;


    [SerializeField]
    private int currentState;
    [SerializeField]
    private int currentNeed;
    [SerializeField]
    private float tirednessLevel;
    [SerializeField]
    private float hungryLevel;
    private int hungerPriority;
    private int tirednessPriority;

    private List<int> needs;

    public float TirednesslLevel { get { return tirednessLevel; } set { tirednessLevel = value; } }
    public float HungryLevel { get { return hungryLevel; } set { hungryLevel = value; } }
    public int CurrentState { get { return currentState; } set { currentState = value; } }
    public int CurrentNeed { get { return currentNeed; } set { currentNeed = value; } }
    public int HungerPriority { get { return hungerPriority; } set { hungerPriority = value; } }
    public int TirednessPriority { get { return tirednessPriority; } set { tirednessPriority = value; } }

    public void HungerTick(float multiplier)
    {
        HungryLevel -= multiplier * Time.deltaTime;
    }

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public void TirednessTick(float multiplier)
    {
        TirednesslLevel -= multiplier * Time.deltaTime;
    }

    public List<int> GetNeeds()
    {
        return needs;
    }
    public  void AddNeeds(int value)
    {
        if (!needs.Contains(value))
            needs.Add(value);
    }


    public void StateMachine()
    {
        switch (CurrentState)
        {
            case (int)HumanStates.IDLE:

                break;
            case (int)HumanStates.WALKING:

                break;
            case (int)HumanStates.RUNNING:

                break;

        }

        if (agent.velocity.magnitude > 0)
            currentState = (int)HumanStates.WALKING;
        if (agent.velocity.magnitude == 0)
            currentState = (int)HumanStates.IDLE;




    }

    private GameObject FindNearestPlace(List<GameObject> places)
    {
        
        float _distanceMin = 0;
        float _distance = 0;
     
        GameObject _obj = null;

            foreach (GameObject _place in places)
        {
            if (_distanceMin == 0) {
                _distanceMin = Vector3.Distance(transform.position, _place.transform.position);
                _obj = _place;
            }

            _distance = Vector3.Distance(transform.position, _place.transform.position);
            if (_distance < _distanceMin)
            {
                _distanceMin = _distance;
                _obj = _place;
            }
        }
        return _obj;
       
    }

    public void Wander()
    {
        if (wanderTimer <= 0 || agent.velocity.magnitude == 0)
        {
            Vector3 newPos = RandomNavSphere(transform.position, 10, -1);
            MoveTo(newPos);
            wanderTimer = wanderTime;
        }
        wanderTimer -= 1 * Time.deltaTime;
    }

    public void GetFood()
    {
        MoveTo(FindNearestPlace(foods).transform.position);
        
        
    }
    public void GetRest()
    {
        MoveTo(FindNearestPlace(rests).transform.position);
    }

    public void NeedsMachine()
    {
        if (hungryLevel <= 0)
            currentNeed = (int)HumanNeeds.HUNGRY;
        if (tirednessLevel <= 0 && hungryLevel >= 50)
            currentNeed = (int)HumanNeeds.TIRED;

        if (tirednessLevel >= 50 && hungryLevel >= 50)
            currentNeed = (int)HumanNeeds.WANDER;

        switch (currentNeed)
        {
            case (int)HumanNeeds.HUNGRY:
                GetFood();
                break;
            case (int)HumanNeeds.TIRED:
                GetRest();
                break;
            case (int)HumanNeeds.WANDER:
                Wander();
                break;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        wanderTimer = wanderTime;

        GameObject[] obj = GameObject.FindGameObjectsWithTag("FoodPlace");
        foreach(GameObject _obj in obj)
        {
            foods.Add(_obj);
        }

        obj = GameObject.FindGameObjectsWithTag("RestPlace");
        foreach (GameObject _obj in obj)
        {
            rests.Add(_obj);
        }

    }

   

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


    // Update is called once per frame
    void Update()
    {
        NeedsMachine();
        StateMachine();
        TirednessTick(1.1f);
        HungerTick(0.25f);
       




    }
}
