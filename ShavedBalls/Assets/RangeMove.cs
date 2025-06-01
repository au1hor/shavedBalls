using System;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RangeMove : MonoBehaviour
{
    public GameObject range2;
    public TextMeshProUGUI PointsText;
    public Boolean apertou = false;
    public int points = 0;
    public int dir = 1;
    public float speed = 10;
    private float dif = 5;
    private float rotaZ;
    private int lifes = 3;

    public TextMeshProUGUI decressLife;
    public Text lifesTexts;
    private float speedTarget = 30;
    public RectTransform decressLifeRect;
    public CanvasGroup canvasGroup;
    public RectTransform PointsRect;
    public CanvasGroup PointsCanvasGroup;

    public TextMeshProUGUI comboText;
    public RectTransform comboRect;
    public CanvasGroup comboGroup;

    public float duracao = 1f;
    public float duracao2 = 0.1f;
    public float distancia = 2f;
    public float distancia2 = 10f;

    public int pointsContinus = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = "INSANE "+ System.Environment.NewLine + pointsContinus;
        if (lifes == 0)
        {
            GameRestart();
            return;
        }
        PointsText.text = points.ToString();
        Vector3 euler1 = transform.eulerAngles;
        Vector3 eulerRange = range2.transform.rotation.eulerAngles;
        Vector3 distance = euler1 - eulerRange;
        rotaZ = Mathf.DeltaAngle(euler1.z,eulerRange.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dir *= -1;
            apertou = true;
            speed += dif;
            if (Mathf.Abs(rotaZ) < 20)
            {
                PointsRect.gameObject.SetActive(true);
                NextRange();
                StartCoroutine(AnimarPoints());
                StartCoroutine(AnimarCombo());

            }
            else
            {
                 comboText.gameObject.SetActive(false);
                decressLife.gameObject.SetActive(true);
                lifes -= 1;
                canvasGroup.alpha = 1;
                decressLife.text = "-1";
                StartCoroutine(AnimarLife());
                lifesTexts.text = "Vidas =  " + lifes;
                pointsContinus = 0;
            }
        }
        else
        {
            apertou = false;
        }
        transform.Rotate(0, 0, speed * Time.deltaTime * dir);
    }

    void NextRange()
    {
        if (pointsContinus > 1)
        {
            points += 1 * pointsContinus;
        }else
        {
            points += 1;
        }
        
        pointsContinus += 1;
        speedTarget = UnityEngine.Random.Range(0, 100);
        range2.transform.Rotate(0, 0, speedTarget);
        PointsText.text = points.ToString();
    }
    void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private System.Collections.IEnumerator AnimarLife()
    {
        Vector2 posInicial = decressLifeRect.anchoredPosition;
        Vector2 posDest = posInicial - new Vector2(0, distancia);
        float tempo = 0;
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracao;

            decressLifeRect.anchoredPosition = Vector2.Lerp(posInicial, posDest, t);
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
        decressLifeRect.anchoredPosition = posDest;
        canvasGroup.alpha = 0;
        decressLifeRect.anchoredPosition = posInicial;
        decressLife.gameObject.SetActive(false);

    }
    private System.Collections.IEnumerator AnimarPoints()
    {
        Vector2 inicialPos = PointsRect.anchoredPosition;
        Vector2 pointsDir = inicialPos - new Vector2(0, distancia2);
        float tempo2 = 0;

        while (tempo2 < duracao2)
        {
            tempo2 += Time.deltaTime;
            float t2 = tempo2 / duracao2;
            PointsRect.anchoredPosition = Vector2.Lerp(inicialPos, pointsDir, t2);
            PointsText.color = Color.red;
            yield return null;
        }
        PointsRect.anchoredPosition = inicialPos;
    }
    /*
    declarar a posição atual do alvo coo um vector 2d
    declarar o destino do alvo pela diferença entre a posição inicial e a distancia que quer chegar x,y como um vector 2d
    declarara a variavel do tempo
    enquanto o tempo declarado for menor que a  duração 
    aumentar o tempo com o deltatime

    */
    private System.Collections.IEnumerator AnimarCombo()
    {
        
        if (pointsContinus > 1)
        {
            comboText.gameObject.SetActive(true);


            float fontInicialSize = comboText.fontSize;
            float fontTamanhoMaximo = 79f;
            Vector2 inicialPosCombo = comboRect.anchoredPosition;
            Vector2 destinoCombo = inicialPosCombo - new Vector2(distancia2, distancia2);

            float tempo3 = 0;
            while (tempo3 < duracao2)
            {
                tempo3 += Time.deltaTime;
                float t3 = tempo3 / duracao2;
                comboRect.anchoredPosition = Vector2.Lerp(inicialPosCombo, destinoCombo, t3);
                comboText.fontSize = Mathf.Lerp(fontInicialSize, fontTamanhoMaximo, t3);
                yield return null;

            }
            comboText.fontSize = fontInicialSize;

        }
       
    }
}
