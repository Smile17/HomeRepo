using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L_Threads
{
    public class ClockTaskResponse
    {
        private readonly string message;

        public ClockTaskResponse(string msg)
        {
            this.message = msg;
        }

        public string Message { get { return message; } }
    }
    public class ClockTask
    {
        public string Word { get; set; }
        public int TimeInSeconds { get; set; }

        // Boolean that indicates wheter the process is running or has been stopped
        private bool ClockProcessStopped;

        // Expose the SynchronizationContext on the entire class
        private readonly SynchronizationContext SyncContext;

        // Create the Callback containers
        public event EventHandler<ClockTaskResponse> Callback;

        // Constructor of your heavy task
        public ClockTask(string word, int time)
        {
            Word = word; TimeInSeconds = time;
            // Important to update the value of SyncContext in the constructor with
            // the SynchronizationContext of the AsyncOperationManager
            SyncContext = AsyncOperationManager.SynchronizationContext;
        }

        // Method to start the thread
        public void Start()
        {
            Thread thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();
        }

        // Method to stop the thread
        public void Stop()
        {
            ClockProcessStopped = true;
        }

        // Method where the main logic of your heavy task happens
        private void Run()
        {
            while (!ClockProcessStopped)
            {
                

                // Trigger the first callback from background thread to the main thread (UI)
                // the callback enables the first button !
                SyncContext.Post(e => triggerCallback(
                    new ClockTaskResponse(Word + "\t")
                ), null);

                // Wait another 2 seconds for more heavy tasks ...
                Thread.Sleep(TimeInSeconds * 1000);

            }
        }

        private void triggerCallback(ClockTaskResponse response)
        {
            Callback?.Invoke(this, response);
        }
        
    }
}
