using System;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RangeMove : MonoBehaviour
{
     public GameObject gameOver;
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


    public struct AnimationConfig
    {
        public RectTransform rectTransform;
        public CanvasGroup canvasGroup;
        public float duracao;
        public float distancia;
        public bool alphaChange;
    }

    public int pointsContinus = 0;

    public AudioSource audioSource;
    public AudioClip soundEfect;
    public string NomeBrabo = "normal";

    private AnimationConfig lifeConfig;
    private AnimationConfig pointsConfig;

    public float StartFontInicial = 36f;


    void Start()
    {
        lifeConfig = new AnimationConfig
        {
            rectTransform = decressLifeRect,
            canvasGroup = LifeCanvasGroup,
            duracao = 1f,
            distancia = 2f,
            alphaChange = true
        };
        pointsConfig = new AnimationConfig
        {
            rectTransform = PointsRect,
            canvasGroup = PointsCanvasGroup,
            duracao = 0.1f,
            distancia = 10f,
            
        };
        
        

    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = NomeBrabo + System.Environment.NewLine + pointsContinus;
        if (lifes == 0)
        {

            gameOver.gameObject.SetActive(true);
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
                StartCoroutine(Animar(pointsConfig.rectTransform, pointsConfig.canvasGroup, pointsConfig.duracao, pointsConfig.distancia));


            }
            else
            {
                comboText.gameObject.SetActive(false);
                decressLife.gameObject.SetActive(true);
                lifes -= 1;
                LifeCanvasGroup.alpha = 1;
                decressLife.text = "-1";
                StartCoroutine(Animar(lifeConfig.rectTransform, LifeCanvasGroup, lifeConfig.duracao, lifeConfig.distancia, lifeConfig.alphaChange));
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
        if (pointsContinus >= 10 && pointsContinus < 20)
        {
            cor = "verdeClaro";
        }
        else if (pointsContinus >= 20 && pointsContinus <= 29)
        {
            cor = "azulClaro";
        }
        else if (pointsContinus >= 30 && pointsContinus <= 49)
        {
            cor = "amareloClaro";

        }
        else if (pointsContinus >= 50)
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
   
    private System.Collections.IEnumerator Animar(RectTransform rectTransform, CanvasGroup canvasGroup, float duracao, float distancia, bool alphaMode = false, GameObject gameObject = null, float StartFontSize = 36)
    {
        
        Vector2 inicialPos = rectTransform.anchoredPosition;
        Vector2 posDest = inicialPos - new Vector2(0, distancia);
       
        float tempo = 0;
       
        float InicialSize = comboText.fontSize;
        float fontTamanhoMaximo = 79f;

        while (tempo < duracao)
        {

            tempo += Time.deltaTime;
            float t = tempo / duracao;
            rectTransform.anchoredPosition = Vector2.Lerp(inicialPos, posDest, t);

            if (pointsContinus >= 2)
            {

                comboText.gameObject.SetActive(true);
                comboText.fontSize = Mathf.Lerp(InicialSize, fontTamanhoMaximo, t);

            }
            else
            {
                comboText.gameObject.SetActive(false);
                InicialSize = StartFontInicial;
            }


            if (alphaMode)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            }
            yield return null;

        }
        rectTransform.anchoredPosition = inicialPos;
        InicialSize += pointsContinus / 10;
        comboText.fontSize = InicialSize;
        

        if (alphaMode)
        {
            canvasGroup.alpha = 0;

        }
        if (gameObject != null)
        {
            gameObject.SetActive(false);
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
