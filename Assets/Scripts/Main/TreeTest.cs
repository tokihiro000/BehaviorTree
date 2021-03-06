﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        INode rootNode = NodeCreator.Instance.CreateRandomSelectorTest();
        BehaviorTree tree = new BehaviorTree();
        tree.Prepare(rootNode);
        tree.Awake();
        StartCoroutine(tree.Update());
    }
}
