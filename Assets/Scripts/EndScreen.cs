using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// EndScreen sınıfı, final ekranındaki işlemleri yönetir.
public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText; // Final skorunu gösterecek UI metin bileşeni.
    ScoreKeeper scoreKeeper; // Oyuncunun skorunu tutan sınıf.

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // Sahnedeki ScoreKeeper bileşenini bulur.
    }

    public void ShowFinalScore()
    {
        // Final skorunu hesaplar ve UI'da gösterir.
        finalScoreText.text = "Tebrikler!\n Başarı Oranı : %" + 
                                scoreKeeper.CalculateScore(); // Skor hesaplama metodu çağrılır.
    }
}
