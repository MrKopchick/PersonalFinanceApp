using PersonalFinanceApp.Models;
using System;
using System.IO;
using System.Text.Json;

namespace PersonalFinanceApp.Services
{
    public static class FileService
    {
        private const string FileName = "user_data.json";

        public static void SaveUser(User user)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(user, options);
                File.WriteAllText(FileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        public static User LoadUser()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    string json = File.ReadAllText(FileName);
                    return JsonSerializer.Deserialize<User>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            return null;
        }
    }
}
