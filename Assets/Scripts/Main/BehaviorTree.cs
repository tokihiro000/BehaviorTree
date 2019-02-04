using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree {
    Node rootNode;

    public void Prepare ()
    {
        CreateTestNode5();
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
            else
            {
                Debug.Log("謎の失敗？");
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    private void CreateTestNode1()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        rootNode = factory.Create(NodeType.Root);
        //var actionNode = (ActionNode)factory.Create(NodeType.Action);
        var actionNode = CreateSampleActionNode1();
        //actionNode.Action = sampleAction;
        rootNode.AddNode(actionNode);
    }

    // sequencer
    private void CreateTestNode2()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        rootNode = factory.Create(NodeType.Root);

        var sequencerNode = (SequencerNode)factory.Create(NodeType.Sequencer);
        var actionNode1 = CreateSampleActionNode1();
        var actionNode2 = CreateSampleActionNode1();
        var actionNode3 = CreateSampleActionNode1();
        sequencerNode.AddNode(actionNode1);
        sequencerNode.AddNode(actionNode2);
        sequencerNode.AddNode(actionNode3);
        rootNode.AddNode(sequencerNode);
    }

    // selector
    private void CreateTestNode3()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        rootNode = factory.Create(NodeType.Root);
        var selectorNode = (SelectorNode)factory.Create(NodeType.Selector);
        var actionNode1 = CreateSampleActionNode1();
        var actionNode2 = CreateSampleActionNode2();
        selectorNode.AddNode(actionNode1, 1);
        selectorNode.AddNode(actionNode2, 100);
        rootNode.AddNode(selectorNode);
    }

    // Decorate 失敗 
    private void CreateTestNode4()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        rootNode = factory.Create(NodeType.Root);

        var selectorNode = (SelectorNode)factory.Create(NodeType.Selector);
        var actionNode1 = CreateSampleActionNode1();
        selectorNode.AddNode(actionNode1, 1);
        var decoratorNode = (DecoratorNode)factory.Create(NodeType.Decorator);
        var decoratorFactory = FactoryManager.Instance.GetFactory<Decorator, DecoratorType>(FactoryType.Decorator);
        var decorator = decoratorFactory.Create(DecoratorType.False);
        decoratorNode.SetDecoratable(decorator);
        var actionNode2 = CreateSampleActionNode2();
        decoratorNode.AddNode(actionNode2);
        selectorNode.AddNode(decoratorNode, 100);
        rootNode.AddNode(selectorNode);
    }

    private void CreateTestNode5()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        rootNode = factory.Create(NodeType.Root);

        var sequencerNode = (SequencerNode)factory.Create(NodeType.Sequencer);
        var actionNode1 = CreateSampleActionNode1();
        var actionNode2 = CreateSampleActionNode1();
        var actionNode3 = CreateSampleActionNode1();
        var conditionFactory = FactoryManager.Instance.GetFactory<Condition, ConditionType>(FactoryType.Condition);
        var conditionNode = (ConditionNode) factory.Create(NodeType.Condition);
        var condition = conditionFactory.Create(ConditionType.Sample1);
        conditionNode.Condition = condition;
        sequencerNode.AddNode(conditionNode);
        sequencerNode.AddNode(actionNode1);
        sequencerNode.AddNode(actionNode2);
        sequencerNode.AddNode(actionNode3);
        rootNode.AddNode(sequencerNode);
    }

    private ActionNode CreateSampleActionNode1()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        var actionNode = (ActionNode)factory.Create(NodeType.Action);
        var actionFactory = FactoryManager.Instance.GetFactory<Action, ActionType>(FactoryType.Action);
        Action sampleAction = actionFactory.Create(ActionType.Sample1);
        actionNode.Action = sampleAction;
        return actionNode;
    }

    private ActionNode CreateSampleActionNode2()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        var actionNode = (ActionNode)factory.Create(NodeType.Action);
        var actionFactory = FactoryManager.Instance.GetFactory<Action, ActionType>(FactoryType.Action);
        Action sampleAction = actionFactory.Create(ActionType.Sample2);
        actionNode.Action = sampleAction;
        return actionNode;
    }
}
