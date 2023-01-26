using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ping
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var builder = CreateHostBuilder();
            var host = builder.Build();

            await Run(host);

            return 0;
        }

        private static async Task Run(IHost host)
        {
            CreateCUI();
            while (true)
            {
                int choosenResult = 0;
                if (int.TryParse(Console.ReadLine(), out choosenResult) == false)
                {
                    Console.WriteLine("Неправильный ввод. Повторите снова. Нужно выбрать от 1 - 3. 0 для выхода.");
                    continue;
                }

                if (choosenResult == 0)
                {
                    break;
                }
                using (var serviceScope = host.Services.CreateScope())
                {
                    var services = serviceScope.ServiceProvider;
                    var client = services.GetRequiredService<IMessageClient>();

                    switch (choosenResult)
                    {
                        case 1:
                            await client.AddMessage();
                            Console.WriteLine("\nВыберите вариант: ");
                            break;
                        case 2:
                            await client.GetMessages();
                            Console.WriteLine("\nВыберите вариант: ");
                            break;
                        case 3:
                            await client.DeleteMessage();
                            Console.WriteLine("\nВыберите вариант: ");
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return new HostBuilder()
                .ConfigureAppConfiguration((builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile($"appsettings.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient<IMessageClient, MessageClient>(opt =>
                    {
                        opt.BaseAddress = new Uri(hostContext.Configuration["HttpSettings:MessageServiceBaseAddres"]);
                    });
                }).UseConsoleLifetime();
        }

        private static void CreateCUI()
        {
            Console.WriteLine("Возможности:");
            Console.WriteLine("1. Написать сообщение:");
            Console.WriteLine("2. Получить сообщения для юзера:");
            Console.WriteLine("3. Удалить сообщение по айди сообщения и юзера");
            Console.WriteLine("0. Для выхода. Или кобминацию Ctrl+C");
            Console.WriteLine();
            Console.WriteLine("Выберите вариант: ");
        }

        private static void ClearCUI()
        {
            Console.Clear();
        }
    }
}
