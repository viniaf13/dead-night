using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(AIWaypointNetwork))]
public class AIWaypointNetworkEditor : Editor
{
    private void OnSceneGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        for (int waypointIndex = 0; waypointIndex < network.Waypoints.Count; waypointIndex++)
        {
            if (network.Waypoints[waypointIndex])
            {
                Handles.Label(network.Waypoints[waypointIndex].position, "Waypoint " + waypointIndex);
            }
        }

        if (network.DisplayMode == PathDisplayMode.Connections)
        {
            DrawConnections(network);
        }
        else if (network.DisplayMode == PathDisplayMode.Paths)
        {
            DrawPaths(network);
        }
    }

    private static void DrawPaths(AIWaypointNetwork network)
    {
        NavMeshPath path = new NavMeshPath();
        Vector3 from = network.Waypoints[network.UIFrom].position;
        Vector3 to = network.Waypoints[network.UITo].position;

        NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);

        Handles.color = Color.yellow;
        Handles.DrawPolyLine(path.corners);
    }

    private static void DrawConnections(AIWaypointNetwork network)
    {
        Vector3[] linePoints = new Vector3[network.Waypoints.Count + 1];

        for (int waypointIndex = 0; waypointIndex < network.Waypoints.Count; waypointIndex++)
        {
            int lineIndex = waypointIndex != network.Waypoints.Count ? waypointIndex : 0;
            if (network.Waypoints[waypointIndex])
            {
                linePoints[waypointIndex] = network.Waypoints[lineIndex].position;
            }
            else
            {
                linePoints[waypointIndex] = Vector3.zero;
            }
        }
        linePoints[linePoints.Length - 1] = linePoints[0];
        Handles.color = Color.cyan;
        Handles.DrawAAPolyLine(linePoints);
    }

    //Works but generate delay
    /*public override void OnInspectorGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;
        network.DisplayMode = (PathDisplayMode)EditorGUILayout.EnumPopup("Display Mode", network.DisplayMode);
        network.UIFrom = EditorGUILayout.IntSlider("Waypoint Start", network.UIFrom, 0, network.Waypoints.Count - 1);
        network.UITo = EditorGUILayout.IntSlider("Waypoint End", network.UITo, 0, network.Waypoints.Count - 1);
        base.OnInspectorGUI();
        
    }*/

}
