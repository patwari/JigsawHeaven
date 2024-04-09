/**
This class provides some useful methods to read or write to files
*/
using System.IO;
using UnityEngine;

namespace Utils
{
    public class FileUtils
    {
        private static string ToCompletePath(string file) => $"{Application.persistentDataPath}/{file}";

        public static void Write(string filename, string text) => File.WriteAllText(ToCompletePath(filename), text);

        public static string Read(string filename)
        {
            filename = ToCompletePath(filename);
            if (!File.Exists(filename)) return null;
            return File.ReadAllText(filename);
        }

        public static void DeleteFile(string filename)
        {
            filename = ToCompletePath(filename);
            if (File.Exists(filename)) File.Delete(filename);
        }

        public static bool Exists(string filename)
        {
            filename = ToCompletePath(filename);
            return File.Exists(filename);
        }

        public static void CreateNew(string filename)
        {
            filename = ToCompletePath(filename);
            if (File.Exists(filename)) File.Delete(filename);
            File.Create(filename);
        }
    }
}