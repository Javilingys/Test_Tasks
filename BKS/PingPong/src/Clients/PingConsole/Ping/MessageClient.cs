using Ping.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ping
{
    public class MessageClient : IMessageClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public MessageClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        public async Task AddMessage()
        {
            var addMessageRequest = GetAddMessageRequestFromConsoleInput();

            var httpResult = await _httpClient.PostAsJsonAsync("/api/messages/add", addMessageRequest, _jsonOptions);

            if (httpResult.IsSuccessStatusCode)
            {
                var response = await httpResult.Content.ReadAsStringAsync();
                Console.WriteLine("PING " + response);
                return;
            }
            else
            {
                Console.WriteLine("Ошибка добавления сообщения.");
                return;
            }
        }

        public async Task DeleteMessage()
        {
            var deleteMessageRequest = GetDeleteMessageRequestFromConsoleInput();

            var httpResult = await _httpClient.PostAsJsonAsync("/api/messages/delete", deleteMessageRequest, _jsonOptions);

            if (httpResult.IsSuccessStatusCode)
            {
                var response = await httpResult.Content.ReadAsStringAsync();
                Console.WriteLine("PING " + response);
                return;
            }
            else
            {
                Console.WriteLine("Ошибка удаления сообщения.");
                return;
            }
        }

        public async Task GetMessages()
        {
            var listRequestDto = GetListMessagesRequestFromConsoleInput();

            var httpResult = await _httpClient.PostAsJsonAsync("/api/messages/list", listRequestDto, _jsonOptions);

            if (httpResult.IsSuccessStatusCode)
            {
                var response = await httpResult.Content.ReadAsStringAsync();
                Console.WriteLine("PING " + response);
                return;
            }
            else
            {
                Console.WriteLine("Ошибка отправки сообщения.");
                return;
            }
        }

        /// <summary>
        /// Построить запрос на добавление сообщения из консоли
        /// </summary>
        private CreatePongMessageRequestDto GetAddMessageRequestFromConsoleInput()
        {
            var messageRequest = new CreatePongMessageRequestDto();

            messageRequest.User = GetUserIdFromConsole();

            Console.Write("Введите сообщение: ");
            messageRequest.Message = Console.ReadLine();

            return messageRequest;
        }

        /// <summary>
        /// Построить запрос на получения списка сообщения из консоли
        /// </summary>
        private ListPongMessagesRequestDto GetListMessagesRequestFromConsoleInput()
        {
            var listRequestDto = new ListPongMessagesRequestDto();

            listRequestDto.User = GetUserIdFromConsole();
            listRequestDto.Id = GetMessageIdFromConsole(true);

            return listRequestDto;
        }

        /// <summary>
        /// Построить запрос на удаление из консоли
        /// </summary>
        private DeletePongMessageRequestDto GetDeleteMessageRequestFromConsoleInput()
        {
            var deleteRequestDto = new DeletePongMessageRequestDto();

            deleteRequestDto.User = GetUserIdFromConsole();
            deleteRequestDto.Id = GetMessageIdFromConsole().Value;

            return deleteRequestDto;
        }

        /// <summary>
        /// Получить айди пользователя из консоли в int
        /// </summary>
        /// <returns></returns>
        private int GetUserIdFromConsole()
        {
            int userId = 1;

            Console.Write("Введите айди пользователя (число): ");
            while (int.TryParse(Console.ReadLine(), out userId) == false)
            {
                Console.Write("Неверно. Введите айди пользователя (число): ");
            }

            return userId;
        }

        /// <summary>
        /// Получить айди сообщения из консоли в форме GUID
        /// </summary>
        /// <param name="optional">может ли GUID быть нулем</param>
        /// <returns>GUID</returns>
        private Guid? GetMessageIdFromConsole(bool optional = false)
        {
            Guid messageId;

            Console.Write("Введите айди сообщения (GUID)");
            if (optional == true)
            {
                Console.Write(", если айди нет, то поставье прочерка \"-\"");
            }
            Console.Write(": ");

            string inputResult = Console.ReadLine().Trim();
            // Если парс введенной строки не удался, то проверяем, если это опциональный параметр (может его не быть в объекте)
            // то возвращаем 0. Если же не опциаональный и парс прошел, то возвращаем GUID
            while (Guid.TryParse(inputResult, out messageId) == false)
            {
                if (optional == true && inputResult == "-")
                {
                    return null;
                }

                Console.Write("Неверный формат. Введите айди сообщения (GUID)");
                if (optional == true)
                {
                    Console.Write(", если айди нет, то поставье прочерка \"-\"");
                }
                Console.Write(": ");
                inputResult = Console.ReadLine().Trim();
            }

            return messageId;
        }
    }
}
