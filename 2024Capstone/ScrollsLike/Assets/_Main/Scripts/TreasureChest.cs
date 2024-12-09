using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private bool isOpened = false;

    private void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            transform.LookAt(GameManager.Instance.Player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player")) 
        {
            OpenChest();
            GameManager.Instance.SetPause();
        }
    }

    private void OpenChest()
    {
        isOpened = true;

        GameManager.Instance.CardRewards();

        Debug.Log("Chest opened! Showing rewards.");

        GameManager.Instance.ResumeGame();

        Destroy(gameObject, 0.5f); 
    }
}
