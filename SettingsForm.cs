using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SebbyScreensaver
{
    public partial class SettingsForm : Form
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


        public SettingsForm()
        {
            InitializeComponent();
            if (trackBarSize != null)
            {
                if (File.Exists(Program.settingsPath))
                {
                    int val;
                    if (int.TryParse(File.ReadAllText(Program.settingsPath), out val))
                        trackBarSize.Value = val;
                }
                trackBarSize.ValueChanged += (s, e) =>
                {
                    this.Text = $"Size: {trackBarSize.Value}";
                };
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ImageSize = trackBarSize.Value;
            Properties.Settings.Default.Save();
            this.Close();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsForm));
            trackBarSize = new TrackBar();
            btnOK = new Button();
            titleLabel = new Label();
            ((ISupportInitialize)trackBarSize).BeginInit();
            SuspendLayout();
            // 
            // trackBarSize
            // 
            trackBarSize.LargeChange = 16;
            trackBarSize.Location = new Point(12, 41);
            trackBarSize.Maximum = 512;
            trackBarSize.Minimum = 16;
            trackBarSize.Name = "trackBarSize";
            trackBarSize.Size = new Size(358, 45);
            trackBarSize.TabIndex = 0;
            trackBarSize.Value = 64;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(295, 92);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += button1_Click;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(12, 9);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(106, 15);
            titleLabel.TabIndex = 2;
            titleLabel.Text = "Change image size";
            titleLabel.Click += label1_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 127);
            Controls.Add(titleLabel);
            Controls.Add(btnOK);
            Controls.Add(trackBarSize);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsForm";
            Text = "Settings for SebbyScreensaver";
            ((ISupportInitialize)trackBarSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Program.settingsPath));
            File.WriteAllText(Program.settingsPath, trackBarSize.Value.ToString());
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e){}

        private TrackBar? trackBarSize;
        private Button? btnOK;
        private Label? titleLabel;
    }
}