using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string targetTag = "Player"; // Tag do objeto que a câmera deve seguir
    public float smoothSpeed = 0.125f; // Velocidade de suavização do movimento da câmera
    public float minX = -83.31f; // Valor mínimo de posição x
    public float maxX = 288.54f; // Valor máximo de posição x

    private Transform target; // Referência ao transform do objeto que a câmera deve seguir
    private Vector3 offset; // Distância entre a câmera e o objeto

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(targetTag); // Encontra o objeto a ser seguido com base na tag

        if (player != null)
        {
            target = player.transform;
            offset = transform.position - target.position; // Calcula o offset inicial entre a câmera e o objeto
        }
        else
        {
            Debug.LogWarning("Objeto com tag '" + targetTag + "' não encontrado para a câmera seguir.");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a posição de destino da câmera
            Vector3 targetPosition = target.position + offset;
            targetPosition.y = transform.position.y; // Mantém a posição Y da câmera fixa

            // Verifica se a posição x do personagem está dentro dos limites
            if (target.position.x <= minX || target.position.x >= maxX)
            {
                targetPosition.x = transform.position.x; // Mantém a posição x da câmera fixa
            }

            // Suaviza o movimento da câmera usando Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Atualiza a posição da câmera
            transform.position = smoothedPosition;
        }
    }
}