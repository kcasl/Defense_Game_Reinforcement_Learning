using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // �� ������
    public Transform spawnCenter;      // �߽� ���� ����
    public Transform targetPoint;      // ������ �̵��� ��ǥ ����
    public int enemyCount = 15;        // ������ �� ��
    public float spawnRadius = 5f;     // ���� ��ġ �ݰ�

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

    // �־��� �߽� ��ǥ �ֺ����� ���� ��ġ �ϳ� ����
    Vector3 GetRandomPositionAround(Vector3 center, float radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, radius);

        float xOffset = Mathf.Cos(angle) * distance;
        float zOffset = Mathf.Sin(angle) * distance;

        return new Vector3(center.x + xOffset, center.y, center.z + zOffset);
    }
}
