using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pokeemerald_pokefirered_mergetool
{
    public partial class MergeForm : Form
    {
        private int numberToCompute = 0;
        private int highestPercentageReached = 0;

        public MergeForm()
        {
            InitializeComponent();
        }

        private string GetFolderPath()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            else
            {
                return "";
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            buttonMerge.Enabled = enabled;

            buttonEmeraldBrowse.Enabled = enabled;
            textBoxEmeraldFilePath.Enabled = enabled;

            buttonFireRedBrowse.Enabled = enabled;
            textBoxFireRedFilePath.Enabled = enabled;

            buttonOutputBrowse.Enabled = enabled;
            textBoxOutputFilePath.Enabled = enabled;
        }

        private void buttonEmeraldBrowse_Click(object sender, EventArgs e)
        {
            textBoxEmeraldFilePath.Text = GetFolderPath();
        }

        private void buttonFireRedBrowse_Click(object sender, EventArgs e)
        {
            textBoxFireRedFilePath.Text = GetFolderPath();
        }

        private void buttonOutputBrowse_Click(object sender, EventArgs e)
        {
            textBoxOutputFilePath.Text = GetFolderPath();
        }

        private void buttonMerge_Click(object sender, EventArgs e)
        {
            // Reset the text in the result label.
            labelProgress.Text = String.Empty;

            SetControlsEnabled(false);

            string[] paths = new string[3];
            paths[0] = textBoxEmeraldFilePath.Text;
            paths[1] = textBoxFireRedFilePath.Text;
            paths[2] = textBoxOutputFilePath.Text;

            backgroundWorker.RunWorkerAsync(paths);
        }

        private void buttonCancelMerge_Click(object sender, EventArgs e)
        {
            // Cancel the asynchronous operation.
            this.backgroundWorker.CancelAsync();

            // Disable the Cancel button.
            buttonCancelMerge.Enabled = false;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the
            // RunWorkerCompleted eventhandler.
            e.Result = Start((string[])e.Argument, worker, e);
        }
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled
                // the operation.
                // Note that due to a race condition in
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                labelProgress.Text = "Cancelled";
            }
            else
            {
                string resultText = "Unknown result";
                switch (e.Result)
                {
                    case 0:
                        resultText = "Success!";
                        break;
                    case 1:
                        resultText = "Failed! Ensure your output folder is empty.";
                        break;

                }

                // Finally, handle the case where the operation
                // succeeded.
                labelProgress.Text = resultText;
            }

            SetControlsEnabled(true);
        }

        private void StartFibonacci()
        {
            // Get the value from the UpDown control.
            numberToCompute = 40;

            // Reset the variable for percentage tracking.
            highestPercentageReached = 0;

            // Start the asynchronous operation.
            backgroundWorker.RunWorkerAsync(numberToCompute);
        }

        long Start(string[] paths, BackgroundWorker worker, DoWorkEventArgs e)
        {
            return MergeTool.StartMerge(paths);
        }

        // This is the method that does the actual work. For this
        // example, it computes a Fibonacci number and
        // reports progress as it does its work.
        long ComputeFibonacci(int n, BackgroundWorker worker, DoWorkEventArgs e)
        {
            // The parameter n must be >= 0 and <= 91.
            // Fib(n), with n > 91, overflows a long.
            if ((n < 0) || (n > 91))
            {
                throw new ArgumentException(
                    "value must be >= 0 and <= 91", "n");
            }

            long result = 0;

            // Abort the operation if the user has canceled.
            // Note that a call to CancelAsync may have set
            // CancellationPending to true just after the
            // last invocation of this method exits, so this
            // code will not have the opportunity to set the
            // DoWorkEventArgs.Cancel flag to true. This means
            // that RunWorkerCompletedEventArgs.Cancelled will
            // not be set to true in your RunWorkerCompleted
            // event handler. This is a race condition.

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                if (n < 2)
                {
                    result = 1;
                }
                else
                {
                    result = ComputeFibonacci(n - 1, worker, e) +
                             ComputeFibonacci(n - 2, worker, e);
                }

                // Report progress as a percentage of the total task.
                int percentComplete =
                    (int)((float)n / (float)numberToCompute * 100);
                if (percentComplete > highestPercentageReached)
                {
                    highestPercentageReached = percentComplete;
                    worker.ReportProgress(percentComplete);
                }
            }

            return result;
        }

        
    }
}
