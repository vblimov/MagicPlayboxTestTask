using System;
using System.Collections.Generic;
using System.IO;
using Ingosstrakh.UI.MagicComponents;
using Newtonsoft.Json;
using UnityEngine;

namespace Ingosstrakh.ResourcesLoader
{
    public static class JsonSerializer
    {
        /// <summary>
        ///  Load data from PlayerPrefs. If function return false - use default constructor for serialized object
        ///  Loaded object must have empty constructor and public fields
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool LoadLocal<T> ( string filePath, out T result )
        {
            result = default;

            if ( TryLoad( out result, filePath ) ) return true;

            Debug.Log( $"Load ({filePath}) failed" );

            return false;
        }

        public static void SaveLocal<T> ( string filePath, T data )
        {
            try
            {
                var streamWriter = new StreamWriter($"{Application.persistentDataPath}{filePath}");
                streamWriter.Write(JsonConvert.SerializeObject( data ));
                streamWriter.Close();
            }
            catch ( Exception e )
            {
                Debug.Log( e );
            }
        }

        private static bool TryLoad<T> ( out T data, string filePath )
        {
            data = default;
            try
            {
                var streamReader = new StreamReader($"{Application.persistentDataPath}{filePath}");
                data = JsonConvert.DeserializeObject<T>( streamReader.ReadLine() ?? string.Empty );
                streamReader.Close();
            }
            catch ( Exception e )
            {
                Debug.Log( e );
            }

            return data != null;
        }
    }
}