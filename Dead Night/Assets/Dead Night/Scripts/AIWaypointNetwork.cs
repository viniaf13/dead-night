using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathDisplayMode { None,Connections,Paths}

public class AIWaypointNetwork : MonoBehaviour
{
    public PathDisplayMode DisplayMode = PathDisplayMode.Connections;
    [Range(0, 10)] public int UIFrom = 0;
    [Range(0, 10)] public int UITo = 0;
    public List<Transform> Waypoints = new List<Transform>();

}
