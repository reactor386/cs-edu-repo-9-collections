//-
using System;
using System.IO;

using System.Reflection;


namespace WorkWithCollections.Utilities;

public static class DirectoryUtil
{
    /// <summary>
    /// Получаем путь до каталога, в который вложен указанный каталог по имени
    /// </summary>
    public static string GetRootForLibrary(string libraryFolderName)
    {
        string rootLocation = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location) ?? string.Empty;

        while (!string.IsNullOrWhiteSpace(rootLocation))
        {
            if (Directory.GetDirectories(rootLocation, libraryFolderName,
                SearchOption.TopDirectoryOnly).Length > 0)
            {
                break;
            }
            rootLocation = Path.GetDirectoryName(rootLocation) ?? string.Empty;
        }

        return rootLocation;
    }
}
