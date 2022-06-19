using System;
using DIO.Series.Classes;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorioSerie = new SerieRepositorio();
        static FilmeRepositorio repositorioFilme = new FilmeRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        InserirFilme();
                        break;
                    case "4":
                        AtualizarSerie();
                        break;
                    case "5":
                        AtualizarFilme();
                        break;
                    case "6":
                        ExcluirSerie();
                        break;
                    case "7":
                        ExcluirFilme();
                        break;
                    case "8":
                        VisualizarSerie();
                        break;
                    case "9":
                        VisualizarFilme();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos seviços.");
            Console.ReadLine();
        }

        private static void ListarSeries(){
            Console.WriteLine("Listar Séries e Filmes\n");

            var series = repositorioSerie.Lista();

            Console.WriteLine("Séries:");
            if(series.Count==0)
                Console.WriteLine("Nenhuma série cadastrada.");

            foreach(var serie in series)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido?"- *Excluido*":""));
            }

            var filmes = repositorioFilme.Lista();

            Console.WriteLine("Filmes:");
            if(filmes.Count==0)
                Console.WriteLine("Nenhum filme cadastrado.");

            foreach(var filme in filmes)
            {
                var excluido = filme.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", filme.retornaId(), filme.retornaTitulo(), (excluido?"- *Excluido*":""));
            }
        }

        //*******SERIES*******//
        private static void DetalheSerie(int index , int fin){
            foreach(int i in Enum.GetValues(typeof(Genero)))
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));

            Console.WriteLine("Digite o gênero estre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano em que a série foi lançada: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: index,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            if(fin==1) repositorioSerie.Insere(novaSerie);
            if(fin==2) repositorioSerie.Atualiza(index, novaSerie);
        }

        private static void InserirSerie(){
            Console.WriteLine("Inserir nova série");

            int indiceSerie = repositorioSerie.ProximoId();

            DetalheSerie(indiceSerie, 1);
        }

        private static void AtualizarSerie()
        {
            Console.Write("Digite o id da série");
            int indiceSerie = int.Parse(Console.ReadLine());

            if(indiceSerie<repositorioSerie.ProximoId())
            {
                var serie = repositorioSerie.RetornaPorId(indiceSerie);
                if(!(serie.retornaExcluido()))
                {
                    DetalheSerie(indiceSerie, 2);
                }
                else
                    Console.WriteLine("Série ja excluida!");
            }
            else
                Console.WriteLine("Id não correspondente!");

        }

        private static void ExcluirSerie()
        {
            Console.Write("Digite o id da série:");
            int indiceSerie = int.Parse(Console.ReadLine());

            if(indiceSerie<repositorioSerie.ProximoId())
            {
                var serie = repositorioSerie.RetornaPorId(indiceSerie);
                if(!(serie.retornaExcluido()))
                    repositorioSerie.Exclui(indiceSerie);
                else
                    Console.WriteLine("Série ja excluida!");
            }
            else
                Console.WriteLine("Id não correspondente!");
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da série:");
            int indiceSerie = int.Parse(Console.ReadLine());

            if(indiceSerie<repositorioSerie.ProximoId())
            {
                var serie = repositorioSerie.RetornaPorId(indiceSerie);

                Console.WriteLine(serie);
            }
            else
                Console.WriteLine("Id não correspondente!");
        }

        //********FILMES*********//
        private static void DetalheFilme(int index, int fin){
            foreach(int i in Enum.GetValues(typeof(Genero)))
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));

            Console.WriteLine("Digite o gênero estre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título do filme: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano em que o filme foi lançado: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição do filme: ");
            string entradaDescricao = Console.ReadLine();

            Filme novoFilme = new Filme(id: index,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            if(fin==1) repositorioFilme.Insere(novoFilme);
            if(fin==2) repositorioFilme.Atualiza(index, novoFilme);
        }

        private static void InserirFilme(){
            Console.WriteLine("Inserir novo filme:");

            int indiceFilme = repositorioFilme.ProximoId();

            DetalheFilme(indiceFilme, 1);
        }

        private static void AtualizarFilme()
        {
            Console.Write("Digite o id do filme: ");
            int indiceFilme = int.Parse(Console.ReadLine());

            if(indiceFilme<repositorioFilme.ProximoId())
            {
                var filme = repositorioFilme.RetornaPorId(indiceFilme);
                if(!(filme.retornaExcluido()))
                {
                    DetalheFilme(indiceFilme, 2);
                }
                else
                    Console.WriteLine("Filme ja excluido!");
            }
            else
                Console.WriteLine("Id não correspondente!");

        }

        private static void ExcluirFilme()
        {
            Console.Write("Digite o id do filme:");
            int indiceFilme = int.Parse(Console.ReadLine());

            if(indiceFilme<repositorioFilme.ProximoId())
            {
                var filme = repositorioFilme.RetornaPorId(indiceFilme);
                if(!(filme.retornaExcluido()))
                    repositorioFilme.Exclui(indiceFilme);
                else
                    Console.WriteLine("Filme ja excluido!");
            }
            else
                Console.WriteLine("Id não correspondente!");
        }

        private static void VisualizarFilme()
        {
            Console.Write("Digite o id do filme:");
            int indiceFilme = int.Parse(Console.ReadLine());

            if(indiceFilme<repositorioFilme.ProximoId())
            {
                var filme = repositorioFilme.RetornaPorId(indiceFilme);

                Console.WriteLine(filme);
            }
            else
                Console.WriteLine("Id não correspondente!");
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries e Filmes a seu dispor");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries e filmes");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Inserir novo filme");
            Console.WriteLine("4- Atualizar série");
            Console.WriteLine("5- Atualizar filme");
            Console.WriteLine("6- Excluir série");
            Console.WriteLine("7- Excluir filme");
            Console.WriteLine("8- Visualizar série");
            Console.WriteLine("9- Visualizar filme");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}