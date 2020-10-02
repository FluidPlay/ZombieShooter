using UnityEngine;

public class ProjectileTriggerCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var zombieController = other.GetComponent<ZombieController>();
        if (zombieController == null)
            return;
            
        Destroy(other.gameObject);    // Destroi o zumbi (?)
        Game.Manager.Score++;
        Destroy(gameObject);          // Destroi a propria bala
    }

}
