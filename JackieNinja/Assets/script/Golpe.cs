using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpe : MonoBehaviour
{
    private Animator animator; // Referência ao componente Animator
    private bool isAttacking = false; // Indica se o personagem está atacando

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtém o componente Animator anexado ao personagem
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isAttacking)
        {
            Attack();
        }

        if (Input.GetKeyUp(KeyCode.X) && isAttacking)
        {
            CancelAttack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("ataque"); // Ativa a animação de ataque

        // Lógica adicional do ataque
    }

    void CancelAttack()
    {
        isAttacking = false;
        animator.ResetTrigger("ataque"); // Reseta a animação de ataque
    }
}
