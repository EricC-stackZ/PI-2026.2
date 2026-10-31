using TMPro;
using UnityEngine;

public class InteractionControl : MonoBehaviour
{
    [SerializeField]
    private GameObject todasTelas;
    
    [SerializeField]
    private GameObject telaDoVideo;

    public GameObject npc1, npc2;
    public GameObject tela5;
    public GameObject[] tutorial;

    public TextMeshProUGUI[] textos; private int contagem;
    public ClienteSpawner clienteSpawner;

    public void PassarTutorial()
    {
        if(contagem < tutorial.Length)
        {
            tutorial[contagem].SetActive(false);
        }

        else
        {
            todasTelas.SetActive(false); clienteSpawner.AtivarJogo();
        }

        contagem++;
    }

    public void PularTelaTutorial()
    {
        todasTelas.SetActive(false); clienteSpawner.AtivarJogo();
    }

    public void TelaDialogo1()
    {
        contagem++;

        switch (contagem)
        {
            case 1:

                    textos[0].text = "Boticário viajante? Eu não tinha decidido nenhum nome ainda… Mas foi uma boa ideia.";

                    textos[1].text = "...";

                break;

            case 2:

                    textos[0].text = "A dosagem que passei para o senhor não tinha sido suficiente?";

                break;

            case 3:

                    textos[1].text = "Ela foi sim, minha dor de cabeça passou por um tempo.";

                    textos[0].text = "...";

                break;

            case 4:

                    textos[1].text = "O problema é que minha esposa começou a sentir as mesmas coisas que eu.";

                break;

            case 5:

                    textos[1].text = "Com isso, necessitei te procurar pra que ela pudesse melhorar também.";

                break;

            case 6:

                    textos[0].text = "Certo, desejo melhoras a vocês!";

                    textos[1].text = "...";

                break;

            case 7:

                    textos[1].text = "Obrigado, viajante!";

                    textos[0].text = "...";

                break;

            case 8:

                    npc1.SetActive(false); npc2.SetActive(true);

                    textos[0].text = "Isso deve servir para a sua tosse. Eu… Já não te prescrevi esta medicação antes?";
                
                    textos[1].text = "...";

                break;

            case 9:

                    textos[1].text = "Na sua passagem pela Itália, cheguei a tomar as suas medicações...";

                    textos[0].text = "...";

                break;

            case 10:

                    textos[1].text = "Funcionou por um tempo, mas voltou a piorar.";

                break;

            case 11:

                    textos[0].text = "Pior como?";

                    textos[1].text = "...";

                break;

            case 12:

                    textos[1].text = "Não sei dizer ao certo. Ao menos sei explicar que antes eu conseguia dormir normalmente.";

                    textos[0].text = "...";

                break;

            case 13:

                    textos[1].text = "Pelo menos o seu remédio conseguiu me deixar descansar.";

                break;

            case 14:

                    textos[0].text = "Tente tomar a dose de 12 em 12 horas, deve ser o bastante para melhorar os seus sintomas.";

                    textos[1].text = "...";

                break;

            case 15:

                    textos[1].text = "Obrigada, viajante Remy! Irei seguir as suas instruções.";

                    textos[0].text = "...";

                break;

            case 16:

                todasTelas.SetActive(false); telaDoVideo.SetActive(true);

                break;
        }
    }

    public void PularTelaDialogo1()
    {
        todasTelas.SetActive(false); telaDoVideo.SetActive(true);
    }

    public void TelaDialogo2()
    {
        contagem++;

        switch (contagem)
        {
            case 1:

                    textos[0].text = "Até tinha imaginado, ela está acompanhada de dor de cabeça forte, insônia e enjoos, certo?";

                    textos[1].text = "...";

                break;

            case 2:

                    textos[1].text = "Exatamente! Você tem algo para tratar isso não tem?";

                    textos[0].text = "...";

                break;

            case 3:

                    textos[0].text = "Tenho sim, passei o dia inteiro preparando remédios para isso.";

                    textos[1].text = "...";

                break;

            case 4:

                    textos[1].text = "Obrigado, doutor Remy!";

                    textos[0].text = "...";

                break;

            case 5:

                    textos[0].text = "Doutor? Sinto que ainda não cheguei nesse nível, senhor.";

                    textos[1].text = "...";

                break;

            case 6:

                    textos[1].text = "Não se acanhe rapaz. Com toda essa fama de curandeiro; certamente é um doutor.";

                    textos[0].text = "...";

                break;

            case 7:

                    textos[0].text = "Então certamente agradeço o título.";

                    textos[1].text = "...";

                break;

            case 8:

                    npc1.SetActive(false); npc2.SetActive(true);

                    textos[1].text = "Boticário… Se lembra de mim?";
                
                    textos[0].text = "...";

                break;

            case 9:

                    textos[0].text = "Claro, você veio atrás de mim na Áustria, não foi?";

                    textos[1].text = "...";

                break;

            case 10:

                    textos[0].text = "O que houve, está sentindo os sintomas novamente?";

                break;

            case 11:

                    textos[1].text = "Não… Aquele primeiro remédio realmente foi o bastante.";

                    textos[0].text = "...";

                break;

            case 12:

                    textos[1].text = "Mas minha esposa parece estar ainda pior, eu já não sei o que fazer para ajudá-la.";

                break;

            case 13:

                    textos[0].text = "Entendi, imaginei que você fosse vir aqui e já tinha preparado duas medicações, isso deve bastar para que ela melhore de uma vez.";

                    textos[1].text = "...";

                break;

            case 14:

                    textos[0].text = "Apesar de que se você não está sentindo nada, deve ter melhorado logo no começo, então pode levar apenas uma mesmo.";
                
                break;

            case 15:

                    textos[1].text = "Obrigado, doutor!";

                    textos[0].text = "...";

                break;

            case 16:

                    textos[0].text = "Vou ter que me acostumar com isso...";

                    textos[1].text = "...";

                break;

            case 17:

                    todasTelas.SetActive(false);

                break;
        }
    }

    public void PularTelaDialogo2()
    {
        todasTelas.SetActive(false);
    }

    public void FecharTela5()
    {
        tela5.SetActive(false); telaDoVideo.SetActive(true);
    }
}
