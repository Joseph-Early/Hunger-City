using UnityEngine;
using UnityEngine.AI;
using FPS_Health = HungerCity.Actor.Components.FPS_Health;

[RequireComponent(typeof(FPS_Health))]
public class FPS_Enemy : MonoBehaviour
{
    internal NavMeshAgent navMeshAgent;

    [SerializeField] private float damageAmount;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackRate;
    [SerializeField] private float attackDistance;

    [HideInInspector] public Transform target;
    [HideInInspector] public bool activated = true;

    // Reference to the player
    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Time.time > attackDelay)
        {
            navMeshAgent.destination = target.position;
            if (Vector3.Distance(target.position, transform.position) <= attackDelay && activated)
            {
                target.GetComponent<PlayerHealth>().health -= damageAmount;
                print(target.GetComponent<PlayerHealth>().health);
                attackDelay = Time.time + attackRate;
            }
        }
        else
        {
            navMeshAgent.destination = transform.position;
        }
    }
}
