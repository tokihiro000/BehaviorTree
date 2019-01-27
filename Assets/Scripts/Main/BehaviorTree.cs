using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree {
    Node rootNode;

    public void Prepare ()
    {
        CreateTestNode();
    }
	
	public void Awake () 
    {
        var nodeQueue = new Queue<INode>();
        nodeQueue.Enqueue(rootNode);
        while(nodeQueue.Count != 0)
        {
            INode node = nodeQueue.Dequeue();
            node.Activate();
            if (node.HasChild())
            {
                nodeQueue.Enqueue(node.GetChildNode());
            }
            //foreach (var childNode in node.GetChildNodeList()) {
            //    if (childNode == null) {
            //        continue;
            //    }

            //    nodeQueue.Enqueue(childNode);
            //}
        }
	}

    public IEnumerator Update() 
    {
        int count = 0;
        Node currentNode = rootNode;
        while (currentNode != null) {
            var nodeState = currentNode.GetNodeState();
            if (count > 1000) {
                Debug.Log("無限ループ??");
                yield break;
            }
            count += 1;
            //Debug.Log("Id: " + currentNode.Id + " state:" + nodeState);

            switch (nodeState) {
                case NodeState.Init:
                    currentNode.StartProcess();
                    break;
                case NodeState.Start:
                    currentNode.Run();
                    break;
                case NodeState.Running:
                    currentNode.DoNext();
                    break;
                case NodeState.WaitForChild:
                    currentNode = currentNode.GetChildNode();
                    break;
                case NodeState.Complete:
                    if (currentNode.IsRoot()) {
                        currentNode = null;
                        break;
                    }

                    currentNode = currentNode.GetOwner();
                    break;
                case NodeState.Disable:
                    Debug.Log("予期せぬエラー");
                    yield break;
                default:
                    break;            
            }

            yield return null;
        }

        ExecuteResult result = rootNode.GetExecuteResultState();
        var state = result.GetExecuteResult();
        if (state == ExecuteResultState.Success) {
            Debug.Log("成功");
        } else if (state == ExecuteResultState.Failure) {
            Debug.Log("失敗");
        } else {
            Debug.Log("謎の失敗？");
        }
    }

    private void CreateTestNode()
    {
        NodeFactory factory = new NodeFactory();
        rootNode = factory.CreateNode(NodeType.Root);
        var actionNode = (ActionNode)factory.CreateNode(NodeType.Action);
        var sampleAction = new SampleAction();
        actionNode.Action = sampleAction;
        rootNode.AddNode(actionNode);
    }
}
