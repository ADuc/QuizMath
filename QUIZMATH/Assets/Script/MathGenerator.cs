// Assets/Scripts/MathGenerator.cs
using UnityEngine;

public static class MathGenerator
{
    public struct Q { public string display; public int answer; }

    public static Q GenerateRandom()
    {
        string[] ops = { "+", "-", "×", "÷" };
        string op = ops[Random.Range(0, ops.Length)];

        int a = 0, b = 1, ans = 0;
        if (op == "+")
        {
            a = Random.Range(1, 50);
            b = Random.Range(1, 50);
            ans = a + b;
        }
        else if (op == "-")
        {
            a = Random.Range(1, 50);
            b = Random.Range(1, a + 1);
            ans = a - b;
        }
        else if (op == "×")
        {
            a = Random.Range(1, 13);
            b = Random.Range(1, 13);
            ans = a * b;
        }
        else // ÷
        {
            b = Random.Range(1, 12);
            ans = Random.Range(1, 12);
            a = b * ans; // ensure integer division
        }
        return new Q { display = $"{a,3} {op} {b,-3} =", answer = ans };
        //return new Q { display = $"{a} {op} {b} =", answer = ans };
        
    }

    public static int MakeWrongAnswer(int correct)
    {
        int delta = Random.Range(1, 6);
        if (Random.value < 0.5f) delta = -delta;
        int wrong = correct + delta;
        if (wrong == correct || wrong < 0) wrong = correct + Mathf.Max(1, Mathf.Abs(delta) + 1);
        return wrong;
    }
}