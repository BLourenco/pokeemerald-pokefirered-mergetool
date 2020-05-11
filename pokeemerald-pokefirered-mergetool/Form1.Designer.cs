namespace pokeemerald_pokefirered_mergetool
{
    partial class MergeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelEmeraldFilePath = new System.Windows.Forms.Label();
            this.textBoxEmeraldFilePath = new System.Windows.Forms.TextBox();
            this.buttonEmeraldBrowse = new System.Windows.Forms.Button();
            this.buttonFireRedBrowse = new System.Windows.Forms.Button();
            this.textBoxFireRedFilePath = new System.Windows.Forms.TextBox();
            this.labelFireRedFilePath = new System.Windows.Forms.Label();
            this.buttonOutputBrowse = new System.Windows.Forms.Button();
            this.textBoxOutputFilePath = new System.Windows.Forms.TextBox();
            this.labelOutputFilePath = new System.Windows.Forms.Label();
            this.buttonMerge = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.buttonCancelMerge = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelEmeraldFilePath
            // 
            this.labelEmeraldFilePath.AutoSize = true;
            this.labelEmeraldFilePath.Location = new System.Drawing.Point(12, 9);
            this.labelEmeraldFilePath.Name = "labelEmeraldFilePath";
            this.labelEmeraldFilePath.Size = new System.Drawing.Size(115, 13);
            this.labelEmeraldFilePath.TabIndex = 0;
            this.labelEmeraldFilePath.Text = "pokeemerald File Path:";
            // 
            // textBoxEmeraldFilePath
            // 
            this.textBoxEmeraldFilePath.Location = new System.Drawing.Point(12, 25);
            this.textBoxEmeraldFilePath.Name = "textBoxEmeraldFilePath";
            this.textBoxEmeraldFilePath.Size = new System.Drawing.Size(346, 20);
            this.textBoxEmeraldFilePath.TabIndex = 1;
            this.textBoxEmeraldFilePath.Text = "D:\\Repos\\pokeemerald";
            // 
            // buttonEmeraldBrowse
            // 
            this.buttonEmeraldBrowse.Location = new System.Drawing.Point(364, 23);
            this.buttonEmeraldBrowse.Name = "buttonEmeraldBrowse";
            this.buttonEmeraldBrowse.Size = new System.Drawing.Size(91, 23);
            this.buttonEmeraldBrowse.TabIndex = 2;
            this.buttonEmeraldBrowse.Text = "Browse...";
            this.buttonEmeraldBrowse.UseVisualStyleBackColor = true;
            this.buttonEmeraldBrowse.Click += new System.EventHandler(this.buttonEmeraldBrowse_Click);
            // 
            // buttonFireRedBrowse
            // 
            this.buttonFireRedBrowse.Location = new System.Drawing.Point(364, 77);
            this.buttonFireRedBrowse.Name = "buttonFireRedBrowse";
            this.buttonFireRedBrowse.Size = new System.Drawing.Size(91, 23);
            this.buttonFireRedBrowse.TabIndex = 5;
            this.buttonFireRedBrowse.Text = "Browse...";
            this.buttonFireRedBrowse.UseVisualStyleBackColor = true;
            this.buttonFireRedBrowse.Click += new System.EventHandler(this.buttonFireRedBrowse_Click);
            // 
            // textBoxFireRedFilePath
            // 
            this.textBoxFireRedFilePath.Location = new System.Drawing.Point(12, 79);
            this.textBoxFireRedFilePath.Name = "textBoxFireRedFilePath";
            this.textBoxFireRedFilePath.Size = new System.Drawing.Size(346, 20);
            this.textBoxFireRedFilePath.TabIndex = 4;
            this.textBoxFireRedFilePath.Text = "D:\\Repos\\pokefirered";
            // 
            // labelFireRedFilePath
            // 
            this.labelFireRedFilePath.AutoSize = true;
            this.labelFireRedFilePath.Location = new System.Drawing.Point(12, 63);
            this.labelFireRedFilePath.Name = "labelFireRedFilePath";
            this.labelFireRedFilePath.Size = new System.Drawing.Size(107, 13);
            this.labelFireRedFilePath.TabIndex = 3;
            this.labelFireRedFilePath.Text = "pokefirered File Path:";
            // 
            // buttonOutputBrowse
            // 
            this.buttonOutputBrowse.Location = new System.Drawing.Point(364, 130);
            this.buttonOutputBrowse.Name = "buttonOutputBrowse";
            this.buttonOutputBrowse.Size = new System.Drawing.Size(91, 23);
            this.buttonOutputBrowse.TabIndex = 8;
            this.buttonOutputBrowse.Text = "Browse...";
            this.buttonOutputBrowse.UseVisualStyleBackColor = true;
            this.buttonOutputBrowse.Click += new System.EventHandler(this.buttonOutputBrowse_Click);
            // 
            // textBoxOutputFilePath
            // 
            this.textBoxOutputFilePath.Location = new System.Drawing.Point(12, 132);
            this.textBoxOutputFilePath.Name = "textBoxOutputFilePath";
            this.textBoxOutputFilePath.Size = new System.Drawing.Size(346, 20);
            this.textBoxOutputFilePath.TabIndex = 7;
            this.textBoxOutputFilePath.Text = "D:\\Repos\\pokefireredemerald";
            // 
            // labelOutputFilePath
            // 
            this.labelOutputFilePath.AutoSize = true;
            this.labelOutputFilePath.Location = new System.Drawing.Point(12, 116);
            this.labelOutputFilePath.Name = "labelOutputFilePath";
            this.labelOutputFilePath.Size = new System.Drawing.Size(125, 13);
            this.labelOutputFilePath.TabIndex = 6;
            this.labelOutputFilePath.Text = "Merged Output File Path:";
            // 
            // buttonMerge
            // 
            this.buttonMerge.Location = new System.Drawing.Point(364, 182);
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.Size = new System.Drawing.Size(91, 23);
            this.buttonMerge.TabIndex = 9;
            this.buttonMerge.Text = "Merge";
            this.buttonMerge.UseVisualStyleBackColor = true;
            this.buttonMerge.Click += new System.EventHandler(this.buttonMerge_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 182);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(346, 20);
            this.progressBar1.TabIndex = 10;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(12, 166);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(35, 13);
            this.labelProgress.TabIndex = 11;
            this.labelProgress.Text = "label1";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // buttonCancelMerge
            // 
            this.buttonCancelMerge.Location = new System.Drawing.Point(364, 211);
            this.buttonCancelMerge.Name = "buttonCancelMerge";
            this.buttonCancelMerge.Size = new System.Drawing.Size(91, 23);
            this.buttonCancelMerge.TabIndex = 12;
            this.buttonCancelMerge.Text = "Cancel";
            this.buttonCancelMerge.UseVisualStyleBackColor = true;
            this.buttonCancelMerge.Click += new System.EventHandler(this.buttonCancelMerge_Click);
            // 
            // MergeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 247);
            this.Controls.Add(this.buttonCancelMerge);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonMerge);
            this.Controls.Add(this.buttonOutputBrowse);
            this.Controls.Add(this.textBoxOutputFilePath);
            this.Controls.Add(this.labelOutputFilePath);
            this.Controls.Add(this.buttonFireRedBrowse);
            this.Controls.Add(this.textBoxFireRedFilePath);
            this.Controls.Add(this.labelFireRedFilePath);
            this.Controls.Add(this.buttonEmeraldBrowse);
            this.Controls.Add(this.textBoxEmeraldFilePath);
            this.Controls.Add(this.labelEmeraldFilePath);
            this.Name = "MergeForm";
            this.Text = "Emerald-FireRed Merge Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEmeraldFilePath;
        private System.Windows.Forms.TextBox textBoxEmeraldFilePath;
        private System.Windows.Forms.Button buttonEmeraldBrowse;
        private System.Windows.Forms.Button buttonFireRedBrowse;
        private System.Windows.Forms.TextBox textBoxFireRedFilePath;
        private System.Windows.Forms.Label labelFireRedFilePath;
        private System.Windows.Forms.Button buttonOutputBrowse;
        private System.Windows.Forms.TextBox textBoxOutputFilePath;
        private System.Windows.Forms.Label labelOutputFilePath;
        private System.Windows.Forms.Button buttonMerge;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button buttonCancelMerge;
    }
}

