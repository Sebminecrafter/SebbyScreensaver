using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Timer = System.Windows.Forms.Timer;

namespace SebbyScreensaver
{
    public partial class ScreensaverForm : Form
    {
        public ScreensaverForm()
        {
            InitializeComponent();
        }
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private Point mouseLocation;
        private Timer timer = new Timer();
        private int x = 100, y = 100;
        private int dx = 5, dy = 5;
        private Image logo = ByteArrayToImage(Properties.Resources.sebby);
        private int size = 64;

        public ScreensaverForm(Rectangle bounds)
        {
            InitializeComponent();

            this.Bounds = bounds;
            Init();
        }

        public ScreensaverForm(IntPtr previewHandle)
        {
            InitializeComponent();

            NativeMethods.SetParent(this.Handle, previewHandle);
            this.Bounds = new Rectangle(0, 0, 300, 200);

            Init();
        }

        private void Init()
        {
            int val;

            val = 0;
            if (int.TryParse(File.ReadAllText(Program.sizeFile), out val))
            {
                size = val;
            }

            val = 0;
            if (int.TryParse(File.ReadAllText(Program.speedFile), out val))
            {
                dx = val;
                dy = val;
            }
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;

            Cursor.Hide();

            timer.Interval = 16;
            timer.Tick += (s, e) =>
            {
                x += dx;
                y += dy;

                if (x < 0 || x > this.Width - size) dx *= -1;
                if (y < 0 || y > this.Height - size) dy *= -1;

                this.Invalidate();
            };
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(logo, x, y, size, size);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!mouseLocation.IsEmpty)
            {
                if (Math.Abs(e.X - mouseLocation.X) > 5 ||
                    Math.Abs(e.Y - mouseLocation.Y) > 5)
                {
                    closeScreensaver();
                }
            }
            mouseLocation = e.Location;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            closeScreensaver();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            closeScreensaver();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timer.Stop();
            base.OnFormClosing(e);
        }

        private void closeScreensaver()
        {
            foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
            {
                form.Close();
            }
            Application.Exit();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Cursor.Show();
        }

        private static Image ByteArrayToImage(byte[] bytes)
        {
            using (var ms = new System.IO.MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // ScreenSaverForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 389);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ScreenSaverForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion
    }
}
static class NativeMethods
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);
}