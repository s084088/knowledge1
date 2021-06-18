using Newtonsoft.Json;
using System;
using System.IO;

namespace Util.Helper
{
    public static class JsonConfigHelper
    {
        public static bool SaveContractToJSON<T>(T contract, string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (var fs = File.Open(filePath, FileMode.OpenOrCreate))
                {
                    SerializeToStream(contract, fs);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void SaveContractToJSON<T>(T contract, MemoryStream stream)
        {
            SerializeToStream(contract, stream);
        }

        private static void SerializeToStream<T>(T contract, Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(stream))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, contract);
            }
        }

        public static T LoadContractFromJSON<T>(string filePath)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    return DeserializeFromStream<T>(fileStream);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static T LoadContractFromJSON<T>(MemoryStream stream)
        {
            try
            {
                return DeserializeFromStream<T>(stream);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        private static T DeserializeFromStream<T>(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }
    }
}