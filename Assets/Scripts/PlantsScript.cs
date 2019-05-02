using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsScript : MonoBehaviour
{
    [SerializeField] public float health = 100;
    public GameObject deadModel;
    private Transform plantTransform;
    private float reproduce = 0;
    public GameObject newPlant;

    // Use this for initialization
    void Start()
    {
        plantTransform = transform;
        health = 100;
        reproduce = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
        else if (reproduce > 100)
        {
            Reproduce();
        }
        reproduce += Time.deltaTime;
    }

    public void Die()
    {

        GameObject.Destroy(gameObject);
        GameObject.Instantiate(deadModel, plantTransform.position, transform.rotation);

    }
    public void Reproduce()
    {
        Vector3 seed;
        seed.x = Random.Range(plantTransform.position.x - 20, transform.position.x + 20);
        seed.z = Random.Range(plantTransform.position.z - 20, transform.position.z + 20);
        seed.y = plantTransform.position.y;
        GameObject.Instantiate(newPlant, seed, plantTransform.rotation);
        reproduce = 0;
    }
}

