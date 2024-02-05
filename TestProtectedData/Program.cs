using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestProtectedData
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "D:\\ProtectedDataTest.txt";
            while (true)
            {

                Console.WriteLine($"Do you want to save (1) or read (2) data in {path}");
                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    Console.WriteLine("What text do you want to save");
                    var textToSave = Console.ReadLine();
                    var bytesToSave = Encoding.ASCII.GetBytes(textToSave);
                    var encrypted = ProtectedData.Protect(bytesToSave, null, DataProtectionScope.CurrentUser);
                    File.WriteAllBytes(path, encrypted);
                    Console.WriteLine($"Succesfully written '{textToSave}' to {path}");
                }

                if (choice == "2")
                {
                    var encryptedBytes = File.ReadAllBytes(path);
                    try
                    {
                        var decryptedBytes =
                            ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
                        Console.WriteLine($"Decrypting from {path}...");
                        var text = Encoding.ASCII.GetString(decryptedBytes);
                        Console.WriteLine(text);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                Console.WriteLine("Want to exit?");
                var exit = Console.ReadLine();
                if (exit == "y")
                {
                    break;
                }
            }
        }
    }
}
