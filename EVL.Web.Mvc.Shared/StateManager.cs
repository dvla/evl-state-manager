using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Web.Security;
using System.Security.Cryptography;

namespace EVL.Web.Mvc.Shared
{
    public static class StateManager
    {

        private const string purpose = "Purpose For Authentication";

        /// <summary>
        /// Takes an object and turns it into an encrypted, serialised string to be returned to the client as a "viewstate" 
        /// </summary>
        /// <param name="obj">The object you wish to encrypt</param>
        /// <returns>An encrypted value to send to the client</returns>
        public static string GetEncryptedViewState<o>(o obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            string encryptedJson = EncryptText(json);
            return encryptedJson;
        }

        /// <summary>
        /// Takes a string and decrypts and deserialzes it back to an object
        /// </summary>
        /// <param name="viewState">The value from the client</param>
        /// <returns></returns>
        public static T DecryptFromViewState<T>(string viewState)
        {

            if (string.IsNullOrWhiteSpace(viewState)) return default(T);

            try
            {
                string decryptedJson = DecryptText(viewState);
                T obj = JsonConvert.DeserializeObject<T>(decryptedJson);

                return obj;
            }
            catch (CryptographicException cryptoEx)
            {
                throw cryptoEx;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        private static string EncryptText(string valueToEncrypt)
        {
            var unprotectedBytes = Encoding.UTF8.GetBytes(valueToEncrypt);
            var protectedBytes = MachineKey.Protect(unprotectedBytes, purpose);
            var protectedText = Convert.ToBase64String(protectedBytes);

            return protectedText;
        }

        private static string DecryptText(string valueToDecrypt)
        {
            var protectedBytes = Convert.FromBase64String(valueToDecrypt);
            var unprotectedBytes = MachineKey.Unprotect(protectedBytes, purpose);
            var unprotectedText = Encoding.UTF8.GetString(unprotectedBytes);

            return unprotectedText;
        }

    }
}