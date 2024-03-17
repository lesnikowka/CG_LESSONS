﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_LESSON_1
{
    public partial class Form1 : Form
    {
        Bitmap image = null;
        string logName = "prev.txt";

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            try
            {

                using (var streamReader = new StreamReader(logName))
                {
                    string fileName = streamReader.ReadToEnd();
                    loadImage(fileName);
                    streamReader.Close();
                }
            }
            catch { }
        }

        private void loadImage(string path)
        {
            image = new Bitmap(path);
            pictureBox1.Image = image;
            pictureBox1.Refresh();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.*";
        
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                loadImage(ofd.FileName);

                using (var streamWriter = new StreamWriter(logName, false))
                {
                    streamWriter.Write(ofd.FileName);
                    streamWriter.Close();
                }
            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            InvertFilter filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            
            if (backgroundWorker1.CancellationPending != true)
            {
                image = newImage;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }

            progressBar1.Value = 0;
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            BlurFilter filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            GaussianFilter filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void чернобелыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            GrayScaleFilter filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            SepiaFilter filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
}
