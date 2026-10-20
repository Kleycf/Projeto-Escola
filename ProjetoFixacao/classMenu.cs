using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace ProjetoFixacao;

public class  Menu
{
    private readonly Escola _escola;

    private readonly LogRepository _logRepository;

    private readonly IExchange _exchange;

    public Menu(Escola escola, LogRepository logRepository, IExchange exchange)
    {
        _escola = escola;
        _logRepository = logRepository;
        _exchange = exchange ;
    }

    public void MenuOpcoes()
    {
        Console.WriteLine("#################### Seja bem vindo ao ADMINISTRA ALUNOS ####################");
        Console.WriteLine("");
        Console.WriteLine("  1. Cadastro de aluno.");
        Console.WriteLine("  2. Lista de alunos cadastrados.");
        Console.WriteLine("  3. Avalie um aluno.");
        Console.WriteLine("  4. Busque por um aluno.");
        Console.WriteLine("  5. Histórico de avaliações.");
        Console.WriteLine("  6. Sair.");
    }

    public async Task Exibir()
    {
        bool executando = true;

        while (executando)
        {   
            

             MenuOpcoes();
           
            Console.WriteLine("");

            Console.Write("- Escolha uma das opções acima : ");

            int opcao;

            while (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.Write("Opção invalida, digite apenas o numero da opção desejada!");
                Console.WriteLine("");
            }

            switch (opcao)
            {
                case 1:
                    {
                        string? nome;

                        int idade;

                        Console.Write("- Digite o nome do Aluno que deseja cadastrar: ");

                        nome = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(nome))
                        {
                            Console.WriteLine("Nome invalido!");
                            break;
                        }

                        string nomeValidado = nome;

                        Console.Write($"- Digite a idade do aluno {nomeValidado} : ");

                        if (!int.TryParse(Console.ReadLine(), out idade))
                        {
                            Console.WriteLine("Idade invalida, digite apenas a idade do aluno!");
                        }

                        _escola.AdicionarAluno(nomeValidado, idade);

                        Console.WriteLine($"Aluno {nomeValidado} cadastrado com sucesso!");
                        break;
                    }

                case 2:
                    {
                        var lista = _escola.ObterAlunos();


                        if (lista is null)
                        {
                            Console.WriteLine("Não ha alunos cadastrados até o momento!");
                        }
                        else
                        {
                            foreach (var aluno in lista)
                            {
                                Console.WriteLine($"Aluno {aluno.Value.Nome} {aluno.Value.Idade} anos. ");
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Qual o nome do aluno que deseja avaliar?");

                        string? nome = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(nome))
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Nome invalido!");
                            break;
                        }

                        var aluno = _escola.BuscarAluno(nome);


                        if (aluno == null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Nome não cadastrado!");
                            break;
                        }


                        Console.Write($"De 0 à 10. Digite a nota que gostaria de atribuir ao aluno {nome} : ");

                        int notaValidada;

                        while (!int.TryParse(Console.ReadLine(), out notaValidada) || notaValidada > 10 || notaValidada < 0)
                        {
                            Console.Write("Nota invalida!, Digite uma nota de 0 à 10 : ");
                        }

                        aluno.AdicionarNota(notaValidada);

                        var notificacao = new EventoAvaliacao(aluno.Nome, notaValidada);

                        await _exchange.PublicarAsync("avaliacao", notificacao);

                        Console.WriteLine("Aluno avaliado com sucesso!");

                        break;
                    }

                case 4:
                    {
                        Console.Write("Digite o nome do aluno que deseja buscar: ");

                        string? nome = Console.ReadLine();


                        if (string.IsNullOrWhiteSpace(nome))
                        {
                            Console.WriteLine("Nome invalido!");
                            break;
                        }

                        var aluno = _escola.BuscarAluno(nome);

                        if (aluno == null)
                        {
                            Console.WriteLine("Nome não cadastrado!");
                            break;
                        }
                        else
                        {

                            Console.WriteLine($"- Aluno:{aluno.Nome}");

                            Console.WriteLine($"- Idade:{aluno.Idade}");

                            if (aluno.ObterNotas().Count == 0)
                            {
                                Console.WriteLine($"O aluno {aluno.Nome} ainda não foi avaliado!");
                                break;
                            }

                            Console.WriteLine($"- Notas Atribuidas: " + string.Join(", ", aluno.ObterNotas()));

                            Console.WriteLine($"- Média de Notas: {aluno.CalcularMedia()}");


                        }
                        break;
                    }

                case 5:
                    {
                        var lista = _logRepository.ObterLogs();

                        if (lista is null)
                        {
                            Console.WriteLine("Não ha logs até o momento!");
                        }
                        else
                        {
                            foreach (var list in lista)
                            {
                                Console.WriteLine($" - {list}");
                            }
                        }
                        break;
                    }

                case 6:
                    {
                        Console.WriteLine("Obrigado por utilizar nossos serviços!");
                        executando = false;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Opção invalida, digite apenas o numero da opção desejada!");
                        break;
                    }
            }
        }
    }
}
