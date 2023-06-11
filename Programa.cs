using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Gerenciador de Tarefas\n");
            Console.WriteLine("1. Adicionar Tarefa");
            Console.WriteLine("2. Concluir Tarefa");
            Console.WriteLine("3. Listar Tarefas");
            Console.WriteLine("0. Sair\n");

            Console.Write("Digite sua escolha: ");
            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "0":
                    return;

                case "1":
                    Console.Write("Digite o título da tarefa: ");
                    string titulo = Console.ReadLine();
                    AdicionarTarefa(titulo);
                    Console.WriteLine("Tarefa adicionada com sucesso!\n");
                    break;

                case "2":
                    Console.Write("Digite o ID da tarefa a ser concluída: ");
                    string idTarefa = Console.ReadLine();
                    if (int.TryParse(idTarefa, out int id))
                    {
                        ConcluirTarefa(id);
                        Console.WriteLine("Tarefa marcada como concluída!\n");
                    }
                    else
                    {
                        Console.WriteLine("ID de tarefa inválido. Por favor, tente novamente.\n");
                    }
                    break;

                case "3":
                    Console.WriteLine();
                    ListarTarefas();
                    Console.WriteLine();
                    break;

                default:
                    Console.WriteLine("Escolha inválida. Por favor, tente novamente.\n");
                    break;
            }
        }
    }

    static void AdicionarTarefa(string titulo)
    {
        string comandoPerl = $"perl tasks.pl";
        string argumentos = $"-e \"adicionar_tarefa({{ titulo => '{titulo}', concluida => 0 }})\"";
        ExecutarComandoPerl(comandoPerl, argumentos);
    }

    static void ConcluirTarefa(int idTarefa)
    {
        string comandoPerl = $"perl tasks.pl";
        string argumentos = $"-e \"concluir_tarefa({idTarefa})\"";
        ExecutarComandoPerl(comandoPerl, argumentos);
    }

    static void ListarTarefas()
    {
        string comandoPerl = $"perl tasks.pl";
        string argumentos = $"-e \"listar_tarefas()\"";
        ExecutarComandoPerl(comandoPerl, argumentos);
    }

    static void ExecutarComandoPerl(string comando, string argumentos)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = comando,
            Arguments = argumentos,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process processo = Process.Start(startInfo))
        {
            string resultado = processo.StandardOutput.ReadToEnd();
            Console.WriteLine(resultado);
        }
    }
}
