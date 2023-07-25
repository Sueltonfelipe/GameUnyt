using UnityEngine;

public class Ataque : MonoBehaviour
{
    public string wallTag = "parede"; // Tag do objeto da parede
    public float speed = 2f; // Velocidade de movimento do objeto

    private Rigidbody2D rb; // Referência ao componente Rigidbody2D

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D
        Destroy(gameObject, 6f); // Autodestruição após 4 segundos
    }

    private void Update()
    {
        // Movimenta o objeto para a direita
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(wallTag))
        {
            Destroy(collision.gameObject); // Destruir o objeto da parede que colidiu
        }
    }
}