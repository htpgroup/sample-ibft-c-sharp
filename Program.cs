// See https://aka.ms/new-console-template for more information
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Text;
using System.Security;
 

namespace Signature
{
  class Program
  {
    static void Main(string[] args)
    {
      //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Names.txt");
      var dir = Directory.GetCurrentDirectory();
      var publicKeyPath = Path.Combine(dir, "resource\\64875.cer");
     // byte[] publicKey =  ReadFile(publicKeyPath);

      var privateKeyPath = Path.Combine(dir, "resource\\64875Key.pem");
      //byte[] privateKey =  ReadFile(privateKeyPath);
      String hello = "what the hell";
      Console.WriteLine("God only knows what the heck I'm doing");

      var sign = rsaSign(hello, privateKeyPath);
      
      Console.WriteLine(verifySign(hello, publicKeyPath, sign));

    }

 

  static string rsaSign(string data, String privateKeyPath)
    {
      Chilkat.PrivateKey privKey = new Chilkat.PrivateKey();
      bool success = privKey.LoadAnyFormatFile(privateKeyPath,"");

      Chilkat.Rsa rsa = new Chilkat.Rsa();
      rsa.ImportPrivateKeyObj(privKey);
      rsa.EncodingMode = "base64";

      string sigBase64 = rsa.SignStringENC(data,"sha256");
      Console.WriteLine(sigBase64);    
      return sigBase64;
    }

    static bool verifySign(string data, String publicKeyPath, String sigBase64)
    {
      Chilkat.Cert cert = new Chilkat.Cert();
       bool success = cert.LoadFromFile(publicKeyPath);
        Chilkat.PublicKey pubKey =  cert.ExportPublicKey();

        Chilkat.Rsa rsaDecryptor  = new Chilkat.Rsa();
        rsaDecryptor.ImportPublicKey(pubKey.GetXml());
        rsaDecryptor.EncodingMode = "base64";

       return rsaDecryptor.VerifyStringENC(data,"sha256", sigBase64);
    }

   //  static byte[] GetPem(string type, byte[] data)
   // {
     // string pem = Encoding.UTF8.GetString(data);
      //string header = String.Format("-----BEGIN {0}-----\\n", type);
      //string footer = String.Format("-----END {0}-----", type);
      //int start = pem.IndexOf(header) + header.Length;
      //int end = pem.IndexOf(footer, start);
      //string base64 = pem.Substring(start, (end - start));
      //return Convert.FromBase64String(base64);
    //}

    //static RSACryptoServiceProvider LoadCertificateFile(string filename)
   // {
     // using (System.IO.FileStream fs = System.IO.File.OpenRead(filename))
      //{
        //byte[] data = new byte[fs.Length];
        //byte[] res = null;
        //fs.Read(data, 0, data.Length);
       // if (data[0] != 0x30)
        //{
          //res = GetPem("PRIVATE KEY", data);
        //}
        //RSACryptoServiceProvider rsa = DecodeRSAPrivateKey(res);
      //return rsa;
      //}      
    //}


    public static byte[] ReadFile(string filePath)
    {

        using (FileStream stream = File.OpenRead(filePath))
            {
                int totalBytes = (int)stream.Length;
                byte[] bytes = new byte[totalBytes];
                int bytesRead = 0;
    
                while (bytesRead < totalBytes)
                {
                    int len = stream.Read(bytes, bytesRead, totalBytes);
                    bytesRead += len;
                }
    
               return bytes;
            }
        return null;
    }
  }
}