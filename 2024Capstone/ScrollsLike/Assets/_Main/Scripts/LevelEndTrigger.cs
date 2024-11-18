using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            DungeonLevelLoader levelLoader = FindObjectOfType<DungeonLevelLoader>();
            if (levelLoader != null)
            {
                levelLoader.LoadNextLevel();
            }
        }
    }
}
