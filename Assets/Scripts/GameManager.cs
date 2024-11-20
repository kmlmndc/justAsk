using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager sınıfı, oyun akışını ve sahne yönetimini kontrol eder.
public class GameManager : MonoBehaviour
{
    Quiz quiz; // Quiz bileşenine referans.
    EndScreen endScreen; // EndScreen bileşenine referans.

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>(); // Sahnedeki Quiz bileşenini bulur.
        endScreen = FindObjectOfType<EndScreen>(); // Sahnedeki EndScreen bileşenini bulur.
    }

    void Start()
    {
        quiz.gameObject.SetActive(true); // Quiz'i aktif hale getirir.
        endScreen.gameObject.SetActive(false); // EndScreen'i devre dışı bırakır.
    }

    void Update()
    {
        if (quiz.isComplete) // Quiz tamamlandığında kontrol edilir.
        {
            quiz.gameObject.SetActive(false); // Quiz'i devre dışı bırakır.
            endScreen.gameObject.SetActive(true); // EndScreen'i aktif hale getirir.
            endScreen.ShowFinalScore(); // Final skorunu gösterir.
        }
    }

    public void OnReplayLevel()
    {
        // Geçerli sahneyi yeniden yükler.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
