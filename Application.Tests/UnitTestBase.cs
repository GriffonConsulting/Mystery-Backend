using Newtonsoft.Json;

namespace Application.Tests
{
    public class UnitTestBase : IDisposable
    {
        private static readonly Random _random = new();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public static async Task<string> GetTextFromFixture(string fixtureName)
        {
            return await File.ReadAllTextAsync($@"{Directory.GetCurrentDirectory()}\DataFixtures\{fixtureName}");
        }

        public static async Task<(bool isSuccess, T result)> GetFromFixture<T>(string fixtureName)
        {
            var text = await GetTextFromFixture(fixtureName);

            var result = JsonConvert.DeserializeObject<T>(text);

            return (true, result);
        }

        public static string RandomString(int length, string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789") => new(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}