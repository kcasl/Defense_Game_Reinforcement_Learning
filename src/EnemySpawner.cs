using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // 적 프리팹
    public Transform spawnCenter;      // 중심 스폰 지점
    public Transform targetPoint;      // 적들이 이동할 목표 지점
    public int enemyCount = 15;        // 생성할 적 수
    public float spawnRadius = 5f;     // 스폰 위치 반경

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomPos = GetRandomPositionAround(spawnCenter.position, spawnRadius);

            GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.targetPoint = targetPoint;
            }
        }
    }

    // 주어진 중심 좌표 주변에서 랜덤 위치 하나 생성
    Vector3 GetRandomPositionAround(Vector3 center, float radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, radius);

        float xOffset = Mathf.Cos(angle) * distance;
        float zOffset = Mathf.Sin(angle) * distance;

        return new Vector3(center.x + xOffset, center.y, center.z + zOffset);
    }
}
