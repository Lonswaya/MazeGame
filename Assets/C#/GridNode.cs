using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour {
	public GameObject[] walls;
	public SpriteRenderer sprite;

	[HideInInspector]
	public bool visited = false;
}
