using UnityEngine;

public class Node2D : IHeapItem2D<Node2D>
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX, gridY;

    public int gCost; 
    public int hCost; 
    public Node2D parent;
    private int heapIndex;

    public int FCost
    {
        get { return gCost + hCost; }
    }

    public Node2D(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node2D nodeToCompare)
    {
        int compare = FCost.CompareTo(nodeToCompare.FCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}