using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject explosionEffectPrefab;

    public List<GameObject> enemiesInRange = new List<GameObject>();
    private void Start()
    {
        enemiesInRange = new List<GameObject>();
        GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 10f);
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") && !enemiesInRange.Contains(other.gameObject))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    public float blockTarget()
    {
        return enemiesInRange.Count;
    }

    public void Resetting()
    {
        Destroy(gameObject);
        enemiesInRange = new List<GameObject>();
    }
}
