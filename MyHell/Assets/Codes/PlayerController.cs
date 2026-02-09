using UnityEngine;
using DragonBones;

public class PlayerController : MonoBehaviour
{
    [Header("Componentes")]
    private UnityArmatureComponent armature;

    [Header("Movimento")]
    [SerializeField] private float velocidadeMovimento = 5f;
    [SerializeField] private float forcaPulo = 10f;

    [Header("Detec��o de Ch�o")]
    [SerializeField] private UnityEngine.Transform verificadorChao;
    [SerializeField] private float raioVerificacao = 0.2f;
    [SerializeField] private LayerMask layerChao;

    [Header("Nomes das Anima��es DragonBones")]
    [SerializeField] private string animIdle = "idle";
    [SerializeField] private string animAndar = "anda";
    [SerializeField] private string animPular = "pula";
    [SerializeField] private string animAtacar = "atack";

    [Header("Controles")]
    [SerializeField] private KeyCode teclaPular = KeyCode.Space;
    [SerializeField] private KeyCode teclaAtacar = KeyCode.Z;

    // Vari�veis privadas
    private Rigidbody2D rb;
    private bool estaNoChao;
    private bool estaAtacando;
    private float movimentoHorizontal;
    private bool olhandoDireita = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        armature = GetComponentInChildren<UnityArmatureComponent>();

        if (armature == null)
        {
            Debug.LogError("UnityArmatureComponent n�o encontrado! Verifique se o DragonBones est� configurado corretamente.");
        }

        TocarAnimacao(animIdle, true);
    }

    void Update()
    {
        // Captura input do teclado
        movimentoHorizontal = Input.GetAxisRaw("Horizontal");

        // Verifica se est� no ch�o
        if (verificadorChao != null)
        {
            estaNoChao = Physics2D.OverlapCircle(verificadorChao.position, raioVerificacao, layerChao);
        }

        // Pulo
        if (Input.GetKeyDown(teclaPular) && estaNoChao && !estaAtacando)
        {
            Pular();
        }

        // Ataque
        if (Input.GetKeyDown(teclaAtacar) && !estaAtacando)
        {
            Atacar();
        }

        // Virar o personagem
        VirarPersonagem();

        // Atualizar anima��es
        if (!estaAtacando)
        {
            AtualizarAnimacoes();
        }
    }

    void FixedUpdate()
    {
        // Movimento horizontal
        if (!estaAtacando)
        {
            rb.linearVelocity = new Vector2(movimentoHorizontal * velocidadeMovimento, rb.linearVelocity.y);
        }
    }

    void Pular()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        TocarAnimacao(animPular, false);
    }

    void Atacar()
    {
        estaAtacando = true;
        TocarAnimacao(animAtacar, false);

        // Dura��o do ataque (ajuste conforme sua anima��o)
        Invoke("FinalizarAtaque", 0.5f);
    }

    void FinalizarAtaque()
    {
        estaAtacando = false;
    }

    void AtualizarAnimacoes()
    {
        // Se est� no ar
        if (!estaNoChao)
        {
            if (armature.animation.lastAnimationName != animPular)
            {
                TocarAnimacao(animPular, false);
            }
        }
        // Se est� andando
        else if (Mathf.Abs(movimentoHorizontal) > 0.1f)
        {
            TocarAnimacao(animAndar, true);
        }
        // Se est� parado
        else
        {
            TocarAnimacao(animIdle, true);
        }
    }

    void TocarAnimacao(string nomeAnimacao, bool loop)
    {
        if (armature != null && armature.animation.lastAnimationName != nomeAnimacao)
        {
            armature.animation.Play(nomeAnimacao, loop ? 0 : 1);
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
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void OnDrawGizmosSelected()
    {
        if (verificadorChao != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(verificadorChao.position, raioVerificacao);
        }
    }
}