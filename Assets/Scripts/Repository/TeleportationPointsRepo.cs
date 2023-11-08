using Assets.Scripts.Interactables;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Repository
{
    public class TeleportationPointsRepo : MonoBehaviour
    {
        public static TeleportationPointsRepo instance;

        public List<TeleportationPoint> TeleportationPoints = new List<TeleportationPoint>();

        private void Awake()
        {
            instance = this;          
        }

        public void Add(TeleportationPoint point)
        {
            TeleportationPoints.Add(point);
        }
    }
}
