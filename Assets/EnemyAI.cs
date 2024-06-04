using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Variables for enemy movement
    public float moveSpeed;
    public float rotationSpeed;

    // Variables for enemy death
    public float deathSize;
    private bool isDead;

    // Reference to player game object
    public GameObject player;

    void Update()
    {
        if (!isDead)
        {
            // Move towards player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);


            // Randomize movement
            transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Time.deltaTime;
        }
    }
}
