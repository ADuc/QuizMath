using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionItem : MonoBehaviour
{
    public TMP_Text questionText;
    public Button leftButton;
    public Button rightButton;

    float moveSpeed;
    float topY;
    bool answered = false;
    bool stopped = false;

    bool correctIsLeft;

    public event Action OnAnsweredCorrect;
    public event Action OnAnsweredWrong;

    public void Setup(MathGenerator.Q q, float speed, float topLimit)
    {
        moveSpeed = speed;
        topY = topLimit;

        if (questionText) questionText.text = q.display;

        correctIsLeft = UnityEngine.Random.Range(0, 2) == 0;
        int wrongAns = MathGenerator.MakeWrongAnswer(q.answer);

        TMP_Text ltxt = leftButton.GetComponentInChildren<TMP_Text>();
        TMP_Text rtxt = rightButton.GetComponentInChildren<TMP_Text>();

        if (correctIsLeft)
        {
            if (ltxt) ltxt.text = q.answer.ToString();
            if (rtxt) rtxt.text = wrongAns.ToString();
            leftButton.onClick.AddListener(() => Choose(true, leftButton));
            rightButton.onClick.AddListener(() => Choose(false, rightButton));
        }
        else
        {
            if (ltxt) ltxt.text = wrongAns.ToString();
            if (rtxt) rtxt.text = q.answer.ToString();
            leftButton.onClick.AddListener(() => Choose(false, leftButton));
            rightButton.onClick.AddListener(() => Choose(true, rightButton));
        }
    }

    void Choose(bool correct, Button pressed)
    {
        if (answered) return;
        answered = true;

        // ✅ Khi trả lời đúng → đổi màu nút
        if (correct)
        {
            pressed.image.color = Color.green; // nút đúng
            GetOtherButton(pressed).image.color = new Color(0.7f, 0.7f, 0.7f); // nút còn lại xám đi
            OnAnsweredCorrect?.Invoke();
        }
        else
        {
            pressed.image.color = Color.red; // nút sai
            OnAnsweredWrong?.Invoke();
        }
    }

    Button GetOtherButton(Button pressed)
    {
        return pressed == leftButton ? rightButton : leftButton;
    }

    void Update()
    {
        if (stopped) return;

        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // ❌ Nếu chưa trả lời mà vượt top thì tính là sai
        if (!answered && transform.localPosition.y > topY)
        {
            answered = true;
            OnAnsweredWrong?.Invoke();
        }
    }

    // 🔸 cho GameManager gọi khi cần dừng tất cả
    public void StopMoving()
    {
        stopped = true;
    }

    // 🔸 cho GameManager cập nhật tốc độ
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}