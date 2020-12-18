using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRecord
{
	public Node node;
	public NodeRecord connection;
	public double costSoFar;
	public double estimatedTotalCost;

	public NodeRecord(Node node, NodeRecord connection, double costSoFar, double estimatedTotalCost)
	{
		this.node = node;
		this.connection = connection;
		this.costSoFar = costSoFar;
		this.estimatedTotalCost = estimatedTotalCost;
	}
}
