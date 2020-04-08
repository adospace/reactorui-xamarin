﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace XamarinReactorUI.HotReloadVsExtension
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ReloadCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("a0330769-c00a-40d7-9ac6-d2135980e1f4");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private ReloadCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ReloadCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        private EnvDTE.DTE _dte;
        private IVsOutputWindow _outputWindow;

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in ReloadCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ReloadCommand(package, commandService);

            if (!(await package.GetServiceAsync(typeof(EnvDTE.DTE)) is EnvDTE.DTE dte))
                return;

            if (!(await package.GetServiceAsync(typeof(SVsOutputWindow)) is IVsOutputWindow outputWindow))
                return;

            //IVsOutputWindow outWindow = package.GetServiceAsync(typeof(SVsOutputWindow)) as IVsOutputWindow;

            Instance._dte = dte;
            Instance._outputWindow = outputWindow;
        }

        private IReadOnlyList<Project> GetFormsProjects()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var projectsFound = new List<Project>();
            
            /*
TargetFrameworkMoniker=.NETStandard,Version=v2.0
TargetFramework=131072
            */


            var allProjectsInSolution = _dte.Solution.Projects;
            foreach (var project in allProjectsInSolution.Cast<Project>())
            {
                var targetFrameworkIsNetStandard = project.Properties.Cast<Property>().Any(_ => _.Name == "TargetFrameworkMoniker" && _.Value.ToString().IndexOf(".NETStandard", StringComparison.OrdinalIgnoreCase) > -1);
                if (!targetFrameworkIsNetStandard)
                    continue;

                //var optimizeIsEnabled = project.ConfigurationManager.ActiveConfiguration.Properties.Cast<Property>().Any(_ => _.Name == "Optimize" && (bool)_.Value);
                //if (optimizeIsEnabled) //i.e. is in release mode
                //    continue;


                //bool optimizeIsEnabled = false;
                //foreach (Property property in project.ConfigurationManager.ActiveConfiguration.Properties)
                //{
                //    try
                //    {
                //        System.Diagnostics.Debug.WriteLine($"{property.Name}={property.Value}");
                //    }
                //    catch (Exception ex)
                //    {
                //        System.Diagnostics.Debug.WriteLine($"{property.Name}={ex.Message}");
                //    }
                //    //if (prop.Name == "Optimize")
                //    //{
                //    //    optimizeIsEnabled = (bool)property.Value;
                //    //    break;
                //    //}
                //}

                //if (optimizeIsEnabled) //i.e. is in release mode
                //    continue;

                var vsproject = project.Object as VSLangProj.VSProject;
                
                var referenceReactorUIHotReloadPackage = vsproject.References.Cast<VSLangProj.Reference>().Any(_ => _.Name == "XamarinReactorUI.HotReload");
                if (!referenceReactorUIHotReloadPackage)
                    continue;

                projectsFound.Add(project);
            }

            return projectsFound;

            //System.Diagnostics.Debug.WriteLine($"Project={project.UniqueName} Properties");
            //foreach (var property in )
            //{
            //    try
            //    {
            //        System.Diagnostics.Debug.WriteLine($"{property.Name}={property.Value}");
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"{property.Name}={ex.Message}");
            //    }
            //}

            //System.Diagnostics.Debug.WriteLine($"Project={project.UniqueName} References");

            //var vsproject = project.Object as VSLangProj.VSProject;
            //foreach (VSLangProj.Reference reference in vsproject.References)
            //{
            //    //if (reference.SourceProject == null)
            //    //{
            //    //    System.Diagnostics.Debug.WriteLine(reference.Name);

            //    //}
            //    //else
            //    //{
            //    //    // This is a project reference
            //    //    System.Diagnostics.Debug.WriteLine(reference.Name);
            //    //}

            //    //if (reference.Name == "XamarinReactorUI.HotReload")
            //    //    return project;

            //    System.Diagnostics.Debug.WriteLine($"{reference.Name}({reference.SourceProject == null})");
            //}
            //}

            //return projectsFound;
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var generalPane = GetVsOutputWindow();

            var selectedProject = GetFormsProjects().FirstOrDefault();

            if (selectedProject == null)
            {
                generalPane.OutputString($"Solution doesn't contain a valid ReactorUI hot reload project (ensure it references XamarinReactorUI.HotReload package and call WithHotReload() on RxApplication)...{Environment.NewLine}");
                generalPane.Activate(); // Brings this pane into view
                return;
            }

            string projectPath = selectedProject.FullName;

            var outputFilePath = Path.Combine(Path.GetDirectoryName(projectPath), 
                selectedProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString(),
                selectedProject.Properties.Item("OutputFileName").Value.ToString());

            if (Path.GetExtension(outputFilePath) != ".dll")
                return;

            generalPane.Activate(); // Brings this pane into view
            generalPane.OutputString($"Building {outputFilePath}...{Environment.NewLine}");
            generalPane.Activate(); // Brings this pane into view

            _dte.Solution.SolutionBuild.BuildProject(selectedProject.ConfigurationManager.ActiveConfiguration.ConfigurationName, selectedProject.UniqueName, true);

            generalPane.OutputString($"Sending to emulator...{Environment.NewLine}");
            generalPane.Activate(); // Brings this pane into view

            ExecutePortForwardCommmand(generalPane);

            SendAssemblyToEmulator(outputFilePath, generalPane);

            // Show a message box to prove we were here
            //VsShellUtilities.ShowMessageBox(
            //    package,
            //    message,
            //    outputFilePath,
            //    OLEMSGICON.OLEMSGICON_INFO,
            //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            //string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            //string title = "ReloadCommand";

            //IVsHierarchy hierarchy = null;
            //uint itemid = VSConstants.VSITEMID_NIL;

            //if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;

            //var currentProject = ((IVsProject)hierarchy);

            //// Show a message box to prove we were here
            //VsShellUtilities.ShowMessageBox(
            //    this.package,
            //    message,
            //    title,
            //    OLEMSGICON.OLEMSGICON_INFO,
            //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private IVsOutputWindowPane GetVsOutputWindow()
        {
            ThreadHelper.ThrowIfNotOnUIThread();


            Guid generalPaneGuid = VSConstants.GUID_OutWindowDebugPane; // P.S. There's also the GUID_OutWindowDebugPane available. (GUID_OutWindowGeneralPane)
            _outputWindow.GetPane(ref generalPaneGuid, out IVsOutputWindowPane generalPane);

            return generalPane;
        }

        private static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        private static bool ExecutePortForwardCommmand(IVsOutputWindowPane outputPane)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            
            var adbCommandLine = "\"" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Android", "sdk", "platform-tools", "adb" + (IsWindows ? ".exe" : "")) + "\" "
                + "forward tcp:45820 tcp:45820";

            var process = new System.Diagnostics.Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            if (IsWindows)
            {
                process.StartInfo.Arguments = adbCommandLine;
                process.StartInfo.FileName = "powershell";
            }
            else
            {
                process.StartInfo.Arguments = string.Format("-c \"{0}\"", adbCommandLine);
                process.StartInfo.FileName = "/bin/sh";
            }

            try
            {
                process.Start();

                var adb_output = process.StandardOutput.ReadToEnd();

                if (adb_output.Length > 0 && adb_output != "45820" + Environment.NewLine)
                    throw new InvalidOperationException($"Unable to forward tcp port from emulator, is emulator running? (adb tool returned '{adb_output}')");
            }
            catch (Exception ex)
            {
                outputPane.OutputString($"{process.StandardOutput.ReadToEnd()}{Environment.NewLine}");
                outputPane.OutputString($"{process.StandardError.ReadToEnd()}{Environment.NewLine}");
                outputPane.OutputString($"{ex}{Environment.NewLine}");
                return false;
            }

            return true;
        }

        private static void SendAssemblyToEmulator(string assemblyPath, IVsOutputWindowPane outputPane)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var client = new TcpClient
            {
                ReceiveTimeout = 5000,
                SendTimeout = 5000
            };

            try
            {
                client.Connect(IPAddress.Loopback, 45820);

                var assemblyRaw = File.ReadAllBytes(assemblyPath);

                var binaryWriter = new BinaryWriter(client.GetStream());

                binaryWriter.Write(assemblyRaw.Length);

                binaryWriter.Write(assemblyRaw);

                binaryWriter.Flush();

                var binaryReader = new BinaryReader(client.GetStream());

                binaryReader.ReadBoolean();

                outputPane.OutputString($"New assembly ({assemblyRaw.Length} bytes) sent{Environment.NewLine}");
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                outputPane.OutputString(ex.ToString());
            }
            finally
            {
                client.Close();
            }
        }
    }
}
