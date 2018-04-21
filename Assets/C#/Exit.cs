using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.GetComponent<MarbleMover>()) {
			print("Finished maze");
			// Triggers are in the finish area
			// End the game, and tell main menu to restart
			this.GetComponentInParent<Maze>().mainMenu.SendMessage("NextMaze");
		}

	}
}
