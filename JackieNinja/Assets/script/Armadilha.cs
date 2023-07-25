using UnityEngine;
using UnityEngine.SceneManagement;

public class Armadilha : MonoBehaviour
{
    public string playerTag = "Player"; // Nome da tag do jogador

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}