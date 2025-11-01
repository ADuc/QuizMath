// Assets/Scripts/GameManager.cs
using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject questionPrefab;         // assign QuestionItem prefab
    public RectTransform gameArea;            // assign GameArea RectTransform (the visible viewport)
    public TMP_Text scoreText;                // assign Score Text (TextMeshPro)
    public TMP_Text gameOverText;             // assign GameOver Text (hidden at start)

    [Header("Settings")]
    public float spawnInterval = 1.2f;
    public float moveSpeed = 200f;
    public float addSpeed = 5;
    public int pointsPerCorrect = 10;

    bool running = false;
    int score = 0;
    Coroutine spawnCoroutine;

    void Start()
    {
        ValidateSetup();
        StartGame();
    }

    void ValidateSetup()
    {
        if (questionPrefab == null) Debug.LogError("GameManager: questionPrefab is not assigned!");
        if (gameArea == null) Debug.LogError("GameManager: gameArea is not assigned!");
        if (scoreText == null) Debug.LogError("GameManager: scoreText is not assigned!");
        if (gameOverText == null) Debug.LogError("GameManager: gameOverText is not assigned!");
    }

    public void StartGame()
    {

        // clear existing children
        for (int i = gameArea.childCount - 1; i >= 0; i--) Destroy(gameArea.GetChild(i).gameObject);

        running = true;
        score = 0;
        UpdateUI();
        if (gameOverText) gameOverText.gameObject.SetActive(false);

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
        
        spawnInterval = 160f/ (moveSpeed *0.8f);
        moveSpeed = moveSpeed + addSpeed; 

        if (questionPrefab == null || gameArea == null) return;

        GameObject go = Instantiate(questionPrefab, gameArea);
        RectTransform rt = go.GetComponent<RectTransform>();

        // place at bottom inside gameArea
        float itemHeight = rt.rect.height;
        float bottomY = -(gameArea.rect.height / 2f) + 10f  ; // +10 padding
        rt.anchoredPosition = new Vector2(0f, bottomY);

        // compute top threshold
        float topY = (gameArea.rect.height / 2f) + (itemHeight / 2f);

        // generate question
        var q = MathGenerator.GenerateRandom();

        // get component and setup
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
    }

    void HandleCorrect()
    {
        score += pointsPerCorrect;
        UpdateUI();
    }

    void HandleWrong()
    {
        running = false;
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);

        if (gameOverText)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over\nScore: " + score;
        }

        // optional: clear remaining spawned items
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
    }

    // optional restart method hooked to UI button
    public void Restart()
    {
        StartGame();
    }
}