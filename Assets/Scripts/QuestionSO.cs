using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CreateAssetMenu, bu ScriptableObject'in Unity menüsünden oluşturulmasını sağlar.
[CreateAssetMenu(fileName = "Quiz Question", menuName = "New Question")]

// QuestionSO, bir soru ve cevapları tutan ScriptableObject'tir.
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)] // Editörde metin kutusunun minimum 2, maksimum 6 satır olmasını sağlar.
    [SerializeField] string question = "Enter your new question here"; // Soru metni.

    [SerializeField] string[] answers = new string[4]; // Cevapları tutan dizi.
    [SerializeField] int correctAnswerIndex; // Doğru cevabın dizideki indeksini tutar.

    // Soruyu döndüren metot.
    public string GetQuestion()
    {
        return question;
    }

    // Belirli bir indeks için cevabı döndüren metot.
    public string GetAnswer(int index)
    {
        return answers[index];
    }

    // Doğru cevabın indeksini döndüren metot.
    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
}
