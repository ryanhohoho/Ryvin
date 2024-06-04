using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int coinValue = 1; // The amount to add to the coin count when collected
    public float fadeSpeed = 2.0f; // The speed at which the coin will fade out
    public float rotationSpeed = 50.0f; // The speed at which the coin will rotate
    public float bobbingSpeed = 0.5f; // The speed at which the coin will bob up and down
    public float bobbingAmount = 0.05f; // The amount that the coin will bob up and down

    private float initialY; // The starting Y position of the coin
    private bool fading = false; // Whether or not the coin is currently fading out
    private Renderer renderer; // The renderer of the coin

    void Start()
    {
        // Store the initial Y position of the coin
        initialY = transform.position.y;

        // Get the renderer of the coin
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Rotate the coin
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Bob the coin up and down
        float newY = initialY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Fade out the coin if necessary
        if (fading)
        {
            renderer.material.color = new Color(1, 1, 1, renderer.material.color.a - fadeSpeed * Time.deltaTime);
            if (renderer.material.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player touches the coin, add to the coin count and start fading out
        if (other.CompareTag("Player"))
        {
            //CoinCount.instance.AddCoins(coinValue);
            fading = true;
        }
    }
}