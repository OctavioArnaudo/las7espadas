using UnityEngine;

public class InitAnimator : BaseSpriteRenderer
{
    [AnimatorModel("AttackType")][SerializeField] protected int attackType = 0; // 0 = ninguno, 1 = melee, 2 = ranged
    [AnimatorModel("canDash")][SerializeField] protected bool canDash;
    [AnimatorModel("Health")][SerializeField] public int currentHP = 1;
    [AnimatorModel("Health")][SerializeField] public int maxHP = 1;
    [AnimatorModel("hurtPressed")][SerializeField] protected bool hurtPressed;
    [AnimatorModel("isAlive")][SerializeField] protected bool isAlive;
    [AnimatorModel("IsAttacking")][SerializeField] protected bool isAttacking = false;
    [AnimatorModel("isBroken")][SerializeField] protected bool isBroken;
    [AnimatorModel("isDashing")][SerializeField] protected bool isDashing;
    [AnimatorModel("IsDead")][SerializeField] protected bool isDead = false;
    [AnimatorModel("isDefeated")][SerializeField] protected bool isDefeated;
    [AnimatorModel("isGameOver")][SerializeField] protected bool isGameOver;
    [AnimatorModel("isGrounded")][SerializeField] protected bool isGrounded;
    [AnimatorModel("IsHurt")][SerializeField] protected bool isHurt = false;
    [AnimatorModel("IsIdle")][SerializeField] protected bool isIdle = false;
    [AnimatorModel("isInFlight")][SerializeField] protected bool isInFlight;
    [AnimatorModel("isInPursuit")][SerializeField] protected bool isInPursuit = false;
    [AnimatorModel("isInvulnerable")][SerializeField] protected bool isInvulnerable;
    [AnimatorModel("isJumping")][SerializeField] protected bool isJumping;
    [AnimatorModel("isLanded")][SerializeField] protected bool isLanded;
    [AnimatorModel("isLanding")][SerializeField] protected bool isLanding;
    [AnimatorModel("isPatrolling")][SerializeField] protected bool isPatrolling = true;
    [AnimatorModel("isPreparingToJump")][SerializeField] protected bool isPreparingToJump;
    [AnimatorModel("isRespawning")][SerializeField] protected bool isRespawning;
    [AnimatorModel("isRunning")][SerializeField] protected bool isRunning;
    [AnimatorModel("isTouchingWall")][SerializeField] protected bool isTouchingWall;
    [AnimatorModel("isVictorious")][SerializeField] protected bool isVictorious;
    [AnimatorModel("isWalking")][SerializeField] protected bool isWalking;
    [AnimatorModel("isWallSliding")][SerializeField] protected bool isWallSliding;
    [AnimatorModel("jumpPressed")][SerializeField] protected bool jumpPressed = false;
    [AnimatorModel("jumpReleased")][SerializeField] protected bool jumpReleased = false;
    [AnimatorModel("spawnPressed")][SerializeField] protected bool spawnPressed;

    [Range(2F, 6F)]
    protected float maxJumpSpeed = 6F;
    [Range(1F, 9.81F)]
    protected float jumpDeceleration = 2F;
    [AnimatorModel("Speed")]
    [SerializeField]
    [Range(2F, 6F)]
    public float maxMoveSpeed = 6F;
    [Range(2F, 15F)]
    public float moveAcceleration = 3F;

    protected string currentState = "";

    protected override void Update()
    {
        base.Update();
        isAlive = currentHP > 0;
    }
}