using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace XamarinReactorUI.Scaffold
{
    class Program
    {
        static void Main()
        {
            var typeToScaffold = typeof(Xamarin.Forms.ViewCell);

            var outputPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "gen");
            Directory.CreateDirectory(outputPath);

            Scaffold(typeToScaffold, outputPath);
        }

        private static void Scaffold(Type typeToScaffold, string outputPath)
        {
            var typeGenerator = new TypeGenerator(typeToScaffold);
            File.WriteAllText(Path.Combine(outputPath, $"Rx{typeToScaffold.Name}.cs"), typeGenerator.TransformText());
        }
    }
}
