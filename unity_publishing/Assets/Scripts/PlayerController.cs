using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables

    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public  Image winLoseImage;
    public int health = 5;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] Rigidbody rb;

    private int score = 0;

    #endregion
    #region Unity Callbacks

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(horizontalInput * speed, rb.linearVelocity.y, verticalInput * speed);
    }

    void Update()
    {
        if (health == 0)
            {
                //Debug.Log("Game Over!");
                SetEndScreen(false);
            }
        if (Input.GetKey("escape"))
            SceneManager.LoadScene("menu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            //Debug.Log($"Score: {score}");
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            //Debug.Log($"Health: {health}");
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            SetEndScreen(true);
            //Debug.Log("You win!");
        }
    }
    #endregion
    #region UI
        
    private void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    private void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }

    private void SetEndScreen(bool win)
    {
        if (win)
        {
            winLoseText.text = "You Win!";
            winLoseText.color = Color.black;
            winLoseImage.color = Color.green;
            winLoseImage.gameObject.SetActive(true);
        } else
        {
            winLoseText.text = "Game Over!";
            winLoseText.color = Color.white;
            winLoseImage.color = Color.red;
            winLoseImage.gameObject.SetActive(true);
        }
        StartCoroutine(LoadScene(3));
    }

    #endregion

    #region Methods
        
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(this.gameObject.scene.name);


    }
    #endregion
}
