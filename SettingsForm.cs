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
            if (File.Exists(Program.settingsPath))
            {
                int val;

                val = 0;
                if (int.TryParse(File.ReadAllText(Program.sizeFile), out val))
                    trackBarSize.Value = val;

                val = 0;
                if (int.TryParse(File.ReadAllText(Program.speedFile), out val))
                    trackBarSpeed.Value = val;
            }
            trackBarSize.ValueChanged += (s, e) =>
            {
                sizelabel.Text = $"Size: {trackBarSize.Value}";
            };
            trackBarSpeed.ValueChanged += (s, e) =>
            {
                speedlabel.Text = $"Speed: {trackBarSpeed.Value}";
            };
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
            sizelabel = new Label();
            trackBarSpeed = new TrackBar();
            speedlabel = new Label();
            ((ISupportInitialize)trackBarSize).BeginInit();
            ((ISupportInitialize)trackBarSpeed).BeginInit();
            SuspendLayout();
            // 
            // trackBarSize
            // 
            trackBarSize.LargeChange = 16;
            trackBarSize.Location = new Point(12, 27);
            trackBarSize.Maximum = 512;
            trackBarSize.Minimum = 8;
            trackBarSize.Name = "trackBarSize";
            trackBarSize.Size = new Size(358, 45);
            trackBarSize.TabIndex = 0;
            trackBarSize.Value = 64;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(295, 123);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += Click_Ok;
            // 
            // sizelabel
            // 
            sizelabel.AutoSize = true;
            sizelabel.Location = new Point(12, 9);
            sizelabel.Name = "sizelabel";
            sizelabel.Size = new Size(27, 15);
            sizelabel.TabIndex = 2;
            sizelabel.Text = "Size";
            // 
            // trackBarSpeed
            // 
            trackBarSpeed.LargeChange = 2;
            trackBarSpeed.Location = new Point(14, 78);
            trackBarSpeed.Maximum = 30;
            trackBarSpeed.Minimum = 1;
            trackBarSpeed.Name = "trackBarSpeed";
            trackBarSpeed.Size = new Size(356, 45);
            trackBarSpeed.TabIndex = 3;
            trackBarSpeed.Value = 5;
            // 
            // speedlabel
            // 
            speedlabel.AutoSize = true;
            speedlabel.Location = new Point(12, 60);
            speedlabel.Name = "speedlabel";
            speedlabel.Size = new Size(39, 15);
            speedlabel.TabIndex = 4;
            speedlabel.Text = "Speed";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 158);
            Controls.Add(speedlabel);
            Controls.Add(trackBarSpeed);
            Controls.Add(sizelabel);
            Controls.Add(btnOK);
            Controls.Add(trackBarSize);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsForm";
            Text = "Settings for SebbyScreensaver";
            ((ISupportInitialize)trackBarSize).EndInit();
            ((ISupportInitialize)trackBarSpeed).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private void Click_Ok(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Program.settingsPath);
            File.WriteAllText(Program.sizeFile, trackBarSize.Value.ToString());
            File.WriteAllText(Program.speedFile, trackBarSpeed.Value.ToString());
            this.Close();
        }

        private TrackBar trackBarSize = null!;
        private Button btnOK = null!;
        private TrackBar trackBarSpeed = null!;
        private Label speedlabel = null!;
        private Label sizelabel = null!;
    }
}