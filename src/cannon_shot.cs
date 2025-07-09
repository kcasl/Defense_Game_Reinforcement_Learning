using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon_shot : MonoBehaviour
{
    public Transform shootPos;
    public GameObject sphere;
    public float ammo = 10f;
    public float shootPower;
    public bool canFire = true;

    public void Shoot()
    {
        if (ammo > 0)
        {
            if (!canFire) return;

            Instantiate(sphere, shootPos.position, shootPos.rotation).GetComponent<Rigidbody>().AddRelativeForce(shootPos.forward * shootPower);
            StartCoroutine(FireCooldownRoutine());
            ammo -= 1;
        }
    }
    IEnumerator FireCooldownRoutine()
    {
        canFire = false;
        yield return new WaitForSeconds(5.0f);
        canFire = true;
    }
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }*/

    public void setShootPower(float power)
    {
        shootPower = power;
    }

    public void Resetting()
    {
        ammo = 10f;
        shootPower = 1800f;
        canFire = true;
    }


}
