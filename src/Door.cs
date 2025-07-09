using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public float DoorHP = 1000f;
    public float damagePerEnemy = 10f;
    public DefenseAgent Agent;
    public GameObject explosionEffectPrefab;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private bool isTakingDamage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") && !enemiesInRange.Contains(other.gameObject))
        {
            enemiesInRange.Add(other.gameObject);

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.StartAttackingDoor();
            }

            if (!isTakingDamage)
            {
                StartCoroutine(DamageLoop());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            enemiesInRange.Remove(other.gameObject);

            if (enemiesInRange.Count == 0)
            {
                isTakingDamage = false;
            }
        }
    }

    IEnumerator DamageLoop()
    {
        isTakingDamage = true;

        while (enemiesInRange.Count > 0 && DoorHP > 0 && isTakingDamage)
        {
            float totalDamage = enemiesInRange.Count * damagePerEnemy;
            DoorHP -= totalDamage;
            DoorHP = Mathf.Max(DoorHP, 0);
            Debug.Log(DoorHP);
            yield return new WaitForSeconds(2f);
        }

        isTakingDamage = false;
    }

    void FixedUpdate()
    {

        if (DoorHP <= 0)
        {
            if (explosionEffectPrefab != null)
            {
                GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 1.5f);
            }
            Agent.DoorDestroyed();
        }
    }

    public void Resetting()
    {
        isTakingDamage = false;
        DoorHP = 1000f;
        enemiesInRange.Clear();
    }
}
