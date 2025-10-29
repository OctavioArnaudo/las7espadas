using UnityEngine;

public class PlayerCoinCollision : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Coin"))
		{
			KnapsackModel inventory = GetComponent<KnapsackModel>();
			InventoryModel item = GetComponent<InventoryModel>();
			if (inventory != null)
			{
				bool isAdded = inventory.AddItem(item);
			}
			Destroy(collision.gameObject);
		}
	}
}
