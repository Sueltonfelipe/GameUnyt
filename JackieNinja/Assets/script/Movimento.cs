using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float speed = 10f; // Velocidade de movimento do personagem
    public float jumpForce = 10f; // Força do pulo do personagem
    public int maxJumps = 2; // Número máximo de pulos

    private bool isFacingRight = true; // Indica se o personagem está virado para a direita
    private Rigidbody2D rb; // Referência ao componente Rigidbody2D
    private Animator animator; // Referência ao componente Animator
    private int jumpsRemaining; // Número de pulos restantes
    private bool isGrounded; // Indica se o personagem está no chão

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D anexado ao personagem
        animator = GetComponent<Animator>(); // Obtém o componente Animator anexado ao personagem
        jumpsRemaining = maxJumps; // Inicializa o número de pulos restantes
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Obtém o input horizontal (esquerda ou direita)

        // Calcula o deslocamento baseado na velocidade e no input horizontal
        float movement = moveHorizontal * speed * Time.deltaTime;

        // Movimenta o personagem na direção desejada
        transform.Translate(movement, 0, 0);

        // Atualiza a animação de acordo com a direção e velocidade do movimento
        if (moveHorizontal != 0)
        {
            animator.SetBool("parado", false); // Define a animação "parado" como falsa
            animator.SetBool("correndo", true); // Define a animação "correndo" como verdadeira
        }
        else
        {
            animator.SetBool("correndo", false); // Define a animação "correndo" como falsa
            animator.SetBool("parado", true); // Define a animação "parado" como verdadeira
        }

        // Inverte a imagem do personagem de acordo com a direção do movimento
        if (moveHorizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && isFacingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsRemaining > 0) // Verifica se ainda é possível realizar um pulo
            {
                Jump(); // Chama a função de pulo
                jumpsRemaining--; // Reduz o número de pulos restantes
            }
        }
    }

    // Função de pulo do personagem
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // Inverte a imagem do personagem
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("terra"))
        {
            jumpsRemaining = maxJumps; // Restaura o número máximo de pulos quando colidir com objeto com tag "Terra"
        }
    }
}
