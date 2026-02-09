using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform alvo; // O personagem
    [SerializeField] private float suavidade = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    void LateUpdate()
    {
        if (alvo == null) return;

        Vector3 posicaoDesejada = alvo.position + offset;
        Vector3 posicaoSuave = Vector3.Lerp(transform.position, posicaoDesejada, suavidade * Time.deltaTime);
        transform.position = posicaoSuave;
    }
}