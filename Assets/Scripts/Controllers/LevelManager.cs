using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private ObjectStats objectStats;

    private void Start()
    {
        objectStats = GetComponent<ObjectStats>();
    }

    private void Update()
    {
        //level up after unlocking teleportationPoint
    }

    public void LevelUp()
    {
        objectStats.BaseStatsIncrementation(ConstantsValues.LevelIncrement.inceremnt);
    }
    //increases stats when is discovred Teleportation Point (future release)
}
