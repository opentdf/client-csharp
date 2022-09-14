using System;
using System.IO;

namespace virtru_tdf_sample
{
    class Program
    {
        static void doNano()
        {
            const string user = "bob_5678";
            const string easUrl = "https://etheria.local/eas";
            const string plaintextFilename = "plaintext.txt";
            const string encryptedFilename = "encrypted.ntdf";
            const string decryptedFilename = "decrypted.txt";

            Console.WriteLine("NanoTDF round-trip");

            if (File.Exists(encryptedFilename)) File.Delete(encryptedFilename);    
            if (File.Exists(decryptedFilename)) File.Delete(decryptedFilename);    

            string plaintext = File.ReadAllText(plaintextFilename);
            Console.WriteLine("Plaintext: " + plaintext);

            try
            {
                NanoTDFClient client = new NanoTDFClient(easUrl, user);

                client.encryptFile(plaintextFilename, encryptedFilename);
         
                client.decryptFile(encryptedFilename, decryptedFilename);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return;
            }

            string decryptedtext = File.ReadAllText(decryptedFilename);
            Console.WriteLine("Decrypted: " + decryptedtext);
        }

        static void doTDF()
        {
            const string easUrl = "https://etheria.local/eas";

            const string plaintextFilename = "plaintext.txt";
            const string encryptedFilename = "encrypted.tdf";
            const string decryptedFilename = "decryptedTDF.txt";

            const string user = "Charlie_1234";
            const string clientKeyFileName = "Charlie_1234.key";
            const string clientCertFileName = "Charlie_1234.crt";
            const string sdkConsumerCertAuthority = "ca.crt";

            Console.WriteLine("TDF round-trip");

            if (File.Exists(encryptedFilename)) File.Delete(encryptedFilename);    
            if (File.Exists(decryptedFilename)) File.Delete(decryptedFilename);    

            string plaintext = File.ReadAllText(plaintextFilename);
            Console.WriteLine("Plaintext: " + plaintext);

            try
            {
                TDFClient client = new TDFClient(easUrl, user, clientKeyFileName, clientCertFileName, sdkConsumerCertAuthority);

                client.encryptFile(plaintextFilename, encryptedFilename);

                client.decryptFile(encryptedFilename, decryptedFilename);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return;
            }

            string decryptedtext = File.ReadAllText(decryptedFilename);
            Console.WriteLine("Decrypted: " + decryptedtext);

        }

        static void doTDFOidc()
        {
            const string plaintextFilename = "plaintext.txt";
            const string encryptedFilename = "encrypted.tdf";
            const string decryptedFilename = "decryptedTDF.txt";

            Console.WriteLine("TDF round-trip");

            if (File.Exists(encryptedFilename)) File.Delete(encryptedFilename);    
            if (File.Exists(decryptedFilename)) File.Delete(decryptedFilename);    

            string plaintext = File.ReadAllText(plaintextFilename);
            Console.WriteLine("Plaintext: " + plaintext);

            try
            {
                OIDCCredentials creds = new OIDCCredentials("neep@yeep.dance", "tdf-client", "123-456", "tdf", "http://localhost:8080");

                TDFClient client = new TDFClient(creds, "http://etheria.local:8000");

                client.encryptFile(plaintextFilename, encryptedFilename);

                client.decryptFile(encryptedFilename, decryptedFilename);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return;
            }

            string decryptedtext = File.ReadAllText(decryptedFilename);
            Console.WriteLine("Decrypted: " + decryptedtext);

        }

        static void Main(string[] args)
        {
            doNano();
            doTDF();
            doTDFOidc();
        }
    }
}
