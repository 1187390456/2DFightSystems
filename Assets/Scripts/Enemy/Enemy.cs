using EpicToonFX;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Enemy : MonoBehaviour
{
    #region 核心

    public Core core { get; private set; }
    public Movement movement => core.movement;
    public EnemyCollisionSenses sense => core.enemyCollisionSenses;

    public EnemyStats stats => core.enemyStats;

    #endregion 核心

    public E_StateMachine stateMachine = new E_StateMachine();

    public Animator at { get; private set; }

    public AnimationToScript animationToScript { get; private set; }

    [HideInInspector] public MaterialPropertyBlock mpb; // 渲染材质空值
    [HideInInspector] public Renderer render;  // 渲染

    [HideInInspector] public GameObject stunEffect;

    [HideInInspector] public bool isUseAbility2 = false;

    public bool ablity2ing { get; set; }

    public bool deading { get; set; }

    public virtual void Awake()
    {
        core = GetComponentInChildren<Core>();
        at = GetComponent<Animator>();
        mpb = new MaterialPropertyBlock();
        render = GetComponent<Renderer>();
        animationToScript = GetComponent<AnimationToScript>();
        InitStunEffect();
    }

    public virtual void Update()
    {
        at.SetFloat("yVelocity", movement.rbY);
        stateMachine.currentState.Update();
    }

    public virtual void Start()
    {
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixUpdate();
    }

    // 设置骨骼动画渲染透明度
    public virtual void SetSpineTransparent(float transparent)
    {
        Color color = new Color(1f, 1f, 1f, transparent);
        mpb.SetColor("_Color", color);
        render.SetPropertyBlock(mpb);
    }

    private void InitStunEffect()
    {
        if (transform.Find("StunEffect") != null)
        {
            stunEffect = transform.Find("StunEffect").gameObject;
            stunEffect.SetActive(false);
        }
    }
}