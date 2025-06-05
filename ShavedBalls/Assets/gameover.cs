using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TypewriterText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string fullText;
    public float typingSpeed = 0.10f;
    public AudioSource keySound;
    public GameObject gamePlay;

    private string currentText = "";

    void Start()
    {
        gamePlay.gameObject.SetActive(false);
        StartCoroutine(TypeText());
        
    }

    IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            currentText += c;
            textComponent.text = currentText;

            if (keySound != null)
                keySound.Play();

            yield return new WaitForSeconds(typingSpeed);
        }

        // Quando terminar de digitar, iniciar animação dos três pontos
        StartCoroutine(AnimateDots());
    }

    IEnumerator AnimateDots()
    {
        string baseText = currentText;
        int dotCount = 0;
         float tempo = 0;

        while (true)
        {

            tempo += 1;

            dotCount = (dotCount + 1) % 4; // 0 a 3 pontos
            textComponent.text = baseText + new string('.', dotCount);
            yield return new WaitForSeconds(0.3f);
            if (tempo >= 3)
            {
                GameRestart();
            }

        }
         
       
    }
     void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
