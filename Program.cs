using SnakeGameCsharp.enums;
using SnakeGameCsharp.game;
using System;


const int larguraTela = 69;
const int alturaTela = 29;
const string caractereCobra = "+=+";

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
    throw new NotImplementedException();
}

void Renderizar()
{
    throw new NotImplementedException();
}

void TransladarCobra()
{
    throw new NotImplementedException();
}

void LerTeclas()
{
    throw new NotImplementedException();
}

void CriarComida()
{
    throw new NotImplementedException();
}

void CriarCobra()
{
    coordenadasCobra.Add(new Coordenada(7,14));
    coordenadasCobra.Add(new Coordenada(7, 14));
    coordenadasCobra.Add(new Coordenada(7, 14));

    AtualizarPosicaoCobra();

}

void AtualizarPosicaoCobra()
{
    for(int l = 0; l < larguraTela; l++)
    {
        for(int a = 0; a < alturaTela; a++)
        {
            var posicaoDeveConterCobra = coordenadasCobra.Any(coordenada => coordenada.X == l && coordenada.Y == a);

            if (posicaoDeveConterCobra)
            {
                tela[l, a] = caractereCobra;
                continue;
            }

            if (tela[l,a] == caractereCobra)
            {
                tela[l, a] = " ";
            }
        }
    }
}