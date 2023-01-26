using Microsoft.EntityFrameworkCore;
using Pong.API.Infrastructure;

namespace Pong.API.Helpers
{
    public sealed class MigrateDatabaseHelper
    {
        private MigrateDatabaseHelper() { }

        /// <summary>
        /// Проверяет подключение к базе данных какое-то время, и если может подключиться, то применяет все миграции к бд
        /// </summary>
        public static async Task MigrateDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var logger = loggerFactory.CreateLogger<MigrateDatabaseHelper>();
                var context = services.GetRequiredService<PongDbContext>();

                // Retry механизм. Проверяет подключение к бд 5 раз
                // если подключение к бд установлено - применяются миграции и завершается цикл
                int retryTimeInMilliseconds = 500;
                for (int attempt = 0; attempt < 5; attempt++)
                {
                    if (await context.Database.CanConnectAsync())
                    {
                        logger.LogInformation("Применение миграций к базе данных со след. строкой подключения: {connectionString}",
                            app.Configuration.GetConnectionString("SqlServerConnection"));

                        await context.Database.MigrateAsync();

                        logger.LogInformation("Миграция прошла успешно");
                        break;
                    };

                    await Task.Delay(retryTimeInMilliseconds);
                    // / 3 - Просто рендомное магическое число
                    retryTimeInMilliseconds += (int)(retryTimeInMilliseconds / 3);
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<MigrateDatabaseHelper>();
                logger.LogError("Ошибка создания бд", ex);
            }
        }
    }
}
