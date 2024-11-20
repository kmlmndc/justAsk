using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Timer sınıfı, bir sorunun cevaplama süresini ve doğru cevabı gösterme süresini yönetir.
public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f; // Soruyu cevaplama süresi.
    [SerializeField] float timeToShowCorrectAnswer = 10f; // Doğru cevabı gösterme süresi.

    public bool loadNextQuestion; // Yeni soru yüklenip yüklenmeyeceğini işaretler.
    public bool isAnsweringQuestion = false; // Şu anda bir soruya cevap verilip verilmediğini kontrol eder.
    public float fillFraction; // Zamanlayıcının dolma oranını (UI'da) temsil eder.

    float timerValue; // Kalan süreyi saklar.

    void Update()
    {
        UpdateTimer(); // Her karede zamanlayıcıyı günceller.
    }

    // Zamanlayıcıyı sıfırlar (örneğin kullanıcı bir soruyu erken cevapladığında).
    public void CancelTimer()
    {
        timerValue = 0;
    }

    // Zamanlayıcının durumunu ve dolma oranını günceller.
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime; // Zamanlayıcıyı her karede azalan şekilde günceller.

        if (isAnsweringQuestion) // Kullanıcı soruyu cevaplıyorsa.
        {
            if (timerValue > 0) // Süre henüz bitmediyse.
            {
                fillFraction = timerValue / timeToCompleteQuestion; // Kalan süreye göre dolma oranını hesapla.
            }
            else // Süre bittiğinde.
            {
                isAnsweringQuestion = false; // Cevaplama sürecini sonlandır.
                timerValue = timeToShowCorrectAnswer; // Doğru cevabı gösterme süresini başlat.
            }
        }
        else // Doğru cevap gösteriliyorsa.
        {
            if (timerValue > 0) // Süre henüz bitmediyse.
            {
                fillFraction = timerValue / timeToShowCorrectAnswer; // Kalan süreye göre dolma oranını hesapla.
            }
            else // Süre bittiğinde.
            {
                isAnsweringQuestion = true; // Yeni soru için cevaplama sürecini başlat.
                timerValue = timeToCompleteQuestion; // Yeni soru için süreyi başlat.
                loadNextQuestion = true; // Yeni sorunun yüklenmesi gerektiğini işaretle.
            }
        }

        // Debugging için zamanlayıcının durumunu konsola yazdırabilir.
        // Debug.Log(isAnsweringQuestion + ": " + timerValue + " = " + fillFraction);
    }
}
