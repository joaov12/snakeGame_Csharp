using SnakeGameCsharp.enums;
using SnakeGameCsharp.game;

const int larguraTela = 69;
const int alturaTela = 29;
const string caractereCobra = "■";

string[,] tela = new string[larguraTela, alturaTela];
bool jogoRodando = true;
List<Coordenada> coordenadasCobra = new();
Direcao direcao = Direcao.Direita;
int placar = 0;
Random random = new();

IniciarJogo();

void IniciarJogo()
{
    CriarCobra();
    CriarComida();
    LerTeclas();

    while (jogoRodando)
    {
        Thread.Sleep(50);
        TransladarCobra();
        Renderizar();
    }

    FimDeJogo();
}

void FimDeJogo()
{
    Console.Clear();
    Console.WriteLine("Fim de jogo, pontuação: " + placar);
}

void Renderizar()
{
    Console.Clear();
    var telaASerRenderizada = "";

    for (int a = 0; a < alturaTela; a++)
    {
        for (int l = 0; l < larguraTela; l++)
        {
            if (tela[l, a] is not null or " ")
            {
                telaASerRenderizada += tela[l, a];
            }
            else
            {
                telaASerRenderizada += " ";
            }
        }

        telaASerRenderizada += "\n";
    }

    Console.Write(telaASerRenderizada);
}

void TransladarCobra()
{
    var cabeca = coordenadasCobra[0];
    var coordenadaRaboX = coordenadasCobra[^1].X;
    var coordenadaRaboY = coordenadasCobra[^1].Y;

    for (int i = coordenadasCobra.Count - 1; i > 0; i--)
    {
        coordenadasCobra[i].X = coordenadasCobra[i - 1].X;
        coordenadasCobra[i].Y = coordenadasCobra[i - 1].Y;
    }

    if (direcao is Direcao.Direita)
    {
        cabeca.X += 1;

        if (cabeca.X > larguraTela - 1)
        {
            cabeca.X = 0;
        }
    }

    if (direcao is Direcao.Esquerda)
    {
        cabeca.X--;

        if (cabeca.X < 0)
        {
            cabeca.X = larguraTela - 1;
        }
    }

    if (direcao is Direcao.Cima)
    {
        cabeca.Y--;

        if (cabeca.Y < 0)
        {
            cabeca.Y = alturaTela - 1;
        }
    }

    if (direcao is Direcao.Baixo)
    {
        cabeca.Y++;

        if (cabeca.Y > alturaTela - 1)
        {
            cabeca.Y = 0;
        }
    }

    if (tela[cabeca.X, cabeca.Y] == "*")
    {
        placar += random.Next(1, 10);
        coordenadasCobra.Add(new Coordenada(coordenadaRaboX, coordenadaRaboY));
        CriarComida();
    }

    if (tela[cabeca.X, cabeca.Y] == caractereCobra)
    {
        jogoRodando = false;
        return;
    }

    AtualizarPosicaoCobra();
}

void LerTeclas()
{
    Thread task = new(LerAcaoDaTecla);
    task.Start();
}

void LerAcaoDaTecla()
{
    while (jogoRodando)
    {
        var tecla = Console.ReadKey();

        if (tecla.Key is ConsoleKey.UpArrow && direcao is not Direcao.Baixo)
        {
            direcao = Direcao.Cima;
        }

        if (tecla.Key is ConsoleKey.DownArrow && direcao is not Direcao.Cima)
        {
            direcao = Direcao.Baixo;
        }

        if (tecla.Key is ConsoleKey.LeftArrow && direcao is not Direcao.Direita)
        {
            direcao = Direcao.Esquerda;
        }

        if (tecla.Key is ConsoleKey.RightArrow && direcao is not Direcao.Esquerda)
        {
            direcao = Direcao.Direita;
        }
    }
}

void CriarComida()
{
    int aleatorioX, aleatorioY;

    do
    {
        aleatorioX = random.Next(0, larguraTela);
        aleatorioY = random.Next(0, alturaTela);
    } while (tela[aleatorioX, aleatorioY] is not null or " ");

    tela[aleatorioX, aleatorioY] = "*";
}

void CriarCobra()
{
    coordenadasCobra.Add(new Coordenada(9, 14));
    coordenadasCobra.Add(new Coordenada(8, 14));
    coordenadasCobra.Add(new Coordenada(7, 14));

    AtualizarPosicaoCobra();
}

void AtualizarPosicaoCobra()
{
    for (int l = 0; l < larguraTela; l++)
    {
        for (int a = 0; a < alturaTela; a++)
        {
            var posicaoDeveConterCobra = coordenadasCobra.Any(coordenada => coordenada.X == l && coordenada.Y == a);

            if (posicaoDeveConterCobra)
            {
                tela[l, a] = caractereCobra;
                continue;
            }

            if (tela[l, a] == caractereCobra)
            {
                tela[l, a] = " ";
            }
        }
    }
}