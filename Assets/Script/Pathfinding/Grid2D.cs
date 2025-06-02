using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Pathfinding2D))]
public class Grid2D : MonoBehaviour
{
    [SerializeField] private bool displayGridGizmos = false;
    [SerializeField] private bool onlyDisplayUnwalkableGizmos = false;
    [SerializeField] private LayerMask unwalkableLayerMask;
    [SerializeField] private Vector2 gridWorldSize = new Vector2(10.0f, 10.0f);
    [SerializeField] private float nodeRadius = 0.25f;
    [SerializeField][Tooltip("Controls how forgiving Unwalkable Node detection is.")][Range(0.1f, 10.0f)] private float obstacleDetectionScale = 1.0f;
    [SerializeField] private bool recalculateWalkableNodes = false;
    [SerializeField] private bool precalculateNeighbors = true;

    private Node2D[,] grid;
    private float nodeDiameter;
    private int gridCellCountX, gridCellCountY;
    public int MaxSize { get { return gridCellCountX * gridCellCountY; } }


    public float NodeRadius { get { return nodeRadius; } }

    private Dictionary<Node2D, List<Node2D>> nodeNeighborDictionary;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridCellCountX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridCellCountY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
        if (precalculateNeighbors)
        {
            PrecalculateNeighbors();
        }
    }

    void Update()
    {
        if (recalculateWalkableNodes)
        {
            RecalculateWalkableNodes();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null && displayGridGizmos)
        {
            foreach (Node2D node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                if (onlyDisplayUnwalkableGizmos)
                {
                    if (!node.walkable)
                    {
                        Gizmos.DrawWireCube(new Vector3(node.worldPosition.x, node.worldPosition.y, 0), Vector3.one * (nodeDiameter - 0.1f));
                    }
                }
                else
                {
                    Gizmos.DrawWireCube(new Vector3(node.worldPosition.x, node.worldPosition.y, 0), Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
    }

    private void CreateGrid()
    {
        grid = new Node2D[gridCellCountX, gridCellCountY];
        Vector2 worldBottomLeft = new Vector2(transform.position.x - gridWorldSize.x / 2, transform.position.y - gridWorldSize.y / 2);

        for (int x = 0; x < gridCellCountX; x++)
        {
            for (int y = 0; y < gridCellCountY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + new Vector2(x * nodeDiameter + nodeRadius, y * nodeDiameter + nodeRadius);
                bool walkable = !Physics2D.CircleCast(worldPoint, nodeRadius * obstacleDetectionScale, Vector2.zero, 0, unwalkableLayerMask);
                grid[x, y] = new Node2D(walkable, worldPoint, x, y);
            }
        }
    }

    public void RecalculateWalkableNodes()
    {
        Vector2 worldBottomLeft = new Vector2(transform.position.x - gridWorldSize.x / 2, transform.position.y - gridWorldSize.y / 2);

        for (int x = 0; x < gridCellCountX; x++)
        {
            for (int y = 0; y < gridCellCountY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + new Vector2(x * nodeDiameter + nodeRadius, y * nodeDiameter + nodeRadius);
                bool walkable = !Physics2D.CircleCast(worldPoint, nodeRadius * obstacleDetectionScale, Vector2.zero, 0, unwalkableLayerMask);
                grid[x, y].walkable = walkable;
            }
        }
    }

    public Node2D NodeFromWorldPoint(Vector2 worldPosition)
    {
        Vector2 worldPoint = worldPosition - new Vector2(transform.position.x, transform.position.y);
        float percentX = Mathf.Clamp01(worldPoint.x / gridWorldSize.x + 0.5f);
        float percentY = Mathf.Clamp01(worldPoint.y / gridWorldSize.y + 0.5f);

        int x = Mathf.RoundToInt((gridCellCountX - 1) * percentX);
        int y = Mathf.RoundToInt((gridCellCountY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node2D> GetNeighbors(Node2D node)
    {
        if (!precalculateNeighbors)
        {
            return GetNeighborsRealtime(node);
        }
        else
        {
            return GetNeighborsPrecalculated(node);
        }
    }

    public List<Node2D> GetNeighborsRealtime(Node2D node)
    {
        List<Node2D> neighbors = new List<Node2D>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridCellCountX && checkY >= 0 && checkY < gridCellCountY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    public List<Node2D> GetNeighborsPrecalculated(Node2D node)
    {
        return nodeNeighborDictionary[node];
    }

    private void PrecalculateNeighbors()
    {
        nodeNeighborDictionary = new Dictionary<Node2D, List<Node2D>>();

        for (int x = 0; x < gridCellCountX; x++)
        {
            for (int y = 0; y < gridCellCountY; y++)
            {
                Node2D currentNode = grid[x, y];
                List<Node2D> neighbors = new List<Node2D>();

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;

                        int neighborX = currentNode.gridX + dx;
                        int neighborY = currentNode.gridY + dy;

                        if (neighborX >= 0 && neighborX < gridCellCountX && neighborY >= 0 && neighborY < gridCellCountY)
                        {
                            neighbors.Add(grid[neighborX, neighborY]);
                        }
                    }
                }

                nodeNeighborDictionary[currentNode] = neighbors;
            }
        }
    }
}
