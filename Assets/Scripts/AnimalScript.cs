using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimalScript : MonoBehaviour
{

    [SerializeField] float animalHealth;
    [SerializeField] float hunger;
    [SerializeField] float reproduce;
    [SerializeField] Vector2 xLimits;
    [SerializeField] Vector2 zLimits;
    private Animator animalAnimator;
    private float timeToMove;
    private float timeToDestroy;
    private List<GameObject> possibleVictims = new List<GameObject>();
    private List<GameObject> possibleCouples = new List<GameObject>();
    private Vector3 temp;
    private NavMeshAgent agent;
    public bool isReproducing = false;
    private float timeReproducing = 0;
    public GameObject baby;

    // Use this for initialization

    void Start()
    {
        animalHealth = 100;
        hunger = 100;
        reproduce = 100;
        animalAnimator = GetComponent<Animator>();
        temp = this.transform.position;
        GetComponent<CharacterController>().Move(((Vector3.zero + Physics.gravity) * Time.deltaTime));
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        if (animalHealth <= 0)
        {
            Die();
        }
        else if (hunger <= 25)
        {
            SeekFood();

        }
        else if (reproduce < 50)
        {
            SeekCouple();
        }
        else if (!isReproducing)
        {
            MoveToRandomPoint();
        }
    }
    public void Die()
    {
        animalAnimator.SetBool("isWalking", false);
        animalAnimator.SetBool("isStanding", false);
        animalAnimator.SetBool("isRunning", false);
        animalAnimator.SetBool("isAttacking", false);
        animalAnimator.SetBool("isEating", false);
        timeToDestroy += 1 * Time.deltaTime;
        if (timeToDestroy >= .1)
        {
            animalAnimator.SetBool("isDead", true);
            timeToDestroy += 1 * Time.deltaTime;
            if (timeToDestroy >= 60)
            {
                ThingsLists.things.Remove(gameObject);
                GameObject.Destroy(gameObject);
                timeToDestroy = 0;

            }
        }
    }


    public void MoveToRandomPoint()
    {

        if (!isReproducing)
        {
            if (Mathf.Round(temp.x) == Mathf.Round(transform.position.x) && Mathf.Round(temp.z) == Mathf.Round(transform.position.z))
            {
                temp = new Vector3(Random.Range(xLimits.x, xLimits.y), transform.position.y, Random.Range(zLimits.x, zLimits.y));
            }

            timeToMove += 1 * Time.deltaTime;

            //int range = Random.Range(minTimeRange, maxTimeRange);
            if (timeToMove >= 10)
            {
                if (Mathf.Round(transform.position.x) != Mathf.Ceil(temp.x) && Mathf.Round(transform.position.z) != Mathf.Ceil(temp.z))

                {
                    animalAnimator.SetBool("isEating", false);
                    animalAnimator.SetBool("isWalking", true);
                    agent.SetDestination(temp);

                }
                else
                {
                    animalAnimator.SetBool("isWalking", false);
                    temp = transform.position;
                    timeToMove = 0;
                }
            }
            else
            {
                animalAnimator.SetBool("isWalking", false);
            }
        }
        else return;

    }


    private void FixedUpdate()
    {
        GetComponent<CharacterController>().Move(((Vector3.zero + Physics.gravity) * Time.deltaTime));
    }


    public void SeekFood()
    {
        foreach (GameObject curr in ThingsLists.things)
        {
            if (gameObject.tag.Equals("Carnivorous") && curr.tag.Equals("Herbivorous"))
            {
                possibleVictims.Add(curr);
            }
            if (gameObject.tag.Equals("Herbivorous") && curr.tag.Equals("Plant"))
            {
                possibleVictims.Add(curr);
            }
        }
        Atack(possibleVictims[0]);
    }

    public void Atack(GameObject destination)
    {

        Vector3 desti = destination.transform.position;
        if (gameObject.tag != "Herbivorous")
        {
            float distance = Vector3.Distance(transform.position, desti);
            if (distance > 2)
            {
                animalAnimator.SetBool("isAttacking", false);
                animalAnimator.SetBool("isRunning", true);
                agent.SetDestination(desti);
            }
            else
            {
                if (destination.GetComponent<AnimalScript>().animalHealth > 0)
                {
                    animalAnimator.SetBool("isRunning", false);
                    animalAnimator.SetBool("isAttacking", true);
                    destination.GetComponent<AnimalScript>().animalHealth -= 10 * Time.deltaTime;
                }
                else
                {
                    hunger = 100;
                }

            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, desti);
            if (distance > 2)
            {
                agent.SetDestination(desti);
                Debug.Log(agent.destination);
                animalAnimator.SetBool("isEating", false);
                animalAnimator.SetBool("isRunning", true);


            }
            else if (hunger < 25)
            {

                animalAnimator.SetBool("isRunning", false);
                animalAnimator.SetBool("isEating", true);
                destination.GetComponent<PlantsScript>().health -= 2 * Time.deltaTime;
                hunger += 3 * Time.deltaTime;
            }

        }


    }
    public void SeekCouple()
    {
        foreach (GameObject curr in ThingsLists.things)
        {
            if (gameObject.name == curr.name)
            {
                if(curr.transform != gameObject.transform)
                {
                    possibleCouples.Add(curr);
                }
            }
        }
        Reproduce(possibleCouples[0]);
    }

    public void Reproduce(GameObject destination)
    {
        isReproducing = true;

        Vector3 desti = destination.transform.position;
        float distance = Vector3.Distance(transform.position, desti);
        if (distance > 2)
        {
            agent.SetDestination(desti);
            animalAnimator.SetBool("isRunning", true);
        }
        else
        {
            if (timeReproducing < 10)
            {

                destination.GetComponent<AnimalScript>().isReproducing = true;
                destination.GetComponent<AnimalScript>().animalAnimator.SetBool("isRunning", false);
                destination.GetComponent<AnimalScript>().animalAnimator.SetBool("isWalking", false);
                animalAnimator.SetBool("isRunning", false);
                animalAnimator.SetBool("isWalking", false);
            }
            else
            {
                GameObject.Instantiate(baby, transform.position - new Vector3(-2, transform.position.y, -2), transform.rotation);
                destination.GetComponent<AnimalScript>().reproduce = 100;
                destination.GetComponent<AnimalScript>().isReproducing = false;
                reproduce = 100;
                isReproducing = false;
                GetComponent<ObjectInfo>().quantity++;
            }
            timeReproducing += Time.deltaTime;
        }

    }
}

