using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoalController : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private CurtainManager curtainManager;
	private bool collissionOccurred = false;

	public void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" & !collissionOccurred)
		{
			gameController.triggerGameWon();
			collissionOccurred = true;
		}
	}
}
