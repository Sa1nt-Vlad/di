﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization.TextHandler
{
    public class TextHandler
    {
        public static Dictionary<string, int> GetWordsFrequencyFromFile(
            string path, HashSet<string> forbiddenWords = null)
        {
            if (!File.Exists(path))
                throw new IOException("File you specified does not exist");

            var text = File.ReadAllText(path);
            return GetWordsFrequency(text, forbiddenWords);
        }

        private static Dictionary<string, int> GetWordsFrequency(string text, HashSet<string> forbiddenWords = null)
        {
            var words = text.Split(new[]
                {
                    ' ', ',', '.', ';', ':',
                    '\n', '\t', '\r',
                    '[', ']',
                    '<', '>',
                    '{', '}',
                    '"', '\'',
                    '(', ')',
                    '+', '-', '=', '/', '*', '\\',
                    '!', '?', '#', '$', '|', '_', '&', '^', '%',
                    '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
                }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Length > 1)
                .Select(word => word.ToLower());
            return GetWordsFrequency(forbiddenWords == null
                ? words
                : words.Where(word => !forbiddenWords.Contains(word)));
        }

        private static Dictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            var wordsFrequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordsFrequency.TryGetValue(word, out var frequency))
                    wordsFrequency[word] = frequency + 1;
                else wordsFrequency[word] = 1;
            }

            return wordsFrequency;
        }
    }
}