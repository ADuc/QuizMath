using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject questionPrefab;
    public RectTransform gameArea;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;     // ⚠️ assign GameOverPanel in Inspector
    public TMP_Text gameOverText;
    public Button playAgainButton;
    public Button leaderboardButton;

    [Header("Settings")]
    public float spawnInterval = 1f;
    public float moveSpeed = 50f;
    public float addSpeed = 5;
    public int pointsPerCorrect = 10;

    bool running = false;
    int score = 0;
    Coroutine spawnCoroutine;

    void Start()
    {
        ValidateSetup();

        // Setup button listeners
        if (playAgainButton)
            playAgainButton.onClick.AddListener(Restart);
        if (leaderboardButton)
            leaderboardButton.onClick.AddListener(ShowLeaderboard);

        StartGame();
    }

    void ValidateSetup()
    {
        if (questionPrefab == null) Debug.LogError("GameManager: questionPrefab is not assigned!");
        if (gameArea == null) Debug.LogError("GameManager: gameArea is not assigned!");
        if (scoreText == null) Debug.LogError("GameManager: scoreText is not assigned!");
        if (gameOverPanel == null) Debug.LogError("GameManager: gameOverPanel is not assigned!");
        if (gameOverText == null) Debug.LogError("GameManager: gameOverText is not assigned!");
    }

    public void StartGame()
    {
        // Clear existing children
        for (int i = gameArea.childCount - 1; i >= 0; i--)
            Destroy(gameArea.GetChild(i).gameObject);

        running = true;
        score = 0;
        moveSpeed = 50f;
        spawnInterval = 1f;
        UpdateUI();

        if (gameOverPanel) gameOverPanel.SetActive(false);

        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (running)
        {
            SpawnQuestion();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnQuestion()
    {
        spawnInterval = 50f / (moveSpeed * 0.8f);
        moveSpeed += addSpeed;

        if (questionPrefab == null || gameArea == null) return;

        GameObject go = Instantiate(questionPrefab, gameArea);
        RectTransform rt = go.GetComponent<RectTransform>();

        float itemHeight = rt.rect.height;
        float bottomY = -(gameArea.rect.height / 2f) + 10f;
        rt.anchoredPosition = new Vector2(0f, bottomY);

        float topY = (gameArea.rect.height / 2f) + (itemHeight / 2f);

        var q = MathGenerator.GenerateRandom();

        QuestionItem qi = go.GetComponent<QuestionItem>();
        if (qi == null)
        {
            Debug.LogError("SpawnQuestion: prefab missing QuestionItem script!");
            Destroy(go);
            return;
        }

        qi.Setup(q, moveSpeed, topY);
        qi.OnAnsweredCorrect += HandleCorrect;
        qi.OnAnsweredWrong += HandleWrong;

        // 🔹 Đồng bộ tốc độ cho tất cả câu hỏi hiện có
        foreach (Transform child in gameArea)
        {
            var other = child.GetComponent<QuestionItem>();
            if (other != null)
                other.SetSpeed(moveSpeed);
        }
    }

    void HandleCorrect()
    {
        score += pointsPerCorrect;
        UpdateUI();
    }

    void HandleWrong()
    {
        if (!running) return;
        running = false;
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);

        // 🔴 Dừng tất cả câu hỏi
        foreach (Transform child in gameArea)
        {
            QuestionItem qi = child.GetComponent<QuestionItem>();
            if (qi != null)
            {
                qi.StopMoving();
            }
        }

        // 🔴 Hiện panel mờ
        if (gameOverPanel)
        {
            CanvasGroup cg = gameOverPanel.GetComponent<CanvasGroup>();
            if (cg != null) 
                StartCoroutine(FadeInPanel(cg));   // 👈 gọi hiệu ứng mờ dần
            else   
                gameOverPanel.SetActive(true);     // fallback nếu chưa có CanvasGroup
        }

        if (gameOverText)
        {
            gameOverText.text = "Game Over\nScore: " + score;
        }
    }

    void UpdateUI()
    {
        if (scoreText)
            scoreText.text = "Score: " + score;
    }

    public void Restart()
    {
        StartGame();
    }

    void ShowLeaderboard()
    {
        Debug.Log("Show leaderboard (tạm thời chỉ log)");
        // TODO: hiển thị bảng điểm thật sau này
    }
    
    IEnumerator FadeInPanel(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.gameObject.SetActive(true);
        while (cg.alpha < 1)
        {
           cg.alpha += Time.deltaTime;
            yield return null;
        }
    }
}