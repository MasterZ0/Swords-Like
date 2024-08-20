using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreditManager : MonoBehaviour
{
    [System.Serializable]
    public class Credit
    {
        public string nome;
        public Sprite icone;
        [TextArea] public string funcoes;  // Isso permite múltiplas linhas de texto
        public string portfolioLink;
    }

    public List<Credit> credits = new List<Credit>();  // Lista visível no Inspector
    public GameObject creditCardPrefab;                // Prefab do card
    public Transform gridParent;                       // Pai dos cards (CreditsGrid)

    void Start()
    {
        PopulateCredits();
    }

    void PopulateCredits()
    {
        foreach (Credit credit in credits)
        {
            // Instancia o Prefab do card
            GameObject newCard = Instantiate(creditCardPrefab, gridParent);

            // Preenche os campos do card
            Text nomeText = newCard.transform.Find("Nome").GetComponent<Text>();
            Image iconeImage = newCard.transform.Find("Icone").GetComponent<Image>();
            Text funcoesText = newCard.transform.Find("Funcoes").GetComponent<Text>();
            Button portfolioButton = newCard.transform.Find("PortfolioButton")?.GetComponent<Button>();

            // Verifica se os componentes foram encontrados
            if (nomeText == null || iconeImage == null || funcoesText == null || portfolioButton == null)
            {
                Debug.LogError("Erro: um dos elementos não foi encontrado no Prefab.");
                continue;
            }

            // Preenche os campos do card
            nomeText.text = credit.nome;
            iconeImage.sprite = credit.icone;
            funcoesText.text = credit.funcoes;

            // Configura o botão para abrir o link
            portfolioButton.onClick.AddListener(() => OpenPortfolio(credit.portfolioLink));

            void OpenPortfolio(string url)
            {
                if (!string.IsNullOrEmpty(url))
                {
                    Application.OpenURL(url);  // Abre o link no navegador
                }
            }
        }
    }
}