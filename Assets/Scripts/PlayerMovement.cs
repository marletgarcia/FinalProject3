using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spr;
    float moveForce = 2;
    float jumpForce = 5;
    public GameObject platform;
    public GameObject orbPrefab;
    public GameObject enemyPrefab;
    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public int maxEnergy = 100;
    public int currentEnergy;
    public Slider energySlider; // Reference to the UI Slider
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        respawnPoint = transform.position;
        currentEnergy = maxEnergy;
        energySlider.maxValue = maxEnergy; // Set the max value of the slider
        energySlider.value = currentEnergy; // Initialize the slider's value
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;  // Stop player movement when game is over

        rb.AddForce(transform.right * moveForce * Input.GetAxis("Horizontal"), ForceMode2D.Force);
        if (rb.velocity.magnitude > 0.01f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (rb.velocity.x < 0.01f)
        {
            spr.flipX = true;
        }
        else
        {
            spr.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetTrigger("Down");
        }
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    bool IsGrounded()
    {
        // Check if the player is grounded
        // This can be implemented using raycasts or collision checks with the ground layer
        return true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isGameOver) return;  // Stop interactions when game is over

        if (col.tag == "Spawn" && LevelManager.instance.CanSpawnPlatform())
        {
            float distance = Random.Range(8, 15);
            float height = Random.Range(-4, 3);

            GameObject newPlatform = Instantiate(platform, new Vector3(transform.position.x + distance, height, 0), Quaternion.identity);
            Destroy(col.gameObject);

            // Randomly decide whether to spawn an orb or an enemy
            if (Random.value > 0.5f)
            {
                // Spawn orb
                if (orbPrefab != null)
                {
                    float orbX = newPlatform.transform.position.x;
                    float orbY = newPlatform.transform.position.y + 1;  // Adjust Y position as needed
                    Instantiate(orbPrefab, new Vector3(orbX, orbY, 0), Quaternion.identity);
                }
            }
            else
            {
                // Spawn enemy
                if (enemyPrefab != null)
                {
                    float enemyX = newPlatform.transform.position.x;
                    float enemyY = newPlatform.transform.position.y + 1;  // Adjust Y position as needed
                    Instantiate(enemyPrefab, new Vector3(enemyX, enemyY, 0), Quaternion.identity);
                }
            }
        }
        else if (col.tag == "FallDetector")
        {
            transform.position = respawnPoint;
            LevelManager.instance.NextLevel(); // Proceed to the next level when falling
        }
        else if (col.tag == "Orb")
        {
            GainEnergy(10);  // Adjust the amount of energy gained from an orb as needed
            Destroy(col.gameObject);
        }
        else if (col.tag == "Enemy")
        {
            GainEnergy(-20);  // Adjust the amount of energy lost from an enemy as needed
            Destroy(col.gameObject);
        }
    }

    void GainEnergy(int amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        UpdateEnergyUI();
    }

    public void UpdateEnergyUI()
    {
        if (energySlider != null)
        {
            energySlider.value = currentEnergy;
        }

        if (currentEnergy <= 0)
        {
            GameOver();
        }
    }

    public void GameOver() 
    {
         isGameOver = true;
        rb.velocity = Vector2.zero;  // Stop player movement
        anim.SetBool("isWalking", false);
        Debug.Log("Game Over!");
        // Optionally, add additional game over logic here (e.g., disable controls, show game over screen, etc.)
    }
}
