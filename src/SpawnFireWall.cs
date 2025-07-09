using Unity.Collections;
using UnityEngine;

public class SpawnFireWall : MonoBehaviour
{
    public GameObject wallPrefab;     
    public Vector3 spawnPosition; 
    public float ammo = 1f;
    public Quaternion spawnRotation = Quaternion.identity;

    public void SpawnWall()
    {
        if (ammo > 0f) {
            if (wallPrefab != null)
            {
                Instantiate(wallPrefab, spawnPosition, spawnRotation);
                ammo -= 1f;
            }
        }
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnWall();
        }
    }
*/
    public void Resetting()
    {
        ammo = 1f;
    }

}
