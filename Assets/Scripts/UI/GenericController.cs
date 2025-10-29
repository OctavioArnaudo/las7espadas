using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;

public class GenericController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public int health = 3;
    public int pain = 0;
    public int coins = 0;
    public int lives = 3;
    public int keys = 0;
    public float platformTimerMax = 5f;

    [Header("Menu Settings")]
    public string gameSceneName = "SampleScene";

    [Header("Scene Management")]
    public string victorySceneName = "VictoryScene";

    [Header("Audio Clips")]
    public AudioClip idleClip;
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip jumpClip;
    public AudioClip landClip;
    public AudioClip deathClip;
    public AudioClip hurtClip;
    public AudioClip victoryClip;
    public AudioClip defeatClip;
    public AudioClip spawnClip;
    public AudioClip checkpointClip;
    public AudioClip keyClip;
    public AudioClip doorClip;
    public AudioClip dashClip;
    public AudioClip coinClip;      
    public AudioClip enemyClip;     
    public AudioClip powerUpClip;   
    public AudioClip breakPlatformClip; 

    [Header("Temporary Platform Settings")]
    public float tempPlatformBreakTime = 1.5f;
    public GameObject breakEffectPrefab;

    [Header("Jump Settings")]
    public int maxJumps = 2;

    [Header("Player States")]
    public float invulnerableTime = 1.2f;
    public Color invulnerableColor = new Color(1, 1, 1, 0.5f);

    [Header("Wall Slide/Jump")]
    public float wallSlideSpeed = 1.5f;
    public float wallJumpForce = 8f;
    public LayerMask wallLayer;
    private bool isTouchingWall = false;
    private bool isWallSliding = false;
    private int wallDirX = 0;

    [Header("Dash")]
    public float dashForce = 12f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;

    // --- UI: TextMeshPro ---
    [Header("UI TextMeshPro")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI keysText;
    public TextMeshProUGUI infoText;

    // --- Botones auxiliares ---
    [Header("Botones Auxiliares")]
    public Button respawnButton;
    public Button dieButton;
    public Button victoryButton;
    public Button hurtButton;

    // --- Moving Platforms ---
    [Header("Moving Platforms")]
    public MovingPlatformData[] movingPlatforms;

    [Serializable]
    public class MovingPlatformData
    {
        public Transform platform;
        public float moveDistance = 5f;
        public float moveSpeed = 2f;

        [HideInInspector] public Vector3 startPos;
        [HideInInspector] public Vector3 endPos;
        [HideInInspector] public bool movingToEnd = true;
    }

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public Button restartButton;
    public Button exitButton;
    public Button rebindJumpButton;
    public Button rebindDashButton;
    public TextMeshProUGUI jumpKeyText;
    public TextMeshProUGUI dashKeyText;

    // Teclas configurables
    [Header("Key Bindings")]
    public KeyCode respawnKey = KeyCode.R;
    public KeyCode dieKey = KeyCode.F;
    public KeyCode victoryKey = KeyCode.V;
    public KeyCode hurtKey = KeyCode.H;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;

    private bool waitingForRebind = false;
    private string rebindAction = "";

    // Eventos para UI y logros
    public event Action<int> OnHealthChanged;
    public event Action<int> OnCoinsChanged;
    public event Action<int> OnKeysChanged;
    public event Action<int> OnLivesChanged;
    public event Action<string> OnAchievementUnlocked;

    private bool isGrounded;
    private int jumpsLeft;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private string lastState = "";
    private float platformTimer;
    private bool isInvulnerable = false;
    private Color originalColor;
    private Vector3 lastCheckpointPosition;
    private Vector3 initialPosition;
    private bool isGameOver = false;

    // Power-up control
    private Coroutine coroutine;

    private Dictionary<GameObject, float> tempPlatformTimers = new Dictionary<GameObject, float>();
    private HashSet<GameObject> tempPlatformsBreaking = new HashSet<GameObject>();

    private const float MOVE_THRESHOLD = 0.1f;
    private const float RELOAD_DELAY = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        platformTimer = platformTimerMax;
        jumpsLeft = maxJumps;
        lastCheckpointPosition = transform.position;
        initialPosition = transform.position;
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        // Inicializa plataformas móviles
        foreach (var mp in movingPlatforms)
        {
            if (mp.platform != null)
            {
                mp.startPos = mp.platform.position;
                mp.endPos = mp.startPos + Vector3.right * mp.moveDistance;
                mp.movingToEnd = true;
            }
        }

        // Vincula eventos UI si los textos están asignados
        if (healthText != null) OnHealthChanged += (v) => healthText.text = "Salud: " + v;
        if (coinsText != null) OnCoinsChanged += (v) => coinsText.text = "Monedas: " + v;
        if (livesText != null) OnLivesChanged += (v) => livesText.text = "Vidas: " + v;
        if (keysText != null) OnKeysChanged += (v) => keysText.text = "Llaves: " + v;

        // Inicializa UI
        OnHealthChanged?.Invoke(health);
        OnCoinsChanged?.Invoke(coins);
        OnLivesChanged?.Invoke(lives);
        OnKeysChanged?.Invoke(keys);

        // Vincula botones auxiliares
        if (respawnButton != null) respawnButton.onClick.AddListener(OnRespawnClicked);
        if (dieButton != null) dieButton.onClick.AddListener(OnDieClicked);
        if (victoryButton != null) victoryButton.onClick.AddListener(OnVictoryClicked);
        if (hurtButton != null) hurtButton.onClick.AddListener(OnHurtClicked);

        // Vincula eventos para mostrar mensajes en infoText
        if (infoText != null)
        {
            OnHealthChanged += (v) => ShowInfo("Salud: " + v);
            OnCoinsChanged += (v) => ShowInfo("Monedas: " + v);
            OnLivesChanged += (v) => ShowInfo("Vidas: " + v);
            OnKeysChanged += (v) => ShowInfo("Llaves: " + v);
            OnAchievementUnlocked += (a) => ShowInfo("Logro: " + a);
        }

        // Inicializa el menú de Game Over
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);

        if (rebindJumpButton != null)
            rebindJumpButton.onClick.AddListener(() => StartRebind("Jump"));

        if (rebindDashButton != null)
            rebindDashButton.onClick.AddListener(() => StartRebind("Dash"));

        UpdateKeyBindingUI();

        if (rb == null)
            Debug.LogWarning("Rigidbody2D not found on Player!");
        if (animator == null)
            Debug.LogWarning("Animator not found on Player!");
        if (audioSource == null)
            Debug.LogWarning("AudioSource not found on Player!");
        if (spriteRenderer == null)
            Debug.LogWarning("SpriteRenderer not found on Player!");
    }

    void Update()
    {
        if (isGameOver)
        {
            // Permite cerrar el juego con Escape en Game Over
            if (Input.GetKeyDown(KeyCode.Escape))
                ExitGame();

            // Permite reiniciar con R en Game Over
            if (Input.GetKeyDown(KeyCode.R))
                RestartGame();

            // Permite reasignar teclas si está esperando input
            if (waitingForRebind)
                ListenForRebind();

            return;
        }

        // Permite reasignar teclas si está esperando input
        if (waitingForRebind)
        {
            ListenForRebind();
            return;
        }

        HandleInput();
        UpdateAnimatorParameters();
        HandlePlatformTimer();
        HandleTempPlatformsTimers();
        CheckWallSlide();
        UpdateMovingPlatforms();
    }

    // --- Botones auxiliares ---
    void OnRespawnClicked()
    {
        // Reubica al jugador en la posición inicial y reinicia variables clave
        transform.position = initialPosition;
        health = 3;
        lives = 3;
        keys = 0;
        coins = 0;
        isGameOver = false;
        enabled = true;
        animator.SetBool("isDead", false);
        OnHealthChanged?.Invoke(health);
        OnLivesChanged?.Invoke(lives);
        OnCoinsChanged?.Invoke(coins);
        OnKeysChanged?.Invoke(keys);
        ShowInfo("¡Respawn manual!");
    }

    void OnDieClicked()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnVictoryClicked()
    {
        // Carga la escena de victoria
        if (!string.IsNullOrEmpty(victorySceneName))
            SceneManager.LoadScene(victorySceneName);
        else
            ShowInfo("No se ha configurado la escena de victoria.");
    }

    void OnHurtClicked()
    {
        health = Mathf.Max(0, health - 1);
        SetState("hurt", hurtClip);
        animator.SetBool("isHurt", true);
        OnHealthChanged?.Invoke(health);
        ShowInfo("¡Daño recibido!");
    }

    void ShowInfo(string msg)
    {
        if (infoText != null)
            infoText.text = msg;
    }

    // --- Movimiento de plataformas móviles ---
    void UpdateMovingPlatforms()
    {
        foreach (var mp in movingPlatforms)
        {
            if (mp.platform == null) continue;
            if (mp.movingToEnd)
            {
                mp.platform.position = Vector3.MoveTowards(mp.platform.position, mp.endPos, mp.moveSpeed * Time.deltaTime);
                if (Vector3.Distance(mp.platform.position, mp.endPos) < 0.01f)
                    mp.movingToEnd = false;
            }
            else
            {
                mp.platform.position = Vector3.MoveTowards(mp.platform.position, mp.startPos, mp.moveSpeed * Time.deltaTime);
                if (Vector3.Distance(mp.platform.position, mp.startPos) < 0.01f)
                    mp.movingToEnd = true;
            }
        }
    }

    void HandleInput()
    {
        float moveInput = Input.GetAxis("Horizontal");

        // --- Flip del sprite según dirección ---
        if (moveInput < -MOVE_THRESHOLD)
            spriteRenderer.flipX = true;
        else if (moveInput > MOVE_THRESHOLD)
            spriteRenderer.flipX = false;

        if (!isDashing)
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (health <= 0)
        {
            SetState("death", deathClip);
            animator.SetBool("isDead", true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SetState("victory", victoryClip);
            animator.SetBool("isVictorious", true);
            return;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SetState("hurt", hurtClip);
            animator.SetBool("isHurt", true);
            pain++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetState("defeat", defeatClip);
            animator.SetBool("isDefeated", true);
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetState("spawn", spawnClip);
            animator.SetBool("isRespawning", true);
            return;
        }

        // Dash (ahora con tecla reasignable)
        if (Input.GetKeyDown(dashKey) && canDash && !isDashing)
        {
            StartCoroutine(Dash(moveInput));
        }

        // Wall Jump
        if (isWallSliding && Input.GetKeyDown(jumpKey))
        {
            rb.linearVelocity = new Vector2(-wallDirX * speed, wallJumpForce);
            SetState("jump", jumpClip);
            animator.SetBool("isJumping", true);
            jumpsLeft = maxJumps - 1;

            // Flip según dirección del wall jump
            if (wallDirX != 0)
                spriteRenderer.flipX = wallDirX > 0 ? false : true;

            return;
        }

        // Movimiento y animaciones principales
        if (isGrounded)
        {
            animator.SetBool("isLanding", false);

            if (Mathf.Abs(moveInput) > MOVE_THRESHOLD)
            {
                if (!isDashing && Input.GetKey(KeyCode.LeftControl))
                {
                    SetState("run", runClip);
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    SetState("walk", walkClip);
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isRunning", false);
                }
            }
            else
            {
                SetState("idle", idleClip);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
            }
        }

        // Doble salto (ahora con tecla reasignable)
        if (jumpsLeft > 0 && Input.GetKeyDown(jumpKey) && !isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            SetState("jump", jumpClip);
            animator.SetBool("isJumping", true);
            jumpsLeft--;

            // Flip según dirección del salto (si se está moviendo)
            if (moveInput < -MOVE_THRESHOLD)
                spriteRenderer.flipX = true;
            else if (moveInput > MOVE_THRESHOLD)
                spriteRenderer.flipX = false;
        }
    }

    void UpdateAnimatorParameters()
    {
        float moveInput = Input.GetAxis("Horizontal");
        animator.SetFloat("velocityX", Mathf.Abs(moveInput));
        animator.SetFloat("velocityY", rb.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDead", health <= 0);
    }

    void HandlePlatformTimer()
    {
        if (!isGrounded)
        {
            platformTimer -= Time.deltaTime;
            if (platformTimer <= 0)
            {
                Die();
            }
        }
        else
        {
            platformTimer = platformTimerMax;
        }
    }

    void HandleTempPlatformsTimers()
    {
        List<GameObject> toBreak = new List<GameObject>();

        foreach (var platform in new List<GameObject>(tempPlatformTimers.Keys))
        {
            if (platform == null) continue;
            tempPlatformTimers[platform] -= Time.deltaTime;
            if (tempPlatformTimers[platform] <= 0 && !tempPlatformsBreaking.Contains(platform))
            {
                toBreak.Add(platform);
            }
        }

        foreach (var platform in toBreak)
        {
            BreakTempPlatform(platform);
            tempPlatformsBreaking.Add(platform);
            if (tempPlatformTimers.ContainsKey(platform))
                tempPlatformTimers.Remove(platform);
        }
    }

    void BreakTempPlatform(GameObject platform)
    {
        Animator platAnim = platform.GetComponent<Animator>();
        if (platAnim != null)
            platAnim.SetTrigger("Break");

        PlayObjectAudio(platform);

        if (breakPlatformClip != null && audioSource != null)
            audioSource.PlayOneShot(breakPlatformClip);

        if (breakEffectPrefab != null)
        {
            Instantiate(breakEffectPrefab, platform.transform.position, Quaternion.identity);
        }

        Destroy(platform, 0.5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollisionOrTrigger(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollisionOrTrigger(other.gameObject);
    }

    private void HandleCollisionOrTrigger(GameObject obj)
    {
        // Plataforma indestructible
        if (obj.CompareTag("Ground") || obj.CompareTag("MovingGround"))
        {
            bool wasGrounded = isGrounded;
            isGrounded = true;
            jumpsLeft = maxJumps;

            if (!wasGrounded)
            {
                animator.SetBool("isLanding", true);
                animator.SetBool("isJumping", false);
                SetState("land", landClip);
            }
        }
        // Plataforma temporal/destructible y móvil
        if (obj.CompareTag("TempPlatform") || obj.CompareTag("MovingTempPlatform"))
        {
            isGrounded = true;
            jumpsLeft = maxJumps;

            if (!tempPlatformsBreaking.Contains(obj))
            {
                tempPlatformTimers[obj] = tempPlatformBreakTime;
            }
        }
        // Moneda
        if (obj.CompareTag("Coin"))
        {
            coins++;
            PlayObjectAudio(obj);
            if (coinClip != null && audioSource != null) audioSource.PlayOneShot(coinClip);
            Destroy(obj);
            OnCoinsChanged?.Invoke(coins);
            UnlockAchievement("Moneda");
        }
        // Enemigo
        if (obj.CompareTag("Enemy"))
        {
            // Rebote sobre enemigo
            if (rb.linearVelocity.y < 0 && transform.position.y > obj.transform.position.y + 0.2f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.8f);
                PlayObjectAudio(obj);
                if (enemyClip != null && audioSource != null) audioSource.PlayOneShot(enemyClip);
                Destroy(obj);
                UnlockAchievement("Enemigo");
            }
            else if (!isInvulnerable)
            {
                pain++;
                health--;
                PlayObjectAudio(obj);
                if (enemyClip != null && audioSource != null) audioSource.PlayOneShot(enemyClip);
                OnHealthChanged?.Invoke(health);
                if (health <= 0)
                {
                    Die();
                }
                else
                {
                    SetState("hurt", hurtClip);
                    animator.SetBool("isHurt", true);
                    StartCoroutine(Invulnerability());
                }
            }
        }
        // Power-ups
        if (obj.CompareTag("PowerUpSpeed"))
        {
            PlayObjectAudio(obj);
            if (powerUpClip != null && audioSource != null) audioSource.PlayOneShot(powerUpClip);
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(SpeedPowerUp(2f, 5f));
            Destroy(obj);
        }
        if (obj.CompareTag("PowerUpJump"))
        {
            PlayObjectAudio(obj);
            if (powerUpClip != null && audioSource != null) audioSource.PlayOneShot(powerUpClip);
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(JumpPowerUp(1.5f, 5f));
            Destroy(obj);
        }
        // Checkpoint
        if (obj.CompareTag("Checkpoint"))
        {
            lastCheckpointPosition = obj.transform.position;
            PlayObjectAudio(obj);
            if (checkpointClip != null && audioSource != null) audioSource.PlayOneShot(checkpointClip);
            UnlockAchievement("Checkpoint");
        }
        // Llave
        if (obj.CompareTag("Key"))
        {
            keys++;
            PlayObjectAudio(obj);
            if (keyClip != null && audioSource != null) audioSource.PlayOneShot(keyClip);
            Destroy(obj);
            OnKeysChanged?.Invoke(keys);
        }
        // Puerta
        if (obj.CompareTag("Door"))
        {
            if (keys > 0)
            {
                keys--;
                PlayObjectAudio(obj);
                if (doorClip != null && audioSource != null) audioSource.PlayOneShot(doorClip);
                OnKeysChanged?.Invoke(keys);
                Destroy(obj);
            }
        }
        // Victoria
        if (obj.CompareTag("Victory"))
        {
            PlayObjectAudio(obj);
            SetState("victory", victoryClip);
            animator.SetBool("isVictorious", true);
            UnlockAchievement("Victoria");
            // Carga la escena de victoria
            if (!string.IsNullOrEmpty(victorySceneName))
                SceneManager.LoadScene(victorySceneName);
            else
                ShowInfo("No se ha configurado la escena de victoria.");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("TempPlatform") ||
            collision.gameObject.CompareTag("MovingGround") ||
            collision.gameObject.CompareTag("MovingTempPlatform"))
        {
            isGrounded = false;
            animator.SetBool("isLanding", false);
        }

        if (collision.gameObject.CompareTag("TempPlatform") || collision.gameObject.CompareTag("MovingTempPlatform"))
        {
            if (tempPlatformTimers.ContainsKey(collision.gameObject))
                tempPlatformTimers.Remove(collision.gameObject);
        }
    }

    void Die()
    {
        SetState("death", deathClip);
        animator.SetBool("isDead", true);
        enabled = false;
        rb.linearVelocity = Vector2.zero;
        lives--;
        OnLivesChanged?.Invoke(lives);

        if (lives > 0)
        {
            StartCoroutine(RespawnAfterDelay(RELOAD_DELAY));
        }
        else
        {
            isGameOver = true;
            SetState("defeat", defeatClip);
            ShowGameOverMenu();
        }
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = lastCheckpointPosition;
        health = 3;
        animator.SetBool("isDead", false);
        enabled = true;
        isGameOver = false;
        OnHealthChanged?.Invoke(health);
    }

    IEnumerator SpeedPowerUp(float multiplier, float duration)
    {
        float originalSpeed = speed;
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }

    IEnumerator JumpPowerUp(float multiplier, float duration)
    {
        float originalJump = jumpForce;
        jumpForce *= multiplier;
        yield return new WaitForSeconds(duration);
        jumpForce = originalJump;
    }

    IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        if (spriteRenderer != null)
        {
            float t = 0f;
            while (t < invulnerableTime)
            {
                spriteRenderer.color = invulnerableColor;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(0.1f);
                t += 0.2f;
            }
            spriteRenderer.color = originalColor;
        }
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }

    IEnumerator Dash(float moveInput)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2((moveInput != 0 ? Mathf.Sign(moveInput) : transform.localScale.x) * dashForce, 0f);
        if (dashClip != null && audioSource != null) audioSource.PlayOneShot(dashClip);
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void CheckWallSlide()
    {
        isTouchingWall = Physics2D.OverlapCircle(transform.position + Vector3.right * 0.5f, 0.1f, wallLayer) ||
                         Physics2D.OverlapCircle(transform.position + Vector3.left * 0.5f, 0.1f, wallLayer);

        wallDirX = (Physics2D.OverlapCircle(transform.position + Vector3.right * 0.5f, 0.1f, wallLayer)) ? 1 :
                   (Physics2D.OverlapCircle(transform.position + Vector3.left * 0.5f, 0.1f, wallLayer)) ? -1 : 0;

        isWallSliding = !isGrounded && isTouchingWall && rb.linearVelocity.y < 0;
        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
    }

    void SetState(string state, AudioClip clip)
    {
        string animName = "Player-" + state;
        if (lastState == animName) return;
        lastState = animName;

        if (animator != null && animator.runtimeAnimatorController != null)
        {
            if (animator.HasState(0, Animator.StringToHash(animName)))
            {
                animator.Play(animName);
            }
        }
        if (audioSource != null && clip != null)
        {
            if (audioSource.clip != clip || !audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    void PlayObjectAudio(GameObject obj)
    {
        AudioSource objAudio = obj.GetComponent<AudioSource>();
        if (objAudio != null && objAudio.clip != null)
        {
            objAudio.Play();
        }
    }

    void UnlockAchievement(string achievement)
    {
        OnAchievementUnlocked?.Invoke(achievement);
        // Aquí puedes mostrar un mensaje en pantalla o guardar el logro
    }

    // --- Game Over Menu ---
    void ShowGameOverMenu()
    {
        isGameOver = true;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void HideGameOverMenu()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // --- Key Rebinding ---
    void StartRebind(string action)
    {
        waitingForRebind = true;
        rebindAction = action;
        ShowInfo("Pulsa una nueva tecla para " + (action == "Jump" ? "SALTAR" : "DASH"));
    }

    void ListenForRebind()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                if (rebindAction == "Jump")
                    jumpKey = key;
                else if (rebindAction == "Dash")
                    dashKey = key;

                waitingForRebind = false;
                rebindAction = "";
                UpdateKeyBindingUI();
                ShowInfo("Tecla reasignada.");
                break;
            }
        }
    }

    void UpdateKeyBindingUI()
    {
        if (jumpKeyText != null)
            jumpKeyText.text = "Salto: " + jumpKey.ToString();
        if (dashKeyText != null)
            dashKeyText.text = "Dash: " + dashKey.ToString();
    }
}