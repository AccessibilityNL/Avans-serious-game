using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;
	[SerializeField] private CameraController cameraController;
	[SerializeField] private CurtainManager curtainManager;

	public void triggerGameOver()
	{
		//TODO FINISH
		Rigidbody rb = playerController.GetRigidbody();
		rb.constraints = RigidbodyConstraints.FreezeAll;
		curtainManager.gameObject.SetActive(true);
		curtainManager.Close();

	}

	public void triggerGameWon()
	{
		curtainManager.gameObject.SetActive(true);
		curtainManager.Close();
	}

	public void FinishGame()
	{
		var sceneLoader = FindObjectOfType<SceneLoader>();

		if (sceneLoader)
		{
			sceneLoader.ChangeToScene("FruitCounting", "visual", playerController.GetPoints());
		}
	}
}
