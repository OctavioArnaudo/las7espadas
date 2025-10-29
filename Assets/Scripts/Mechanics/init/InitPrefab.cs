using UnityEngine;
using System;

[Serializable]
public class InitPrefab : InitParticle
{

    [Header("Prefabs")]
    public GameObject ammoPickupPGO;
    public GameObject barrelPGO;
    public GameObject boulderPGO;
    public GameObject breakableWallPGO;
    public GameObject breakEffectPGO;
    public GameObject buttonPGO;
    public GameObject checkpointPGO;
    public GameObject cratePGO;
    public GameObject doorPGO;
    public GameObject elevatorPGO;
    public GameObject flameTrapPGO;
    public GameObject healthPickupPGO;
    public GameObject leverPGO;
    public GameObject meleeEnemyPGO;
    public GameObject movingTempPlatformPGO;
    public GameObject tempPlatformPGO;
    public GameObject movingPlatformPGO;
    public GameObject oneWayPlatformPGO;
    public GameObject platformPathPGO;
    public GameObject playerPGO;
    public GameObject projectilePGO;
    public GameObject rangedEnemyPGO;
    public GameObject sawTrapPGO;
    public GameObject spawnPointPGO;
    public GameObject spikeTrapPGO;
    public GameObject trapPGO;
    public GameObject victoryPGO;
    public GameObject wallPGO;
    public GameObject weaponPickupPGO;
    public GameObject weaponPGO;

    protected override void Awake()
    {
        base.Awake();
        ammoPickupPGO ??= gameObject;
        barrelPGO ??= gameObject;
        boulderPGO ??= gameObject;
        breakableWallPGO ??= gameObject;
        breakEffectPGO ??= gameObject;
        buttonPGO ??= gameObject;
        checkpointPGO ??= gameObject;
        cratePGO ??= gameObject;
        doorPGO ??= gameObject;
        elevatorPGO ??= gameObject;
        flameTrapPGO ??= gameObject;
        healthPickupPGO ??= gameObject;
        leverPGO ??= gameObject;
        meleeEnemyPGO ??= gameObject;
        movingPlatformPGO ??= gameObject;
        oneWayPlatformPGO ??= gameObject;
        platformPathPGO ??= gameObject;
        playerPGO ??= gameObject;
        projectilePGO ??= gameObject;
        rangedEnemyPGO ??= gameObject;
        sawTrapPGO ??= gameObject;
        spawnPointPGO ??= gameObject;
        spikeTrapPGO ??= gameObject;
        trapPGO ??= gameObject;
        victoryPGO ??= gameObject;
        wallPGO ??= gameObject;
        weaponPickupPGO ??= gameObject;
        weaponPGO ??= gameObject;
        AssignDefaults(this, gameObject);
    }
}