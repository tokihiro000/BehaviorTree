using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BehaviorTree tree = new BehaviorTree();
        tree.Prepare();
        tree.Awake();
        StartCoroutine(tree.Update());
    }
}
