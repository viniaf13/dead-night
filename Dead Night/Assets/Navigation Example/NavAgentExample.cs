using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentExample : MonoBehaviour
{
    [SerializeField] AIWaypointNetwork waypointNetwork = default;
    [SerializeField] int currentWaypointIndex = 0; //TODO : make it private
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetNextDestination();
    }

    private void SetNextDestination()
    {
        if (!waypointNetwork) return;

        //Resets waypoint list
        if (currentWaypointIndex >= waypointNetwork.Waypoints.Count)
        {
            currentWaypointIndex = 0;
        }

        if (waypointNetwork.Waypoints[currentWaypointIndex])
        {
            _navMeshAgent.destination = waypointNetwork.Waypoints[currentWaypointIndex].position;
        }
    }

    private void Update()
    {
        //_navMeshAgent.HasPath == false bugged when jumping. Better when not using offmesh links

        //If it is not currently on a path and has not asked for another, set next destination
        if ((_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) 
             && !_navMeshAgent.pathPending)
        {
            currentWaypointIndex++;
            SetNextDestination();
        }
        // If current path if no longer valid or optimal, set new path
        else if (_navMeshAgent.isPathStale)
        {
            SetNextDestination();
        }
    }
}
