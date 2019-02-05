using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCreator : Manager<NodeCreator>
{
    public INode CreateTestNode1()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        var rootNode = factory.Create(NodeType.Root);
        //var actionNode = (ActionNode)factory.Create(NodeType.Action);
        var actionNode = CreateSampleActionNode1();
        //actionNode.Action = sampleAction;
        rootNode.AddNode(actionNode);
        return rootNode;
    }

    // sequencer
    public INode CreateTestNode2()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        var rootNode = factory.Create(NodeType.Root);

        var sequencerNode = (SequencerNode)factory.Create(NodeType.Sequencer);
        var actionNode1 = CreateSampleActionNode1();
        var actionNode2 = CreateSampleActionNode1();
        var actionNode3 = CreateSampleActionNode1();
        sequencerNode.AddNode(actionNode1);
        sequencerNode.AddNode(actionNode2);
        sequencerNode.AddNode(actionNode3);
        rootNode.AddNode(sequencerNode);
        return rootNode;
    }

    // selector
    public INode CreateTestNode3()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        var rootNode = factory.Create(NodeType.Root);
        var selectorNode = (SelectorNode)factory.Create(NodeType.Selector);
        var actionNode1 = CreateSampleActionNode1();
        var actionNode2 = CreateSampleActionNode2();
        selectorNode.AddNode(actionNode1, 1);
        selectorNode.AddNode(actionNode2, 100);
        rootNode.AddNode(selectorNode);
        return rootNode;
    }

    // Decorate 失敗 
    public INode CreateTestNode4()
    {
        var factory = FactoryManager.Instance.GetFactory<Node, NodeType>(FactoryType.Node);
        var rootNode = factory.Create(NodeType.Root);

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
        return rootNode;
    }

    public INode CreateTestNode5()
    {
        var factory = FactoryManager.Instance.GetFactory<NodeFactory>(FactoryType.Node);
        var rootNode = factory.Create(NodeType.Root);

        var sequencerNode = factory.Create<SequencerNode>(NodeType.Sequencer);
        var actionNode1 = CreateSampleActionNode1();
        var actionNode2 = CreateSampleActionNode1();
        var actionNode3 = CreateSampleActionNode1();
        var conditionFactory = FactoryManager.Instance.GetFactory<ConditionFactory>(FactoryType.Condition);
        var conditionNode = factory.Create<ConditionNode>(NodeType.Condition);
        var condition = conditionFactory.Create<Condition>(ConditionType.Sample1);
        conditionNode.Condition = condition;
        sequencerNode.AddNode(conditionNode);
        sequencerNode.AddNode(actionNode1);
        sequencerNode.AddNode(actionNode2);
        sequencerNode.AddNode(actionNode3);
        rootNode.AddNode(sequencerNode);
        return rootNode;
    }

    public INode CreateTestNode6()
    {
        var factory = FactoryManager.Instance.GetFactory<NodeFactory>(FactoryType.Node);
        var rootNode = factory.Create(NodeType.Root);

        var sequencerNode = factory.Create<SequencerNode>(NodeType.Sequencer);
        var actionNode1 = CreateSampleActionNode1();
        var actionNode2 = CreateSampleActionNode2();
        var conditionFactory = FactoryManager.Instance.GetFactory<ConditionFactory>(FactoryType.Condition);
        var conditionNode = factory.Create<ConditionNode>(NodeType.Condition);
        var condition = conditionFactory.Create(ConditionType.Sample1);
        conditionNode.Condition = condition;
        conditionNode.AddNode(sequencerNode);
        sequencerNode.AddNode(actionNode1);
        sequencerNode.AddNode(actionNode2);
        rootNode.AddNode(conditionNode);
        return rootNode;
    }

    public INode CreateTestNode7()
    {
        NodeFactory factory = FactoryManager.Instance.GetFactory<NodeFactory>(FactoryType.Node);
        ConditionFactory conditionFactory = FactoryManager.Instance.GetFactory<ConditionFactory>(FactoryType.Condition);

        var rootNode = factory.Create<RootNode>(NodeType.Root);
        var sequencerNode = factory.Create<SequencerNode>(NodeType.Sequencer);
        var actionNode1 = CreateSampleActionNode1();
        var conditionNode = factory.Create<ConditionWhileNode>(NodeType.ConditionWhile);
        var repeatCondition = conditionFactory.Create<RepeatCondition>(ConditionType.Repeat);
        repeatCondition.LoopCount = 3;
        conditionNode.Condition = repeatCondition;
        conditionNode.AddNode(sequencerNode);
        sequencerNode.AddNode(actionNode1);
        rootNode.AddNode(conditionNode);
        return rootNode;
    }

    private ActionNode CreateSampleActionNode1()
    {
        var factory = FactoryManager.Instance.GetFactory<NodeFactory>(FactoryType.Node);
        var actionNode = factory.Create<ActionNode>(NodeType.Action);
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
