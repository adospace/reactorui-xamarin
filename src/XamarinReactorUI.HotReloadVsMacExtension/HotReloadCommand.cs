﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Mono.Addins;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace XamarinReactorUI.HotReloadVsMacExtension
{
    public class HotReloadCommandHandler : CommandHandler
    {
        private enum OS
        {
            None,

            Android,

            iOS
        }

        protected override async void Run()
        {
            var now = DateTime.Now;
            if (await UpdateInternal())
            {
                LoggingService.LogDebug($"ReactorUI hot reloaded completed in {(DateTime.Now - now).TotalMilliseconds}ms");
            }

            base.Run();
        }

        private async Task<bool> UpdateInternal()
        {

            using (var monitor = IdeApp.Workbench.ProgressMonitors.GetToolOutputProgressMonitor(false))
                    var solution = IdeApp.Workspace.CurrentSelectedSolution;

                    if (solution == null)
                    {
                        return false;
                    }

                    DotNetProject reactorUIProject = null;

                    foreach (var project in solution.GetAllProjects()
                        .OfType<DotNetProject>()
                        .Where(_ => _.TargetFramework.Id.Identifier == ".NETStandard"))
                    {
                        var packages = await project.GetPackageDependencies(ConfigurationSelector.Default, CancellationToken.None);
                        if (packages.Any(_ => _.Name == "XamarinReactorUI") && packages.Any(_ => _.Name == "XamarinReactorUI.HotReload"))
                        {
                            reactorUIProject = project;
                            break;
                        }
                    }

                    if (reactorUIProject == null)
                    {
                        await monitor.Log.WriteLineAsync("Solution doesn't contain a Xamarin Forms project referencing BOTH XamarinReactorUI and XamarinReactorUI.HotReload");
                        return false;
                    }

                    var startupProject = solution.StartupItem as DotNetProject;

                    if (startupProject == null)
                    {
                        await monitor.Log.WriteLineAsync("Solution hasn't a valid startup project");
                        return false;
                    }

                    OS os = OS.None;
                    if (startupProject.TargetFramework.Id.Identifier == "Xamarin.iOS")
                    {
                        os = OS.iOS;
                    }
                    else if (startupProject.TargetFramework.Id.Identifier == "MonoAndroid")
                    {
                        os = OS.Android;
                    }

                    if (os == OS.None)
                    {
                        await monitor.Log.WriteLineAsync($"Unable to find an valid Xamarin Android or iOS project in the solution");
                        return false;
                    }

                    IdeApp.Workbench.SaveAll();

                    var buildResult = await reactorUIProject.Build(monitor, ConfigurationSelector.Default);

                    if (buildResult.Failed)
                    {

                        return false;
                    }

                    if (os == OS.Android)
                    {
                        if (!ExecutePortForwardCommmand(monitor))
                        {
                            return false;
                        }
                    }

                    if (!await SendAssemblyToEmulatorAsync(monitor, reactorUIProject.GetOutputFileName(ConfigurationSelector.Default).FullPath, true))
                    {
                        return false;
                    }

                    return true;
                }
                finally
                {
                    monitor.EndTask();
                }
            }
                    

        }

        private async Task<bool> IsHotReloadAvailable()
        {
            var solution = IdeApp.Workspace.CurrentSelectedSolution;

            if (solution == null)
            {
                return false;
            }

            DotNetProject reactorUIProject = null;

            foreach (var project in solution.GetAllProjects()
                .OfType<DotNetProject>()
                .Where(_ => _.TargetFramework.Id.Identifier == ".NETStandard"))
            {
                var packages = await project.GetPackageDependencies(ConfigurationSelector.Default, CancellationToken.None);
                if (packages.Any(_ => _.Name == "XamarinReactorUI") && packages.Any(_ => _.Name == "XamarinReactorUI.HotReload"))
                {
                    reactorUIProject = project;
                    break;
                }
            }

            if (reactorUIProject == null)
            {
                return false;
            }

            var startupProject = solution.StartupItem as DotNetProject;

            if (startupProject == null)
            {
                return false;
            }

            OS os = OS.None;
            if (startupProject.TargetFramework.Id.Identifier == "Xamarin.iOS")
            {
                os = OS.iOS;
            }
            else if (startupProject.TargetFramework.Id.Identifier == "MonoAndroid")
            {
                os = OS.Android;
            }

            if (os == OS.None)
            {
                return false;
            }

            return true;
        }
    

        protected override async void Update(CommandInfo info)
        {
            info.Enabled = await IsHotReloadAvailable();

            base.Update(info);
        }

        private static bool ExecutePortForwardCommmand(ProgressMonitor progressMonitor)
            process.StartInfo.Arguments = string.Format("-c \"{0}\"", adbCommandLine);
            process.StartInfo.FileName = "/bin/bash";
                //outputPane.OutputString($"{process.StandardOutput.ReadToEnd()}{Environment.NewLine}");
                //outputPane.OutputString($"{process.StandardError.ReadToEnd()}{Environment.NewLine}");
                //outputPane.OutputString($"{ex}{Environment.NewLine}");
                return false;

        private static async Task<bool> SendAssemblyToEmulatorAsync(ProgressMonitor progressMonitor, string assemblyPath, bool debugging)
            var client = new TcpClient

                await networkStream.WriteAsync(assemblyRaw, 0, assemblyRaw.Length);
    }
}