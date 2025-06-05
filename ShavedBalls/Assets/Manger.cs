using UnityEngine;
using UnityEngine.UI;

public class Manger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button IniciarButton;
    public Canvas MenuInicial;
    public GameObject gameplay;
    void Start()
    {
        IniciarButton.onClick.AddListener(ClickInicial);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ClickInicial()
    {
        MenuInicial.gameObject.SetActive(false);
        gameplay.gameObject.SetActive(true);

    }
    
}
