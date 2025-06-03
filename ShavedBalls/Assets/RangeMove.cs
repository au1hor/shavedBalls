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
    public CanvasGroup LifeCanvasGroup;
    public RectTransform PointsRect;
    public CanvasGroup PointsCanvasGroup;

    public TextMeshProUGUI comboText;
    public RectTransform comboRect;
    public CanvasGroup comboGroup;


    public float duracaoLife = 1f;
    public float distanciaLife = 2f;
    public float duracaoPoints = 1f;
    public float distanciaPoints = 2f;
    public float duracaoCombo = 0.1f;
    public float distanciaCombo = 10f;

    public int pointsContinus = 0;

    public AudioSource audioSource;
    public AudioClip soundEfect;
    public string NomeBrabo = "normal";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = NomeBrabo + System.Environment.NewLine + pointsContinus;
        if (lifes == 0)
        {
            GameRestart();
            return;
        }
        PointsText.text = points.ToString();
        Vector3 euler1 = transform.eulerAngles;
        Vector3 eulerRange = range2.transform.rotation.eulerAngles;
        Vector3 distance = euler1 - eulerRange;
        rotaZ = Mathf.DeltaAngle(euler1.z, eulerRange.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dir *= -1;
            apertou = true;
            speed += dif;
            if (Mathf.Abs(rotaZ) < 20)
            {
                PointsRect.gameObject.SetActive(true);
                NextRange();
                StartCoroutine(Animar(PointsRect,PointsCanvasGroup,duracaoCombo,distanciaCombo,false));
                StartCoroutine(AnimarCombo());

            }
            else
            {
                comboText.gameObject.SetActive(false);
                decressLife.gameObject.SetActive(true);
                lifes -= 1;
                LifeCanvasGroup.alpha = 1;
                decressLife.text = "-1";
                StartCoroutine(Animar(decressLifeRect,LifeCanvasGroup,duracaoLife,distanciaLife,true));
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
        }
        else
        {
            points += 1;
        }

        pointsContinus += 1;
        speedTarget = UnityEngine.Random.Range(0, 100);
        range2.transform.Rotate(0, 0, speedTarget);
        PointsText.text = points.ToString();
        string cor;
        if (pointsContinus > 10 && pointsContinus < 20)
        {
            cor = "verdeClaro";
        }
        else if (pointsContinus > 19 && pointsContinus <= 29)
        {
            cor = "azulClaro";
        }
        else if (pointsContinus >= 30 && pointsContinus < 49)
        {
            cor = "amareloClaro";

        }
        else if (pointsContinus >= 49)
        {
            cor = "vermelho";
        }
        else
        {
            cor = "branco";
        }

        MudarCor(comboText, cor);
        audioSource.PlayOneShot(soundEfect);
    }
    void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /*private System.Collections.IEnumerator AnimarLife()
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
    */
    private System.Collections.IEnumerator Animar(RectTransform rectTransform, CanvasGroup canvasGroup, float duracao, float distancia, bool alphaMode, GameObject gameObject = null)
    {
        Vector2 inicialPos = rectTransform.anchoredPosition;
        Vector2 posDest = inicialPos - new Vector2(0, distancia);
        float tempo = 0;
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracao;
            rectTransform.anchoredPosition = Vector2.Lerp(inicialPos, posDest, t);
            if (alphaMode)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            }
            yield return null;

        }
        rectTransform.anchoredPosition = inicialPos;
        if (alphaMode)
        {
            canvasGroup.alpha = 0;

        }
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }
     }
/*    private System.Collections.IEnumerator AnimarPoints()
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
    */
    private System.Collections.IEnumerator AnimarCombo()

    {

        if (pointsContinus > 1)
        {
            comboText.gameObject.SetActive(true);


            float fontInicialSize = comboText.fontSize;
            float fontTamanhoMaximo = 79f;
            Vector2 inicialPosCombo = comboRect.anchoredPosition;
            Vector2 destinoCombo = inicialPosCombo - new Vector2(distanciaCombo, distanciaCombo);

            float tempo3 = 0;
            while (tempo3 < duracaoCombo)
            {
                tempo3 += Time.deltaTime;
                float t3 = tempo3 / duracaoCombo;
                comboRect.anchoredPosition = Vector2.Lerp(inicialPosCombo, destinoCombo, t3);
                comboText.fontSize = Mathf.Lerp(fontInicialSize, fontTamanhoMaximo, t3);
                yield return null;

            }
            comboText.fontSize = fontInicialSize;

        }

    }
    public Color HexToColor(string Hex)
    {
        Color cor; // declarar a varialvel cor
        if (UnityEngine.ColorUtility.TryParseHtmlString(Hex, out cor)) // função da unity de conversão hex/html sera armazenada na cor
        {
            return cor;
        }
        return Color.white; 
    }

    void MudarCor(  TextMeshProUGUI texto, string Cor)
    {
        
        switch (Cor)
        {
            case "verdeClaro":
                texto.color = HexToColor("#99e599");
                NomeBrabo = "bom";
                break;
            case "azulClaro":
                texto.color = HexToColor("#4b90e3");
                NomeBrabo = "medio";
                break;

            case "amareloClaro":
                texto.color = HexToColor("#e5e971");
                NomeBrabo = "anomalia";
                break;

            case "vermelho":
                texto.color = HexToColor("#913217");
                NomeBrabo = "Galado1010";
                break;

            default:
                texto.color = Color.white;
                break;
        }
     }
}
