﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


public class HashSalt
{

    //Lav et tilfældigt indkodningsord
    public static string GetRandomSalt(Int32 size = 12)
    {
        //Vælg noget tilfældigt
        var random = new RNGCryptoServiceProvider();
        //Vælg en længde af cifre
        var salt = new byte[size];
        //Sæt det tilfældige ind i længden af cifre
        random.GetBytes(salt);
        //Ændr den tilfældige ciffer-kombination til en string
        return Convert.ToBase64String(salt);
    }

    //Kryptér kodeordet med indkodningsordet
    public static string HashPassword(string password, string salt)
    {
        //læg kodeordet og indkodningsordet sammen
        var combinedPassword = string.Concat(password, salt);
        var sha256 = new SHA256Managed();
        var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

}
