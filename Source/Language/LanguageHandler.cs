using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MultitoolsMod
{
    public class LanguageHandler
    {
        private static readonly Dictionary<string, string> Translations = new Dictionary<string, string>();

        public static void LoadTranslations(string languageCode)
        {
            string filePath = Path.Combine(Main.ModDirectory, $"translations_{languageCode}.txt");

            if (!File.Exists(filePath))
            {
                Debug.Log($"Translation file not found: {filePath}");
                return;
            }

            Translations.Clear();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                string[] parts = line.Split('=');

                if (parts.Length != 2) continue;

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                Translations[key] = value;
            }

            Debug.Log($"Loaded {Translations.Count} translations for language {languageCode}");
        }

        public static string GetTranslation(string key)
        {
            if (Translations.TryGetValue(key, out string value))
            {
                return value;
            }

            return key;
        }
    }
}


