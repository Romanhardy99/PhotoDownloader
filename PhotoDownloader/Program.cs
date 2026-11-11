using System.Text.Json;
using PhotoDownloader;

namespace PhotoDownloader
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string json = File.ReadAllText("db.json");

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Root root = JsonSerializer.Deserialize<Root>(json, options);

            Directory.CreateDirectory("Photos");
            using HttpClient client = new HttpClient();

            IEnumerable<Task> tasks = root.People.Select(person => DownloadPhotoAsync(client, person));
            await Task.WhenAll(tasks);
        }

        static async Task DownloadPhotoAsync(HttpClient client, PersonInfo person)
        {
            string path = Path.Combine("Photos", $"{person.Name}_{person.SurName}.jpg");

            if (File.Exists(path))
            {
                Console.WriteLine($"Файл уже существует: {path}");
                return;
            }

                try
            {
                byte[] data = await client.GetByteArrayAsync(person.Photo);
                await File.WriteAllBytesAsync(path, data);

                Console.WriteLine($"Скачано: {path}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при скачивании фото для {person.Name} {person.SurName}: {ex.Message}");
            }
        }

    }
}
