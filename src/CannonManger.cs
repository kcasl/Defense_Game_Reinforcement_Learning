using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManger : MonoBehaviour
{
    public cannon_shot cannon1;
    public cannon_shot cannon2;
    public cannon_shot cannon3;
    public cannon_shot cannon4;
    public cannon_shot cannon5;
    public cannon_shot cannon6;

    public void shotcannon()
    {
        cannon1.Shoot();
        cannon2.Shoot();
        cannon3.Shoot();
        cannon4.Shoot();
        cannon5.Shoot();
        cannon6.Shoot();
    }

    public void setpower(float power)
    {
        cannon1.setShootPower(power + 800);
        cannon2.setShootPower(power + 800);
        cannon3.setShootPower(power + 800);
        cannon4.setShootPower(power + 800);
        cannon5.setShootPower(power + 800);
        cannon6.setShootPower(power + 800);
    }

    public void Resetting()
    {
        cannon1.Resetting();
        cannon2.Resetting();
        cannon3.Resetting();
        cannon4.Resetting();
        cannon5.Resetting();
        cannon6.Resetting();
    }
}
