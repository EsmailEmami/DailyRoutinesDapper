using System;
using System.Security.Cryptography;
using System.Text;

namespace DailyRoutines.Application.Security;

public static class PasswordHelper
{
    public static string EncodePasswordMd5(string password) //Encrypt using MD5   
    {
        //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
        MD5 md5 = new MD5CryptoServiceProvider();
        var originalBytes = Encoding.Default.GetBytes(password);
        var encodedBytes = md5.ComputeHash(originalBytes);
        //Convert encoded bytes back to a 'readable' string    
        return BitConverter.ToString(encodedBytes);

    }
}