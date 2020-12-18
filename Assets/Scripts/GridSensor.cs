using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSensor : MonoBehaviour
{
    public static Node[,] collisions;
    private int cols = 29; //29
    private int rows = 30;
    private int startX = -13;
    private int startY = -21;
    public static List<Node> path;
    public GameObject prefab;

    private void Start()
    {
        CreateAStarPath();
    }

    void CreateAStarPath()
    {
        GetGridCollisions();
        path = Pathfind();
        
        if (path != null)
        {
            foreach (Node n in path)
            {
                Instantiate(prefab, new Vector3(n.position.x, n.position.y, 0.5f), Quaternion.identity);
                Debug.DrawRay(n.position, new Vector3(0, 0, -1) * 20, Color.yellow, 50000);
            }
        }
    }

    void GetGridCollisions()
    {
        collisions = new Node[rows, cols];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                float x = startX + 0.5f + col;
                float y = startY + 0.5f + row;
                Vector3 startPos = new Vector3(x, y, 20f);

                int layerMask = 1 << 8;
                layerMask = ~layerMask;
                ContactFilter2D filter = new ContactFilter2D();
                filter.layerMask = layerMask;
                List<Collider2D> hits = new List<Collider2D>();

                Vector3 rayDir = new Vector3(0, 0, -1);
                if (Physics2D.OverlapPoint(startPos, filter, hits) > 0)
                {
                    //Debug.Log("Obstacles at row: " + row + " col: " + col);
                    //Debug.DrawRay(startPos, rayDir * 20, Color.red, 50000);
                    collisions[row, col] = new Node(true, new Vector2(x, y));
                }
                else
                {
                    //Debug.DrawRay(startPos, rayDir * 20, Color.green, 50000);
                    collisions[row, col] = new Node(false, new Vector2(x, y));
                }
            }
        }
    }

    public static Node getNodeAtPoint(Vector2 position) 
    {
        int xInt = Mathf.FloorToInt(position.x) + 13; //start X is -13
        int yInt = Mathf.FloorToInt(position.y) + 21; //start Y is -21 
        Node node;
        try
        {
            node = collisions[yInt, xInt];
        }
        catch {
            node = null;
        }
        return node;
    }
    List<Node> Pathfind()
    {
        Vector2 start = GameObject.FindGameObjectWithTag("Enemy").transform.position;
        Vector2 end = GameObject.FindGameObjectWithTag("Player").transform.position;

        Node startNode = getNodeAtPoint(start);
        Node endNode = getNodeAtPoint(end);
        NodeRecord startRecord = new NodeRecord(startNode, null, 0, getDistance(startNode, endNode));

        //open list is available nodes to visit, closed list is previously visited nodes
        List<NodeRecord> open = new List<NodeRecord>();
        open.Add(startRecord); //add initial
        List<NodeRecord> closed = new List<NodeRecord>();

        NodeRecord current = null;
        while (open.Count > 0)
        {
            NodeRecord min;
            //find smallest element in open list
            if (open[0] != null)
            {
                min = open[0];
            }
            else
            {
                return null;
            }
            foreach (NodeRecord n in open)
            {
                if (n.estimatedTotalCost < min.estimatedTotalCost)
                {
                    min = n;
                }
            }
            current = min;
            closed.Add(current);
            open.Remove(current);
            //if this is the goal position, terminate
            if (current.node.position == endNode.position)
            {
                break;
            }


            //get surrounding nodes
            List<NodeRecord> neighbors = getNeighbors(current, endNode);

            foreach (NodeRecord n in neighbors)
            {
                bool isInClosedList = false;
                foreach (NodeRecord c in closed)
                {
                    if (n.node.position == c.node.position)
                    {
                        isInClosedList = true;
                    }
                }

                //only do these operations if node is not in closed list
                if (!isInClosedList)
                {
                    bool isInOpenList = false;
                    foreach (NodeRecord o in open)
                    {
                        if (n.node.position == o.node.position)
                        {
                            isInOpenList = true;
                        }
                    }
                    //add node to open list if not already there
                    if (!isInOpenList)
                    {
                        open.Add(n);
                    }
                }
            }
        }

        List<Node> path = new List<Node>();
        //get path and reverse
        while (current.node.position != startNode.position)
        {
            path.Add(current.node);
            current = current.connection;
        }
        path.Reverse();
        return path;
    }

    double getDistance(Node n1, Node n2)
    {
        double x1 = n1.position.x;
        double y1 = n1.position.y;
        double x2 = n2.position.x;
        double y2 = n2.position.y;

        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

        return distance;
    }

    List<NodeRecord> getNeighbors(NodeRecord nodeRecord, Node endNode)
    {
        Vector2 root = nodeRecord.node.position;
        List<Node> neighbors = new List<Node>();

        Node top = getNodeAtPoint(root + new Vector2(0, 1));
        Node topRight = getNodeAtPoint(root + new Vector2(1, 1));
        Node right = getNodeAtPoint(root + new Vector2(1, 0));
        Node botRight = getNodeAtPoint(root + new Vector2(1, -1));
        Node bot = getNodeAtPoint(root + new Vector2(0, -1));
        Node botLeft = getNodeAtPoint(root + new Vector2(-1, -1));
        Node left = getNodeAtPoint(root + new Vector2(-1, 0));
        Node topLeft = getNodeAtPoint(root + new Vector2(-1, 1));

        neighbors.Add(top);
        neighbors.Add(topRight);
        neighbors.Add(right);
        neighbors.Add(botRight);
        neighbors.Add(bot);
        neighbors.Add(botLeft);
        neighbors.Add(left);
        neighbors.Add(topLeft);

        List<NodeRecord> validNeighbors = new List<NodeRecord>();
        foreach (Node n in neighbors)
        {
            if (n != null && !n.obstacle)
            {
                Double costSoFar = nodeRecord.costSoFar + 1;
                Double estimatedTotalCost = getDistance(n, endNode) + costSoFar;
                NodeRecord nr = new NodeRecord(n, nodeRecord, costSoFar, estimatedTotalCost);
                validNeighbors.Add(nr);
            }
        }
        return validNeighbors;
    }
}
