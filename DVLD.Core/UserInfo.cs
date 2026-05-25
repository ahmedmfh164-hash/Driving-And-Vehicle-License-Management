using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Core
{
    public static class clsUserInfo
    {
        private static string _Path = @"SOFTWARE\DVLD";

        public static int UserID { get; set; }
        public static string Username { get; internal set; }
        public static string Password { get; internal set; }
        public static bool isRememberMe { get; internal set; }

        public static void SaveLoginData(string username, string password, bool rememberMe)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(_Path);

            key.SetValue("UserName", username);
            key.SetValue("Password", password);
            key.SetValue("RememberMe", rememberMe);

            Username = username;
            Password = password;
            isRememberMe = rememberMe;
        }

        public static void LoadLoginData()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(_Path);

            if (key != null)
            {
                Username = key.GetValue("UserName")?.ToString();
                Password = key.GetValue("Password")?.ToString();

                if (bool.TryParse(key.GetValue("RememberMe")?.ToString(), out bool remember))
                {
                    isRememberMe = remember;
                }
                else
                {
                    isRememberMe = false;
                }
            }
        }

        public static void DeleteLoginData()
        {
            Registry.CurrentUser.DeleteSubKeyTree(_Path);
        }
    }
}
