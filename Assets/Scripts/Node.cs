using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool obstacle;
    public Vector2 position;

    public Node(bool obstacle, Vector2 position)
    {
        this.obstacle = obstacle;
        this.position = position;
    }
}
