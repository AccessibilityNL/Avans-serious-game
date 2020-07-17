
using System;
using UnityEngine;

namespace Assets.Scripts.Overworld
{
    [Serializable]
    public class DestinationWithPath
    {
        public Intersection destination;
        public Transform path;
        public bool reverseDirection;
    }
}
