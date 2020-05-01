using System;
using System.Linq;
using Mono.Addins;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace XamarinReactorUI.HotReloadVsMacExtension
{
    public class HotReloadCommandHandler : CommandHandler
    {
        protected override void Run()
        {
            TryGetReactorUIProject(out var projectFile);
            base.Run();
        }

        private bool TryGetReactorUIProject(out string projectFile)
        {
            projectFile = null;

            var solution = IdeApp.Workspace.CurrentSelectedSolution;
            if (solution != null)
            {
                foreach (var project in solution.GetAllProjects().OfType<DotNetProject>())
                {
                    Console.WriteLine(project.Name);
                }
            }

            return false;
        }

        protected override void Update(CommandInfo info)
        {
            base.Update(info);
        }
    }
}
