using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    public float Damage = 100f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("floor"))
        {
            Destroy(gameObject);
        }
        else if (collider.CompareTag("enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(100f);
            }
            Destroy(gameObject);
        }
    }


void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude > 0.1f)
        {
            // 화살촉 방향을 속도 벡터 방향으로 회전
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
}
