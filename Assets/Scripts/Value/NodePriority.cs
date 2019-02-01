using System;

using PriorityValue = System.Int64;
public class NodePriority {
    private INode node;
    private PriorityValue priority;

    NodePriority(INode node, PriorityValue priority) {
        this.node = node;
        this.priority = priority;
    }
}