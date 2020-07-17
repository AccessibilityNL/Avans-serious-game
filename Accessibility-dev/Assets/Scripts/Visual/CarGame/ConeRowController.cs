using UnityEngine;

public class ConeRowController : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;
	[SerializeField] private int damageToInflict = 1;
	private bool collissionOccurred = false;

	public void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" & !collissionOccurred) {
			playerController.ReceiveDamage(damageToInflict);
			collissionOccurred = true;
		}
	}
}