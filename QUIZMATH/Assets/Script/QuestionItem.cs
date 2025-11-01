// Assets/Scripts/QuestionItem.cs
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class QuestionItem : MonoBehaviour
{
    [Header("UI refs")]
    public TextMeshProUGUI questionText;
    public Button leftButton;
    public Button rightButton;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public Image leftBg;
    public Image rightBg;

    [Header("Colors")]
    public Color defaultBg = new Color(0.95f,0.95f,0.95f);
    public Color correctColor = new Color(0.13f,0.56f,0.99f);
    public Color wrongColor = new Color(0.91f,0.42f,0.38f);

    [HideInInspector] public int correctAnswer;
    [HideInInspector] public float moveSpeed = 160f;
    [HideInInspector] public float topThreshold = 1200f;

    public event Action OnAnsweredCorrect;
    public event Action OnAnsweredWrong;

    RectTransform rt;
    bool answered = false;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        // ensure listeners set (we also set them in Setup to avoid duplicates)
    }

    // Setup called by GameManager after Instantiate
    public void Setup(MathGenerator.Q q, float speed, float topY)
    {
        answered = false;
        moveSpeed = speed;
        topThreshold = topY;

        questionText.text = q.display;
        correctAnswer = q.answer;

        int wrong = MathGenerator.MakeWrongAnswer(correctAnswer);
        bool leftIsCorrect = UnityEngine.Random.value > 0.5f;

        if (leftIsCorrect)
        {
            leftText.text = correctAnswer.ToString();
            rightText.text = wrong.ToString();
        }
        else
        {
            leftText.text = wrong.ToString();
            rightText.text = correctAnswer.ToString();
        }

        ResetUI();

        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        leftButton.onClick.AddListener(() => OnAnswerSelected(true));
        rightButton.onClick.AddListener(() => OnAnswerSelected(false));
    }

    void ResetUI()
    {
        if (leftBg) leftBg.color = defaultBg;
        if (rightBg) rightBg.color = defaultBg;
        if (leftText) leftText.color = Color.black;
        if (rightText) rightText.color = Color.black;
        leftButton.interactable = true;
        rightButton.interactable = true;
    }

    void Update()
    {
        // move item upward in parent's local space
        rt.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;

        if (rt.anchoredPosition.y > topThreshold)
        {
            // destroy when offscreen
            Destroy(gameObject);
        }
    }

    void OnAnswerSelected(bool isLeft)
    {
        if (answered) return;
        answered = true;

        int selected = isLeft ? int.Parse(leftText.text) : int.Parse(rightText.text);
        if (selected == correctAnswer)
        {
            // correct: color blue, award points, keep moving
            if (isLeft) leftBg.color = correctColor; else rightBg.color = correctColor;
            leftButton.interactable = false;
            rightButton.interactable = false;
            OnAnsweredCorrect?.Invoke();
            // optionally destroy after a delay:
            Destroy(gameObject, 0.6f);
        }
        else
        {
            // wrong: show red and show correct, then trigger game over
            if (isLeft) leftBg.color = wrongColor; else rightBg.color = wrongColor;
            // highlight the correct one
            if (int.Parse(leftText.text) == correctAnswer) leftBg.color = correctColor;
            if (int.Parse(rightText.text) == correctAnswer) rightBg.color = correctColor;

            leftButton.interactable = false;
            rightButton.interactable = false;

            // small delay so player sees color
            Invoke(nameof(TriggerWrong), 0.45f);
        }
    }

    void TriggerWrong()
    {
        OnAnsweredWrong?.Invoke();
    }
}