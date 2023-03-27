using System.Text.RegularExpressions;

Console.Title = "Forca";

Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + 38 / 2) + "}", " =================================== "));
Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + 38 / 2) + "}", "|          BEM-VINDO À FORCA        |"));
Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + 38 / 2) + "}", " ===================================\n"));

string[] listaPalavrasInterno = { "ZEUGMA", "AMENDOIM", "CREPUSCULO", "MAGENTA", "MENDACIOSO", "GALAXIA", "PNEUMONIA", "ISTMO", "JOCOSO", "HORRORIZADO", "PIJAMA", "UMBIGO", "LIMONADA", "ESTOJO", "BRANCO", "GELEIA", "AGNOSTICO", "ASTERISCO", "CORACAO", "ELOQUENTE", "ESQUERDA", "MOEDA", "MENTA", "FORCA", "SERENATA", "OTORRINOLARINGOLOGISTA", "TRANSEUNTE", "TRILOGIA", "EMPENHADO", "CORRUPCAO" };
string[] listaPalavrasAdicionada = new string[30];
char[] letras;
char[] tentativas = new char[26];
int modo = 0;
string palavraEscolhida = null, resposta;
Random aleatorio = new Random();

Console.WriteLine("\n ---------------------------------------");
Console.WriteLine("| Escolha um modo de jogo:              |");
Console.WriteLine("| 1 - Lista de Palavras do Sistema      |");
Console.WriteLine("| 2 - Crie uma lista de palavras        |");
Console.WriteLine(" --------------------------------------- \n");

// Seleciona modo do jogo e lida com as exceções (inteiros != 1 ou 2, não inteiros)
while ((modo != 1) && (modo != 2))
{
    Console.Write("\nDigite sua resposta: ");
    bool modoConv = (int.TryParse(Console.ReadLine(), out modo));

    if (modo == 1)
    {
        palavraEscolhida = listaPalavrasInterno[aleatorio.Next(30)]; // Seleciona uma string aleatória dentro da lista palavras do sistema, utilizando o método Random.Next
        Console.Clear();
    }
    else if (modo == 2)
    {
        Console.Clear();
        Console.WriteLine("\nDigite uma lista de palavras para iniciar a forca: ");

        for (int i = 0; i < 30; i++)
        {
            while (listaPalavrasAdicionada[i] == null)
            {
                Console.Write("\n" + (i + 1) + "º palavra (" + (i + 1) + "/30): ");
                resposta = Console.ReadLine();

                // Origina um booleano que informa true caso hajam caracteres fora do formato especificado dentro da string de resposta
                bool existeCaracterEspecial = Regex.IsMatch(resposta, (@"[^a-zA-Z]"));

                if ((resposta == string.Empty) || (resposta == "\n") || (resposta == null) || existeCaracterEspecial)
                {
                    Console.WriteLine("\nFORMATO INCORRETO. Digite sua palavra sem caracteres especiais, números, ou acentos. Tente novamente.");
                }
                else
                {
                    listaPalavrasAdicionada[i] = resposta.ToUpper(); // Deixa todos os caracteres da string de entrada em CapsLock
                }

            }

            int continuar = 0;

            // Permite que usuário selecione quantas palavras deseja adicionar e informa erro em caso de entradas fora do especificado
            while ((continuar != 1) && (continuar != 2))
            {
                Console.Write("\nDeseja adicionar uma nova palavra? [1 - SIM / 2 - NÃO]: ");
                bool continuarConv = (int.TryParse(Console.ReadLine(), out continuar));

                if ((continuar != 1) && (continuar != 2) || !continuarConv)
                    Console.WriteLine("\nVALOR INCORRETO. Digite 1 ou 2.");
                else if ((continuar == 2) && continuarConv)
                    i = 30;
            }
        }

        Console.WriteLine("\nLista de Palavras finalizada! Aperte qualquer tecla para INICIAR O JOGO...");

        for (int i = 0; i < Console.WindowWidth; i++) // Cria uma linha de "-"
        {
            Console.Write("-");
        }

        Console.ReadKey();
        Console.Clear();

        palavraEscolhida = listaPalavrasAdicionada[aleatorio.Next(30)]; // Seleciona uma string aleatória dentro da lista palavras do sistema, utilizando o método Random.Next

        // Como usuário pode inserir quantidade de palavras < 30, impede que valores nulos sejam selecionados
        while (palavraEscolhida == null)
        {
            palavraEscolhida = listaPalavrasAdicionada[aleatorio.Next(30)];
        }
    }
    else if ((modo != 1) && (modo != 2) && modoConv)
        Console.WriteLine("\nVALOR INCORRETO. Tente novamente.");
    else if (modoConv == false)
        Console.WriteLine("\nFORMATO INCORRETO. Digite um número inteiro.");
}


letras = palavraEscolhida.ToCharArray(); // Torna palavra em um array de caracteres. Não exibido no console.

char[] telaResposta = new char[letras.Length]; // Cria um 2º array com tamanho igual ao da palavra escolhida que será exibido no console

for (int i = 0; i < Console.WindowWidth; i++) // Cria uma linha de "-"
{
    Console.Write("-");
}

Console.WriteLine(String.Format("\n\n{0," + ((Console.WindowWidth / 2) + 10 / 2) + "}", "F O R C A \n"));

for (int i = 0; i < Console.WindowWidth; i++) // Cria uma linha de "-"
{
    Console.Write("-");
}

Console.WriteLine("\n\nTente descobrir a palavra abaixo \n\n");

// Insere '_' em todo o array que será exibido e o escreve no console em um única linha
for (int i = 0; i < telaResposta.Length; i++)
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    telaResposta[i] = '_';
    Console.Write(" " + telaResposta[i] + " ");
}

Console.ForegroundColor = ConsoleColor.White;


// Recebe uma letra, salva como char e a compara com os caracteres da palavra selecionada

int n = 1;
bool vencedor = false;
char tentativa;
while (vencedor == false)
{
    Console.Write("\n\n º Digite a " + n + "ª letra: ");

    bool tentativaConv = char.TryParse(Console.ReadLine(), out tentativa);
    string tentativaString = tentativa.ToString();

    /* Origina um booleano que informa true caso hajam caracteres fora do formato especificado dentro de uma string de resposta
	 * Como aqui a entrada era um char, foi feita sua conversão para a variável tentativaString com o método ToString, pois Regex.IsMatch pede uma string*/
    bool existeNumero = Regex.IsMatch(tentativaString, (@"[^a-zA-Z]"));

    while ((tentativaConv == false) || existeNumero)
    {
        Console.WriteLine("\nFORMATO INCORRETO. Digite uma única letra sem acento.");

        Console.WriteLine("\n º Digite a " + n + "ª letra: \n");
        tentativaConv = (char.TryParse(Console.ReadLine(), out tentativa));
        tentativaString = tentativa.ToString();
        existeNumero = Regex.IsMatch(tentativaString, (@"[^a-zA-Z]"));

    }

    Console.Clear();

    for (int i = 0; i < Console.WindowWidth; i++) // Cria uma linha de "-"
    {
        Console.Write("-");
    }

    Console.WriteLine(String.Format("\n\n{0," + ((Console.WindowWidth / 2) + 10 / 2) + "}", "F O R C A \n"));

    for (int i = 0; i < Console.WindowWidth; i++) // Cria uma linha de "-"
    {
        Console.Write("-");
    }
    Console.WriteLine("\n\nTente descobrir a palavra abaixo \n\n");

    char caractere = char.ToUpper(tentativa);
    bool acertou = false;

    /* Retorna letra digitada pelo usuário está ou não presente na palavra da forca.
	 * Também informa caso uma letra já tenha sido usada */
    for (int j = 0; j < n; j++)
    {
        if (caractere == tentativas[j])
        {
            foreach (char t in telaResposta)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" " + t);
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine("\n\n\n ----> Você já tentou a letra ' " + caractere + " '!\n\n");
            j = n;
            n--;
        }


        else if (j == (n - 1))
        {
            tentativas[j] = caractere;

            // Escreve na tela a letras correspondentes da palavra ou '_' para a letras ainda não encontradas
            for (int i = 0; i < letras.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (caractere == letras[i])
                {
                    acertou = true;
                    telaResposta[i] = caractere;
                    Console.Write(" " + telaResposta[i]);
                }
                else
                {
                    Console.Write(" " + telaResposta[i]);
                }
            }

            if (acertou)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n\n\n ----> Parabéns! A palavra tem a letra ' " + caractere + " '!\n\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n\n\n ----> Que pena! A palavra não tem a letra ' " + caractere + " '!\n\n");
            }

        }

        Console.ForegroundColor = ConsoleColor.White;
    }

    n++;

    // Caso a palavra exibida no console corresponda com a palavra correta, retorna true para vencedor
    for (int i = 0; i < telaResposta.Length; i++)
    {
        if (telaResposta[i] == letras[i])
        {
            vencedor = true;
        }
        else
        {
            vencedor = false;
            i = telaResposta.Length;
        }

    }

    // Retorna no console a mensagem de 'Vencedor'
    if (vencedor)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(String.Format("\n\n\n\n\n\n\n{0," + ((Console.WindowWidth / 2) + 40 / 2) + "}", " --------------------------------------- "));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + 40 / 2) + "}", "|               VENCEDOR!               |"));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + 40 / 2) + "}", " --------------------------------------- "));
        int numChar = letras.Length; // conta o número de letras na palavra do jogo

        if (numChar / 2 != 0)
            numChar += 1;

        Console.WriteLine(String.Format("\n\n{0," + ((Console.WindowWidth / 2) + (96 + numChar) / 2) + "}", " ----> Parabéns! Você descobriu que a palavra sorteada era ' " + palavraEscolhida + " '!!! <---- \n\n\n\n\n\n\n\n\n\n\n"));

        Console.ForegroundColor = ConsoleColor.White;
    }

}

