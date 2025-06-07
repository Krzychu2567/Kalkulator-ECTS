using System;
using System.Collections.Generic;
using System.IO;


class Program
{
    static void Main(string[] args)
    {
        Parser parser = new Parser();
        string folderPath = "data";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder {folderPath} nie istnieje.");
            return;
        }

        string[] pliki = Directory.GetFiles(folderPath, "*.txt");
        if (pliki.Length == 0)
        {
            Console.WriteLine($"Brak plików w folderze {folderPath}.");
            return;
        }

        List<Przedmiot> przedmioty = new List<Przedmiot>();

        foreach (string filePath in pliki)
        {
            Console.WriteLine($"Przetwarzanie pliku: {filePath}");
            try
            {
                List<Przedmiot> przedmiotyZPliku = parser.parser(filePath);
                przedmioty.AddRange(przedmiotyZPliku);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas przetwarzania pliku {filePath}: {ex.Message}");
            }

            foreach (Przedmiot przedmiot in przedmioty)
            {
                Console.WriteLine($"Nazwa: {przedmiot.nazwa}, Rodzaj: {przedmiot.rodzaj.nazwa}, ECTS: {przedmiot.ects}, Czy I rok: {przedmiot.czyIrok}");
                Console.WriteLine("Tagi:");
                foreach (Tag tag in przedmiot.tagi)
                {
                    Console.WriteLine($"- {tag.nazwa} ({tag.skr_nazwa})");
                }
                Console.WriteLine($"Semestr: {przedmiot.semestr.nazwa}");
                Console.WriteLine();
            }
        }
    }
}
/*namespace Kalkulator_ECTS;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }    
}*/