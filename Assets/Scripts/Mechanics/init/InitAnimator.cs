using System;
using UnityEngine;

[Serializable]
public class InitAnimator : Init3D
{
    [Header("Animator Model Parameters")]
    [AnimatorModel("AttackType")][Range(0F, 2F)] public int attackType = 0; // 0 = ninguno, 1 = melee, 2 = ranged
    [AnimatorModel("CanDash")] public bool canDash;
    [AnimatorModel("DashCooldown")] public float dashCooldown;
    [AnimatorModel("DashDuration")] public float dashDuration;
    [AnimatorModel("DashForce")] public float dashForce;
    [AnimatorModel("EmitOn")] public bool emitOnCharacterDeath = true;
    [AnimatorModel("Health")] public int currentHP = 1;
    [AnimatorModel("Health")] public int maxHP = 1;
    [AnimatorModel("Horizontal")] public float horizontalInput;
    [AnimatorModel("HurtPressed")] public bool hurtPressed;
    [AnimatorModel("IsAlive")] public bool isAlive;
    [AnimatorModel("IsAttacking")] public bool isAttacking = false;
    [AnimatorModel("IsBroken")] public bool isBroken;
    [AnimatorModel("IsDashing")] public bool isDashing;
    [AnimatorModel("IsDead")] public bool isDead = false;
    [AnimatorModel("IsDefeated")] public bool isDefeated;
    [AnimatorModel("IsFlippedX")] public bool isFlippedX;
    [AnimatorModel("IsGameOver")] public bool isGameOver;
    [AnimatorModel("IsGrounded")] public bool isGrounded;
    [AnimatorModel("IsHurt")] public bool isHurt = false;
    [AnimatorModel("IsIdle")] public bool isIdle = false;
    [AnimatorModel("IsInFlight")] public bool isInFlight;
    [AnimatorModel("IsInPursuit")] public bool isInPursuit = false;
    [AnimatorModel("IsInvulnerable")] public bool isInvulnerable;
    [AnimatorModel("IsJumping")] public bool isJumping;
    [AnimatorModel("IsLanded")] public bool isLanded;
    [AnimatorModel("IsLanding")] public bool isLanding;
    [AnimatorModel("IsMoving")] public bool isMoving;
    [AnimatorModel("IsPatrolling")] public bool isPatrolling = true;
    [AnimatorModel("IsPreparingToJump")] public bool isPreparingToJump;
    [AnimatorModel("IsRespawning")] public bool isRespawning;
    [AnimatorModel("IsRunning")] public bool isRunning;
    [AnimatorModel("IsTouchingWall")] public bool isTouchingWall = false;
    [AnimatorModel("IsVictorious")] public bool isVictorious;
    [AnimatorModel("IsWalking")] public bool isWalking;
    [AnimatorModel("IsWallSliding")] public bool isWallSliding = false;
    [AnimatorModel("JumpDeceleration")][Range(1F, 9.81F)] public float jumpDeceleration = 2F;
    [AnimatorModel("JumpPressed")] public bool jumpPressed = false;
    [AnimatorModel("JumpReleased")] public bool jumpReleased = false;
    [AnimatorModel("JumpSpeed")][Range(2F, 6F)] public float jumpSpeed = 6F;
    [AnimatorModel("LifeTime")] public float lifetime = 2f;
    [AnimatorModel("MenuShown")] public bool menuShown = false;
    [AnimatorModel("MoveAcceleration")][Range(2F, 15F)] public float moveAcceleration = 3F;
    [AnimatorModel("MoveSpeed")][Range(2F, 6F)] public float moveSpeed = 6F;
    [AnimatorModel("POV")] public float fov;
    [AnimatorModel("SpawnDelay")] public float spawnDelay = 1f;
    [AnimatorModel("SpawnDistance")] public float spawnDistance = 0F;
    [AnimatorModel("SpawnPressed")] public bool spawnPressed;
    [AnimatorModel("SpawnSize")] public int spawnSize = 1;
    [AnimatorModel("SpawnSpeed")] public float spawnSpeed = 10f;
    [AnimatorModel("Vertical")] public float verticalInput;
    [AnimatorModel("WallDirX")] public int wallDirX;
    [AnimatorModel("WallSlideSpeed")] public float wallSlideSpeed = 2F;
}