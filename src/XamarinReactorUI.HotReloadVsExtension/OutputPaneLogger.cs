using Microsoft.Build.Framework;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;

namespace XamarinReactorUI.HotReloadVsExtension
{
    internal class OutputPaneLogger : ILogger
    {
        private IVsOutputWindowPane outputPane;
        private readonly List<IEventSource> _eventSources = new List<IEventSource>();

        public OutputPaneLogger(IVsOutputWindowPane outputPane)
        {
            this.outputPane = outputPane;
        }

        public LoggerVerbosity Verbosity { get; set; }
        public string Parameters { get; set; }

        public void Initialize(IEventSource eventSource)
        {
            _eventSources.Add(eventSource);
            if (eventSource != null)
            {
                //eventSource.AnyEventRaised += EventSource_AnyEventRaised;
                //eventSource.ProjectStarted += EventSource_ProjectStarted;
                //eventSource.ProjectFinished += EventSource_ProjectFinished; ;
                eventSource.ErrorRaised += EventSource_ErrorRaised;
                eventSource.BuildStarted += EventSource_BuildStarted;
                eventSource.BuildFinished += EventSource_BuildFinished;
                //eventSource.TaskStarted += EventSource_TaskStarted;
            }
        }

        private void EventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {
            outputPane.OutputStringThreadSafe($"{e.Message}Finished{Environment.NewLine}");
        }

        private void EventSource_TaskStarted(object sender, TaskStartedEventArgs e)
        {
            outputPane.OutputStringThreadSafe($"{e.Message}{Environment.NewLine}");
        }

        private void EventSource_BuildFinished(object sender, BuildFinishedEventArgs e)
        {
            outputPane.OutputStringThreadSafe($"{e.Message}{Environment.NewLine}");
        }

        private void EventSource_BuildStarted(object sender, BuildStartedEventArgs e)
        {
            outputPane.OutputStringThreadSafe($"{e.Message}{Environment.NewLine}");
        }

        private void EventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
        {
            outputPane.OutputStringThreadSafe($"{e.Message}Started{Environment.NewLine}");
        }

        private void EventSource_ErrorRaised(object sender, BuildErrorEventArgs e)
        {
            outputPane.OutputStringThreadSafe($"{e.Message}{Environment.NewLine}");
        }

        private void EventSource_AnyEventRaised(object sender, BuildEventArgs e)
        {
            outputPane.OutputStringThreadSafe($"{e.Message}{Environment.NewLine}");
        }

        public void Shutdown()
        {
            foreach (var eventSource in _eventSources)
            {
                //eventSource.AnyEventRaised += EventSource_AnyEventRaised;
                //eventSource.ProjectStarted -= EventSource_ProjectStarted;
                //eventSource.ProjectFinished -= EventSource_ProjectFinished;
                eventSource.ErrorRaised -= EventSource_ErrorRaised;
                eventSource.BuildStarted -= EventSource_BuildStarted;
                eventSource.BuildFinished -= EventSource_BuildFinished;
                //eventSource.TaskStarted -= EventSource_TaskStarted;
            }

            _eventSources.Clear();
        }
    }
}