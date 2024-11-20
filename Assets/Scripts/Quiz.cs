using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Quiz sınıfı, bir quiz oyununun mantığını ve UI etkileşimlerini yönetir.
public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText; // Soru metnini gösterecek UI bileşeni.
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>(); // Soruları tutan liste.
    QuestionSO currentQuestion; // Şu anda aktif olan soru.

    [Header("Answers")]
    [SerializeField] GameObject[] answerButton; // Cevap butonlarını tutan dizi.
    int correctAnswerIndex; // Doğru cevabın buton indeksini saklar.
    bool hasAnsweredEarly = true; // Oyuncunun erken cevap verip vermediğini kontrol eder.

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite; // Butonların varsayılan görseli.
    [SerializeField] Sprite correctAnswerSprite; // Doğru cevabın görseli.

    [Header("Timer")]
    [SerializeField] Image timerImage; // Zamanlayıcıyı görselleştiren UI bileşeni.
    Timer timer; // Zamanlayıcı kontrolü için bir nesne.

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText; // Skor metnini gösterecek UI bileşeni.
    ScoreKeeper scoreKeeper; // Oyuncunun skorunu takip eden sınıf.

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar; // Quiz ilerlemesini gösteren çubuk.

    public bool isComplete; // Quiz tamamlandı mı bilgisini tutar.

    void Awake()
    {
        timer = FindObjectOfType<Timer>(); // Sahnedeki Timer nesnesini bulur.
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // Sahnedeki ScoreKeeper nesnesini bulur.
        progressBar.maxValue = questions.Count; // Progress bar'ın maksimum değerini soru sayısına eşitler.
        progressBar.value = 0; // Progress bar başlangıç değerini sıfırlar.
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction; // Zamanlayıcıyı UI üzerinde günceller.

        if (timer.loadNextQuestion) // Zamanlayıcı yeni bir soru yüklenmesi gerektiğini işaretliyorsa.
        {
            if (progressBar.value == progressBar.maxValue) // Tüm sorular tamamlandıysa.
            {
                isComplete = true; // Quiz tamamlandı olarak işaretlenir.
                return;
            }

            hasAnsweredEarly = false; // Oyuncunun cevaplamadığı varsayılır.
            GetNextQuestion(); // Yeni bir soru alır.
            timer.loadNextQuestion = false; // Zamanlayıcıyı sıfırlar.
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) // Zaman dolmuş ve cevap verilmemişse.
        {
            DisplayAnswer(-1); // Yanlış cevap olarak işaretler ve doğru cevabı gösterir.
            setButtonState(false); // Cevap butonlarını devre dışı bırakır.
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true; // Oyuncu cevap verdi olarak işaretlenir.
        DisplayAnswer(index); // Seçilen cevabı kontrol eder ve ekranda gösterir.
        setButtonState(false); // Cevap butonlarını devre dışı bırakır.
        timer.CancelTimer(); // Zamanlayıcıyı durdurur.
        scoreText.text = "Skor : %" + scoreKeeper.CalculateScore(); // Skor metnini günceller.
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex()) // Doğru cevap verilmişse.
        {
            questionText.text = "Tebrikler! Doğru Cevap..."; // Doğru cevap mesajını gösterir.
            buttonImage = answerButton[index].GetComponent<Image>(); // Doğru cevabın butonunu bulur.
            buttonImage.sprite = correctAnswerSprite; // Doğru cevap görselini uygular.
            scoreKeeper.IncrementCorrectAnswers(); // Doğru cevap sayısını artırır.
        }
        else // Yanlış cevap verilmişse.
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex(); // Doğru cevabın indeksini bulur.
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex); // Doğru cevabı alır.
            questionText.text = "Üzgünüm Hatalı Cevap, Doğru Cevap:\n" + correctAnswer; // Yanlış cevap mesajını gösterir.
            buttonImage = answerButton[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>(); // Doğru cevabın butonunu bulur.
            buttonImage.sprite = correctAnswerSprite; // Doğru cevabın görselini uygular.
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0) // Sorular kaldıysa.
        {
            setButtonState(true); // Cevap butonlarını aktif eder.
            setDefaultButtonSprites(); // Butonları varsayılan görsele döndürür.
            GetRandomQuestion(); // Rastgele bir soru seçer.
            DisplayQuestion(); // Seçilen soruyu ekranda gösterir.
            progressBar.value++; // İlerleme çubuğunu bir artırır.
            scoreKeeper.IncrementQuestionSeen(); // Görülen soru sayısını artırır.
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count); // Rastgele bir indeks seçer.
        currentQuestion = questions[index]; // Seçilen soruyu aktif soruya atar.
        questions.Remove(currentQuestion); // Seçilen soruyu listeden kaldırır.
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion(); // Soruyu UI üzerinde gösterir.

        for (int i = 0; i < answerButton.Length; i++) // Tüm cevap butonlarını günceller.
        {
            TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>(); // Butonun metin bileşenini alır.
            buttonText.text = currentQuestion.GetAnswer(i); // Buton metnini cevap ile doldurur.
        }
    }

    void setButtonState(bool state)
    {
        for (int i = 0; i < answerButton.Length; i++) // Tüm butonların durumunu ayarlar.
        {
            Button button = answerButton[i].GetComponent<Button>(); // Butonu bulur.
            button.interactable = state; // Aktif/pasif durumunu belirler.
        }
    }

    void setDefaultButtonSprites()
    {
        for (int i = 0; i < answerButton.Length; i++) // Tüm butonları varsayılan görsele döndürür.
        {
            Image buttonImage = answerButton[i].GetComponent<Image>(); // Butonun görsel bileşenini alır.
            buttonImage.sprite = defaultAnswerSprite; // Varsayılan görseli uygular.
        }
    }
}
