
using System.IO;

namespace SaveSystem {

    public static class FileManager {

        public static void WriteToFile(string _data, string _path) {

            if(File.Exists(_path)) {
                File.Delete(_path);
            }

            using(FileStream fileStream = File.Create(_path)) {
                using(StreamWriter writer = new StreamWriter(fileStream)) {
                    writer.Write(_data);
                }
            }

        }

        public static string ReadFromFile(string _path) {

            using(FileStream fileStream = File.OpenRead(_path)) {
                using(StreamReader reader = new StreamReader(fileStream)) {
                    return reader.ReadToEnd();
                }
            }

        }

    }

}