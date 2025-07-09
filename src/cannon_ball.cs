using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    public float splashRadius = 5f;       
    public float splashDamage = 50f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("floor") || collider.CompareTag("enemy"))
        {
            if (explosionEffectPrefab != null)
            {
                GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 1.5f);
            }

            // 스플래시 데미지 계산
            ApplySplashDamage();

            Destroy(gameObject);
        }
    }

    void ApplySplashDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, splashRadius);

        foreach (Collider c in colliders)
        {
            if (c.CompareTag("enemy"))
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(splashDamage);
                }
            }
        }
    }
}
