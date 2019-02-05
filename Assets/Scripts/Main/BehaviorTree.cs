using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree {
    INode rootNode;

    public void Prepare (INode root)
    {
        //CreateTestNode7();
        rootNode = root;
    }
	
	public void Awake () 
    {
        rootNode.Activate();
	}

    public IEnumerator Update() 
    {
        int loogCount = 0;
        while (loogCount < 100)
        {
            loogCount += 1;
            int count = 0;
            rootNode.Activate();
            INode currentNode = rootNode;
            while (currentNode != null)
            {
                var nodeState = currentNode.GetNodeState();
                if (count > 10000)
                {
                    Debug.Log("無限ループ??");
                    yield break;
                }
                count += 1;
                //Debug.Log("Id: " + currentNode.Id + " state:" + nodeState);

                switch (nodeState)
                {
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
                        if (currentNode.IsRoot())
                        {
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
            if (state == ExecuteResultState.Success)
            {
                Debug.Log("成功");
            }
            else if (state == ExecuteResultState.Failure)
            {
                Debug.Log("失敗");
            }
            else if (state == ExecuteResultState.Error)
            {
                Debug.Log("エラー");
            }
            else
            {
                Debug.Log("謎の失敗？");
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
