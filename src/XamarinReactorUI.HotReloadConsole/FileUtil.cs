using System;
using System.IO;
using System.Threading.Tasks;

namespace XamarinReactorUI.HotReloadConsole
{
    public static class FileUtil
    {
        public static async Task<byte[]> ReadAllFileAsync(string filename)
        {
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                var buff = new byte[file.Length];
                await file.ReadAsync(buff, 0, (int)file.Length);
                return buff;
            }
        }

        public static async Task<string> ReadAllTextFileAsync(string filename)
        {
            using (var reader = File.OpenText(filename))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static async Task SaveAllFileAsync(string filename, byte[] data)
        {
            using (var file = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Write, 4096, true))
            {
                await file.WriteAsync(data, 0, data.Length);
            }
        }

        public static async Task SaveAllTextAsync(string filename, string content)
        {
            using (var file = new StreamWriter(filename))
            {
                await file.WriteAsync(content);
            }
        }
    }
}
