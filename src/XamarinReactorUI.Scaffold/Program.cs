using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Xamarin.Forms;

namespace XamarinReactorUI.Scaffold
{
    public class Program
    {
        public static void Main()
        {
            var _ = new Xamarin.Forms.Shapes.Rectangle();

            var types = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                             // alternative: from domainAssembly in domainAssembly.GetExportedTypes()
                         from assemblyType in domainAssembly.GetTypes()
                         where typeof(BindableObject).IsAssignableFrom(assemblyType)
                         // alternative: where assemblyType.IsSubclassOf(typeof(B))
                         // alternative: && ! assemblyType.IsAbstract
                         select assemblyType)
                .ToDictionary(_ => _.FullName, _ => _);

            foreach (var classNameToGenerate in File.ReadAllLines("WidgetList.txt").Where(_ => !string.IsNullOrWhiteSpace(_)))
            {
                var typeToScaffold = types[classNameToGenerate];

                var outputPath = @"..\XamarinReactorUI";
                Directory.CreateDirectory(outputPath);

                Scaffold(typeToScaffold, outputPath);
            }
        }

        private static void Scaffold(Type typeToScaffold, string outputPath)
        {
            var typeGenerator = new TypeGenerator(typeToScaffold);
            File.WriteAllText(Path.Combine(outputPath, $"Rx{typeToScaffold.Name}.cs"), typeGenerator.TransformText());
        }
    }
}
