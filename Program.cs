using System;
using System.IO;
using System.Net.Sockets;

class ZPLPrinter
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Użycie: ZPLPrinter <IP drukarki> <ścieżka do pliku ZPL>");
            return;
        }

        string printerIp = args[0];
        string filePath = args[1];

        // Sprawdzenie, czy plik ZPL istnieje
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Plik ZPL nie istnieje: " + filePath);
            return;
        }

        try
        {
            // Odczytujemy zawartość pliku ZPL
            string zplData = File.ReadAllText(filePath);

            // Ustawienie połączenia z drukarką Zebra
            using (TcpClient client = new TcpClient(printerIp, 9100)) // Standardowy port ZPL II to 9100
            using (NetworkStream stream = client.GetStream())
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // Wysyłanie danych ZPL do drukarki
                writer.Write(zplData);
                writer.Flush(); // Upewnij się, że dane zostały wysłane
                Console.WriteLine("Komendy ZPL zostały wysłane do drukarki.");

                // Możesz dodać tutaj oczekiwanie na potwierdzenie, jeśli chcesz
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd podczas łączenia lub drukowania: " + ex.Message);
        }
    }
}