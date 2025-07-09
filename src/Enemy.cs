using Unity.MLAgents;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform targetPoint;
    public Animator animator;
    public float HP = 100f;
    private NavMeshAgent agent;
    private Rigidbody rb;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator.applyRootMotion = false;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Start()
    {
        agent.SetDestination(targetPoint.position);
    }
    public void TakeDamage(float amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        agent.isStopped = true;
        animator.SetBool("Dead", true);
        Destroy(gameObject, 4f);
    }

    public void StartAttackingDoor()
    {
        Debug.Log("door");
        animator.SetBool("Attack", true);
    }

    public void Resetting()
    {
        HP = 100f;
    }
}
