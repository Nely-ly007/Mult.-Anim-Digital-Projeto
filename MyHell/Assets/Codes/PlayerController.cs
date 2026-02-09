using UnityEngine;
using DragonBones;

public class PlayerController : MonoBehaviour
{
    [Header("Componentes")]
    private UnityArmatureComponent armature;

    [Header("Movimento")]
    [SerializeField] private float velocidadeMovimento = 5f;
    [SerializeField] private float forcaPulo = 10f;

    [Header("Detecção de Chão")]
    [SerializeField] private float distanciaChao = 1.5f;
    [SerializeField] private LayerMask layerChao;
    [SerializeField] private bool usarDeteccaoChao = false;

    [Header("Nomes das Animações DragonBones")]
    [SerializeField] private string animIdle = "idle";
    [SerializeField] private string animAndar = "anda";
    [SerializeField] private string animPular = "pula";
    [SerializeField] private string animAtacar = "atack";

    [Header("Controles")]
    [SerializeField] private KeyCode teclaPular = KeyCode.Space;
    [SerializeField] private KeyCode teclaAtacar = KeyCode.Z;

    // Variáveis privadas
    private Rigidbody2D rb;
    private bool estaNoChao;
    private bool estaAtacando;
    private bool estaPulando;
    private float movimentoHorizontal;
    private bool olhandoDireita = true;
    private string animacaoAtual = "";
    private float tempoUltimaAnimacao = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        armature = GetComponentInChildren<UnityArmatureComponent>();

        if (armature == null)
        {
            Debug.LogError("UnityArmatureComponent não encontrado!");
            return;
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado!");
            return;
        }

        Debug.Log("PlayerController iniciado!");
        TocarAnimacao(animIdle);
    }

    void Update()
    {
        if (armature == null || rb == null) return;

        // Captura input
        movimentoHorizontal = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movimentoHorizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movimentoHorizontal = 1;
        }

        // Verifica chão
        if (usarDeteccaoChao)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanciaChao, layerChao);
            estaNoChao = hit.collider != null;
        }
        else
        {
            // Detecta chão pela velocidade Y
            estaNoChao = Mathf.Abs(rb.linearVelocity.y) < 0.5f;
        }

        // Se tocou o chão, não está mais pulando
        if (estaNoChao && estaPulando)
        {
            estaPulando = false;
        }

        // Pulo
        if (Input.GetKeyDown(teclaPular) && estaNoChao && !estaAtacando)
        {
            Debug.Log("COMANDO DE PULO! Velocidade Y antes: " + rb.linearVelocity.y);
            Pular();
        }

        // Ataque
        if (Input.GetKeyDown(teclaAtacar) && !estaAtacando)
        {
            Atacar();
        }

        // Virar o personagem
        VirarPersonagem();

        // Atualizar animações (com delay para não chamar toda hora)
        if (Time.time - tempoUltimaAnimacao > 0.1f)
        {
            if (!estaAtacando && !estaPulando)
            {
                AtualizarAnimacoes();
            }
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        if (!estaAtacando)
        {
            rb.linearVelocity = new Vector2(movimentoHorizontal * velocidadeMovimento, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void Pular()
    {
        estaPulando = true;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        TocarAnimacao(animPular);
        Debug.Log("PULOU! Velocidade Y: " + rb.linearVelocity.y);
    }

    void Atacar()
    {
        estaAtacando = true;
        TocarAnimacao(animAtacar);
        Invoke("FinalizarAtaque", 0.5f);
    }

    void FinalizarAtaque()
    {
        estaAtacando = false;
    }

    void AtualizarAnimacoes()
    {
        string novaAnimacao = "";

        // Prioridade: pulo > andar > idle
        if (!estaNoChao || estaPulando)
        {
            novaAnimacao = animPular;
        }
        else if (Mathf.Abs(movimentoHorizontal) > 0.1f)
        {
            novaAnimacao = animAndar;
        }
        else
        {
            novaAnimacao = animIdle;
        }

        TocarAnimacao(novaAnimacao);
    }

    void TocarAnimacao(string nomeAnimacao)
    {
        if (armature == null || armature.animation == null) return;

        // Só muda se for diferente da atual
        if (animacaoAtual != nomeAnimacao)
        {
            animacaoAtual = nomeAnimacao;
            tempoUltimaAnimacao = Time.time;

            // Determina se deve fazer loop
            bool deveLoopear = (nomeAnimacao == animIdle || nomeAnimacao == animAndar);
            int playTimes = deveLoopear ? 0 : 1;

            armature.animation.Play(nomeAnimacao, playTimes);
            Debug.Log(">>> Animação mudou para: " + nomeAnimacao);
        }
    }

    void VirarPersonagem()
    {
        if (movimentoHorizontal > 0 && !olhandoDireita)
        {
            Virar();
        }
        else if (movimentoHorizontal < 0 && olhandoDireita)
        {
            Virar();
        }
    }

    void Virar()
    {
        olhandoDireita = !olhandoDireita;
        Vector3 escala = armature.transform.localScale;
        escala.x *= -1;
        armature.transform.localScale = escala;
    }

    void OnDrawGizmos()
    {
        if (usarDeteccaoChao)
        {
            Gizmos.color = estaNoChao ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanciaChao);
        }
    }
}