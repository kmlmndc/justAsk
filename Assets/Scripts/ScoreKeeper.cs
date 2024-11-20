using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScoreKeeper sınıfı, quizdeki skor durumunu takip eder.
public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0; // Doğru cevap sayısını saklar.
    int questionsSeen = 0;  // Görülen soru sayısını saklar.

    // Doğru cevap sayısını döndürür.
    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    // Doğru cevap sayısını bir artırır.
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    // Görülen soru sayısını döndürür.
    public int GetQuestionSeen()
    {
        return questionsSeen;
    }

    // Görülen soru sayısını bir artırır.
    public void IncrementQuestionSeen()
    {
        questionsSeen++;
    }

    // Skoru yüzde olarak hesaplar ve döndürür.
    public int CalculateScore()
    {
        // Doğru cevapların oranını yüzdelik değer olarak döndürür.
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
