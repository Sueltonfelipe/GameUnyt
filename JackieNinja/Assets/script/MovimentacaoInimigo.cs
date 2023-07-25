using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentacaoInimigo : MonoBehaviour
{
    public float speed = 2f; // Velocidade de movimento do objeto
    public string wallTag = "parede"; // Tag do objeto que representa uma parede

    private bool isMovingRight = true; // Indica se o objeto está se movendo para a direita

    void Update()
    {
        float movement = speed * Time.deltaTime;

        if (isMovingRight)
        {
            transform.Translate(Vector2.right * movement);
        }
        else
        {
            transform.Translate(Vector2.left * movement);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(wallTag))
        {
            ChangeDirection();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            RestartLevel();
        }
    }

    void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}