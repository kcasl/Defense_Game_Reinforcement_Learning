using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class DefenseAgent : Agent
{
    public CannonManger cannonmanger;
    public EnemySpawner spawner1;
    public EnemySpawner spawner2;
    public Ballista ballista1;
    public Ballista ballista2;
    public Ballista ballista3;
    public Ballista ballista4;
    public SpawnFireWall spfw;
    public cannon_shot cannon;
    public Wall wall;
    public Door door;

    private float killcount = 0;
    private float lastkillcount = 30f;

    public void DoorDestroyed()
    {
        EndEpisode();
        AddReward(-20f);
    }

    private void FixedUpdate()
    {
        if (wall.blockTarget() > 0)
        {
            AddReward(wall.blockTarget() / 15);
        }

        GameObject[] enem = GameObject.FindGameObjectsWithTag("enemy");

        if (enem.Length == 0)
        {
            AddReward(10f);
            EndEpisode();
        }

        if (enem.Length > 0) 
        {
            killcount = enem.Length;

            killReward(lastkillcount - killcount);
        }
        lastkillcount = killcount;
        AddReward(-0.01f);
    }

    public void killReward(float killed)
    {
        AddReward(killed);
    }

    public override void OnEpisodeBegin()
    {
        GameObject[] enem = GameObject.FindGameObjectsWithTag("enemy");

        foreach (GameObject enemy in enem)
        {
            Destroy(enemy);
        }

        GameObject[] wall = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject w in wall)
        {
            Destroy(w);
        }
        lastkillcount = 30f;
        killcount = 0;
        cannonmanger.Resetting();
        ballista1.Resetting();
        ballista2.Resetting();
        ballista3.Resetting();
        ballista4.Resetting();
        door.Resetting();
        spfw.Resetting();
        spawner1.SpawnEnemies();
        spawner2.SpawnEnemies();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Cannon
        sensor.AddObservation(cannon.ammo / 10f);
        sensor.AddObservation(cannon.shootPower / 2000f);
        sensor.AddObservation(cannon.canFire ? 1f : 0f);

        // Ballista1
        sensor.AddObservation(ballista1.ammo / 20f);
        sensor.AddObservation(ballista1.launchForce / 35f);
        sensor.AddObservation(ballista1.canFire ? 1f : 0f);

        // Ballista2
        sensor.AddObservation(ballista2.ammo / 20f);
        sensor.AddObservation(ballista2.launchForce / 35f);
        sensor.AddObservation(ballista2.canFire ? 1f : 0f);

        sensor.AddObservation(ballista3.ammo / 20f);
        sensor.AddObservation(ballista3.launchForce / 35f);
        sensor.AddObservation(ballista3.canFire ? 1f : 0f);

        sensor.AddObservation(ballista4.ammo / 20f);
        sensor.AddObservation(ballista4.launchForce / 35f);
        sensor.AddObservation(ballista4.canFire ? 1f : 0f);

        // Firewall + Door
        sensor.AddObservation(spfw.ammo / 1f);
        sensor.AddObservation(door.DoorHP / 1000f);

        // Enemy count
        sensor.AddObservation(GameObject.FindGameObjectsWithTag("enemy").Length / 30f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Continuous Action
        float cannonPower = Mathf.Clamp(actions.ContinuousActions[0], 0f, 1f);
        float ballistaPower = Mathf.Clamp(actions.ContinuousActions[1], 0f, 1f);
/*        float ballista1Angle = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        float ballista1Power = Mathf.Clamp(actions.ContinuousActions[2], 0f, 1f); // 파워는 양수
        float ballista2Angle = Mathf.Clamp(actions.ContinuousActions[3], -1f, 1f);
        float ballista2Power = Mathf.Clamp(actions.ContinuousActions[4], 0f, 1f);*/

        // 조작
        cannonmanger.setpower(cannonPower * 2000f);
        /*        ballista1.BallistaAiming(ballista1Angle * 60f, ballista1Power * 100f);
                ballista2.BallistaAiming(ballista2Angle * 60f, ballista2Power * 100f);*/
        ballistaPower = ballistaPower * 35f;


        // Discrete Action
        int action = actions.DiscreteActions[0];

        switch (action)
        {
            case 0:
                break;

            case 1:
                spfw.SpawnWall();
                break;

            case 2:
                if (!cannon.canFire || cannon.ammo <= 0)
                    AddReward(-0.2f);
                else
                    cannonmanger.shotcannon();
                break;

            case 3:
                if (!ballista1.canFire || ballista1.ammo <= 0 || !ballista2.canFire || ballista2.ammo <= 0)
                    AddReward(-0.2f);
                else
                    ballista1.Fire(ballistaPower);
                    ballista2.Fire(ballistaPower);
                    AddReward(0.1f);
                break;

            case 4:
                if (!ballista3.canFire || ballista3.ammo <= 0 || !ballista4.canFire || ballista4.ammo <= 0)
                    AddReward(-0.2f);
                else
                    ballista3.Fire(ballistaPower);
                    ballista4.Fire(ballistaPower);
                    AddReward(0.1f);
                break;
        }
    }

}
