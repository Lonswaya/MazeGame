using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {
	public Vector2 bounds = new Vector2(16, 8);
	public MainMenuController mainMenu;
	public GameObject marble;
	public SpriteHolder marbleSprites;
	public GameObject gridNode;
	public Transform grid;
	//public GridNode[,] nodes;
	public MyArray[] nodes;
	[System.Serializable]
	public class MyArray {
		public GridNode[] nodes;
	}
	private int iterations;
	// Use this for initialization
	void Start () {
		marble.GetComponent<SpriteRenderer>().sprite = marbleSprites.sprites[mainMenu.selectedMarble];
		//GridNode node = GameObject.Instantiate(gridNode, grid).GetComponent<GridNode>();
		nodes = new MyArray[(int)bounds.x - 1];
		//nodes = new GridNode[((int)bounds.x), ((int)bounds.y)];
		for (int x = 1; x < ((int)bounds.x); x++) {
			nodes[x - 1] = new MyArray();
			nodes[x - 1].nodes  = new GridNode[(int)bounds.y - 1];
			for (int y = 1; y < ((int)bounds.y); y++) {
				GridNode node = GameObject.Instantiate(gridNode, grid).GetComponent<GridNode>();
				nodes[x - 1].nodes[y - 1] = node;
				node.transform.position = new Vector2(x - ((int)bounds.x)/2,y - ((int)bounds.y)/2);
				if (x == 1 && y == ((int)bounds.y) - 1) {
					node.walls[2].SetActive(false);
					node.walls[3].SetActive(false);
				}
				else if (x == ((int)bounds.x) - 1 && y == 1) {
					node.walls[0].SetActive(false);
					node.walls[1].SetActive(false);
					node.sprite.color = Color.red;
					//print(x + " " + y);
				}
				else {
					// TODO assign gridnode walls based off of maze algo

					}
					/*foreach (GameObject wall in node.walls) {
						if (Random.Range(0f, 1f) > 0.5f) {
							wall.SetActive(false);
						}
					}*/
				}


			} 
		Stack s = new Stack();
		MakePath(new Vector2(0, ((int)bounds.y) - 2), s);

		// Recursive shtuff here

	}
	public void MakePath(Vector2 currentPos, Stack backtrace) {
		iterations++;
		if (iterations > 200) {
			//Debug.LogWarning("went over 200 iterations in a recursive loop, no thanks");
			//return;
		}
		backtrace.Push(currentPos);
		GridNode currentNode = getGridNode(currentPos);
		currentNode.sprite.color = Color.blue;
		bool[] valids = new bool[4];
		for (int i = 0; i < 4; i++) {
			valids[i] = IsValidLocation(getLocationDir(i, currentPos));
		}
		//print(valids[0] + " " + valids[1] + " " + valids[2] + " " + valids[3]);
		ArrayList options = new ArrayList();
		for (int i = 0; i < valids.Length; i++) {
			if (valids[i]) {
				options.Add(i);
			}
		}
		//print("options: " + options.Count);
		if (options.Count == 0) {
			print("Backtracking");
			// SOL, gotta backtrack
			Vector2 p = new Vector2();
			int whileCnt = 0;
			while(backtrace.Peek() != null) {
				whileCnt++;
				if (whileCnt > 200) {
					Debug.LogWarning("went over 200 iterations in a while loop, no thanks");
					return;
				}
				p = (Vector2)backtrace.Pop();
				GridNode g = getGridNode(p);
				// We set visited to the last path we took from the first

				if (backtrace.Count < 1) {
					g.visited = false;
					g.sprite.color = Color.white;
				} else {
					//print(p.x + " " + p.y);
				}
			}
	
			backtrace.Clear();
			MakePath(p, backtrace);
			return;
		}

		int randomDir = (int)options[Random.Range(0, options.Count)];
		// transfer to that tile, break walls in between
		Vector2 targetPos = getLocationDir(randomDir, currentPos);
		GridNode current = getGridNode(currentPos);
		GridNode target = getGridNode(targetPos);
		current.walls[randomDir].SetActive(false);
		target.walls[(randomDir + 2)%4].SetActive(false);
		MakePath(targetPos, backtrace);
	}
	public GridNode getGridNode(Vector2 pos) {
		//print((pos.x) + " " + (pos.y)) ;
		return nodes[((int)pos.x)].nodes[((int)pos.y)];
	}
	public bool IsValidLocation(Vector2 pos) {
		//print(pos.x + " " + pos.y + " " + nodes[0].nodes.Length);
		if (((int)pos.x) <= 0 || ((int)pos.x) >= nodes.Length || ((int)pos.y) <= 0 || ((int)pos.y) >= nodes[0].nodes.Length) return false; 
		if (getGridNode(pos).visited) return false;

		return true;

	}
	public Vector2 getLocationDir(int direciton, Vector2 pos) {
		switch (direciton) {
		case 0:
			return new Vector2(((int)pos.x) - 1, ((int)pos.y));
			break;
		case 1:
			return new Vector2(((int)pos.x), ((int)pos.y) + 1);
			break;
		case 2:
			return new Vector2(((int)pos.x) + 1, ((int)pos.y));
			break;
		case 3:
			return new Vector2(((int)pos.x), ((int)pos.y) - 1);
			break;
		}
		return pos;
	}

}
