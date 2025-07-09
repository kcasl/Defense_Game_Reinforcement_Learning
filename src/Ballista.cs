using UnityEngine;
using System.Collections;


public class Ballista : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float ammo = 20f;
    public float launchForce =35f;

    public bool canFire = true;

    public void Fire(float force)
    {
        if (ammo > 0)
        {
            if (!canFire) return;

            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            GameObject arrow2 = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb2 = arrow2.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * force;
            rb2.velocity = firePoint.forward * (force - 1f);
            StartCoroutine(FireCooldownRoutine());
            ammo -= 1;
        }
    }

    IEnumerator FireCooldownRoutine()
    {
        canFire = false;
        yield return new WaitForSeconds(2f);
        canFire = true;
    }

/*    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire(25f);
        }
    }
*/
/*    public void BallistaAiming(float angle, float force)
    {
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);
        launchForce = force;
    }*/

    public void Resetting()
    {
        canFire = true;
        launchForce = 35f;
        ammo = 20f;
    }

}
