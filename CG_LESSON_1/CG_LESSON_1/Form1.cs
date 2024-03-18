using System;
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
        string kernelName = "kernel.txt";

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

            try
            {
                using (var streamReader = new StreamReader(kernelName))
                {
                    string kernelText = streamReader.ReadToEnd();
                    richTextBox1.Text = kernelText;
                    parseMatrix(kernelText);
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

            var filter = new InvertFilter();
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

            var filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void чернобелыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void повышениеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new BrightnessFilter(true);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void понижениеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new BrightnessFilter(false);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new ClarityFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void фильтрСоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрСобеляосьYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (image == null)
            //{
            //    return;
            //}
            //
            //var filter = new SobelFilter(false);
            //backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new GrayWorldFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var sfd = new SaveFileDialog();
            sfd.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.*";

            sfd.Title = "Save an Image File";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                image.Save(sfd.FileName);
            }
        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new GlassFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void вертикальныеВолныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new WaveFilter(WaveType.VERTICAL);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void горизонтальныеВолныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new WaveFilter(WaveType.HORIZONTAL);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new MotionBlurFilter(9);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линейноеРастяжениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new LinearContrastCorrectionFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void label1_Click(object sender, EventArgs e){}

        private float[,] parseMatrix(string m)
        {
            string nor = "";

            for (int i = 0; i < m.Length; i++)
            {
                if (m[i] != '\r')
                {
                    nor += m[i];
                }
            }

            if (nor[nor.Length-1] ==  '\n') 
            {
                nor = nor.Substring(0, nor.Length - 1);
            }

            int numLines = 1;
            int numCols = 1;

            string onlySpaces = "";

            for (int i = 0; i <  nor.Length; i++) 
            {
                if (nor[i] == '\n')
                {
                    numLines++;
                    numCols++;
                    onlySpaces += " ";
                    continue;
                }
                else if (nor[i] == ' ')
                {
                    numCols++;
                }
                onlySpaces += nor[i];
            }

            numCols /= numLines;

            var result = new float[numLines, numCols];

            string[] ssize = onlySpaces.Split();

            for (int i = 0; i < numLines; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    result[i,j] = float.Parse(ssize[numCols * i + j]);
                }
            }

            return result;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            using (var streamWriter = new StreamWriter(kernelName, false))
            {
                streamWriter.Write(richTextBox1.Text);
                streamWriter.Close();
            }
        }

        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new DilationFilter(parseMatrix(richTextBox1.Text));
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сужениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new ErosionFilter(parseMatrix(richTextBox1.Text));
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void закрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new ClosingFilter(parseMatrix(richTextBox1.Text));
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void открытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new OpeningFilter(parseMatrix(richTextBox1.Text));
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void topHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                return;
            }

            var filter = new TopHatFilter(parseMatrix(richTextBox1.Text));
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
}
