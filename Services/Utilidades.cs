﻿using System.Security.Cryptography;
using System.Text;

namespace SysCoco._0.Services
{
    public class Utilidades
    {
        public static string EncriptarClave(string Contraseña)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(Contraseña));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}