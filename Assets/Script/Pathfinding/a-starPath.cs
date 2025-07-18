using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding2D : MonoBehaviour
{
    [SerializeField] private bool _printTimeForPath = false;
    private Grid2D _grid;
    private Heap2D<Node2D> _openSet;
    private HashSet<Node2D> _closedSet;

    public float NodeRadius { get { return _grid.NodeRadius; } }

    void Awake()
    {
        _grid = GetComponent<Grid2D>();
    }

    void Start()
    {
        LazyInitializeOpenSet();
        LazyInitializeClosedSet();
    }

    private void LazyInitializeOpenSet()
    {
        _openSet = new Heap2D<Node2D>(_grid.MaxSize);
    }

    private void LazyInitializeClosedSet()
    {
        _closedSet = new HashSet<Node2D>();
    }

    public Vector3[] FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch stopwatch = new Stopwatch();
        if (_printTimeForPath)
        {
            stopwatch.Start();
        }

        Node2D startNode = _grid.NodeFromWorldPoint(startPos);
        Node2D targetNode = _grid.NodeFromWorldPoint(targetPos);

        if ( targetNode.walkable) 
        {
            _openSet.Clear(); 
            _closedSet.Clear();
            _openSet.Add(startNode);

            while (_openSet.Count > 0)
            {
                
                Node2D currentNode = _openSet.RemoveFirst();
                _closedSet.Add(currentNode);

                
                if (currentNode == targetNode)
                {
                    if (_printTimeForPath)
                    {
                        stopwatch.Stop();
                        print("Time for found path: " + stopwatch.ElapsedMilliseconds + " ms");
                    }
                    return RetracePath(startNode, targetNode);
                }

                foreach (Node2D neighbor in _grid.GetNeighbors(currentNode))
                {
                   
                    if (!neighbor.walkable || _closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.gCost || !_openSet.Contains(neighbor))
                    {
                       
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;
                        
                        if (!_openSet.Contains(neighbor))
                        {
                            _openSet.Add(neighbor);
                        }
                        else 
                        {
                            _openSet.UpdateItem(neighbor);
                        }
                    }
                }
            }
        }
        return new Vector3[0];
    }

    private int GetDistance(Node2D nodeA, Node2D nodeB)
    {
       
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

       
        if (distX < distY)
        {
            return (distX * 14) + ((distY - distX) * 10);
        }
       
        else
        {
            return (distY * 14) + ((distX - distY) * 10);

        }
    }

    private Vector3[] RetracePath(Node2D startNode, Node2D endNode)
    {
        List<Node2D> path = new List<Node2D>();
        Node2D currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Add(startNode);
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    
    private Vector3[] SimplifyPath(List<Node2D> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i - 1].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }
}