using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace ImageToHex
{
    public partial class MainForm : Form
    {
        List<byte> convertedBytes = new List<byte>();
        SerialPort arduinoPort = null;
        bool printing = false;
        bool printEnd = true;
        public MainForm()
        {
            InitializeComponent();
        }

        public static Image AdjustContrast(Image img, float contrast)
        {
            //コントラストを変更した画像の描画先となるImageオブジェクトを作成
            Bitmap newImg = new Bitmap(img.Width, img.Height);
            //newImgのGraphicsオブジェクトを取得
            Graphics g = Graphics.FromImage(newImg);

            //ColorMatrixオブジェクトの作成
            float scale = (100f + contrast) / 100f;
            scale *= scale;
            float append = 0.5f * (1f - scale);
            System.Drawing.Imaging.ColorMatrix cm =
                new System.Drawing.Imaging.ColorMatrix(
                    new float[][] {
                        new float[] {scale, 0, 0, 0, 0},
                        new float[] {0, scale, 0, 0, 0},
                        new float[] {0, 0, scale, 0, 0}, 
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {append, append, append, 0, 1}
                    });

            //ImageAttributesオブジェクトの作成
            System.Drawing.Imaging.ImageAttributes ia =
                new System.Drawing.Imaging.ImageAttributes();
            //ColorMatrixを設定する
            ia.SetColorMatrix(cm);

            //ImageAttributesを使用して描画
            g.DrawImage(img,
                new Rectangle(0, 0, img.Width, img.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            //リソースを解放する
            g.Dispose();

            return newImg;
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            fd.CheckFileExists = true;
            fd.CheckPathExists = true;
            fd.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                fileAddressTextBox.Text = fd.FileName;
                convertButton_Click(null, null);
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(fileAddressTextBox.Text))
            {
                var bmpOriginBase = new Bitmap(fileAddressTextBox.Text);
                var bmpOrigin = (Bitmap)AdjustContrast(bmpOriginBase, (float)ContrastNumericUpDown.Value);
                bmpOriginBase.Dispose();
                if (bmpOrigin.Height < bmpOrigin.Width && bmpOrigin.Width > 160)
                {
                    bmpOrigin.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                Bitmap bmp;

                if (bmpOrigin.Width > 160)
                {
                    //描画先とするImageオブジェクトを作成する
                    bmp = new Bitmap(160, (int)(bmpOrigin.Height * (160.0 / bmpOrigin.Width)));
                    //ImageオブジェクトのGraphicsオブジェクトを作成する
                    Graphics g = Graphics.FromImage(bmp);
                    g.InterpolationMode =
                        System.Drawing.Drawing2D.InterpolationMode.High;
                    g.DrawImage(bmpOrigin, 0, 0, bmp.Width, bmp.Height);
                    g.Dispose();
                    bmpOrigin.Dispose();
                }
                else
                {
                    bmp = bmpOrigin;
                }

                var pixels = new byte[160, (int)(Math.Ceiling(bmp.Height / 16.0) * 16)];
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color c = bmp.GetPixel(x, y);
                        Color resultColor;
                        int gray = (int)(0.298912 * c.R + 0.586611 * c.G + 0.114478 * c.B);
                        if (gray <= 63)
                        {
                            pixels[x, y] = 3;
                            resultColor = Color.FromArgb(0, 0, 0);
                        }
                        else if (gray <= 127)
                        {
                            pixels[x, y] = 2;
                            resultColor = Color.FromArgb(85, 85, 85);
                        }
                        else if (gray <= 191)
                        {
                            pixels[x, y] = 1;
                            resultColor = Color.FromArgb(170, 170, 170);
                        }
                        else
                        {
                            pixels[x, y] = 0;
                            resultColor = Color.FromArgb(255, 255, 255);
                        }
                        bmp.SetPixel(x, y, resultColor);
                    }
                }
                var bytes = new List<byte>();
                for (int b = 0; b < (int)Math.Ceiling(bmp.Height / 16.0); b++)
                {
                    int by = b * 16;
                    for (int t = 0; t < 40; t++)
                    {
                        int tx, ty;
                        if (t < 20)
                        {
                            tx = t * 8;
                            ty = by;
                        }
                        else
                        {
                            tx = (t - 20) * 8;
                            ty = by + 8;
                        }
                        for (int c = 0; c < 8; c++)
                        {
                            int cx = tx;
                            int cy = ty + c;
                            byte b0 = 0;
                            byte b1 = 0;
                            for (int p = 0; p < 8; p++)
                            {
                                b0 += (byte)((pixels[cx + p, cy] & 0x1) << (7 - p));
                                b1 += (byte)(((pixels[cx + p, cy] & 0x2) >> 1) << (7 - p));
                            }
                            bytes.Add(b0);
                            bytes.Add(b1);
                        }
                    }
                }
                int addCount = (int)(Math.Ceiling(bytes.Count / 640.0) * 640) - bytes.Count;
                for (int i = 0; i < addCount; i++)
                {
                    bytes.Add(0);
                }
                var sb = new StringBuilder();
                for (int i = 0; i < bytes.Count / 640; i++)
                {
                    sb.Append("const uint8_t band" + i + "[640] PROGMEM = {\n");
                    for (int j = 0; j < 640; j++)
                    {
                        sb.Append("0x" + bytes[i * 640 + j].ToString("X2"));
                        if (j != 639)
                        {
                            sb.Append(", ");
                        }
                    }
                    sb.Append("\n};\n");
                }
                convertedPictureBox.Image = bmp;
                //bmp.Dispose();
                convertedBytes = bytes;
            }
        }

        private void RefleshPortsButton_Click(object sender, EventArgs e)
        {
            PortsComboBox.DataSource = SerialPort.GetPortNames();
        }

        private void SendDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                printing = true;
                printEnd = false;
                if (convertedBytes.Count > 0)
                {
                    SendDataButton.Enabled = false;
                    if (convertedBytes.Count < 16000)
                    {
                        uint byteCount = (uint)convertedBytes.Count;
                        var dataSize = new byte[6];
                        dataSize[0] = (byte)((byteCount & 0xFF000000) >> 24);
                        dataSize[1] = (byte)((byteCount & 0x00FF0000) >> 16);
                        dataSize[2] = (byte)((byteCount & 0x0000FF00) >> 8);
                        dataSize[3] = (byte)((byteCount & 0x000000FF) >> 0);
                        dataSize[4] = (byte)1;
                        dataSize[5] = (byte)3;
                        arduinoPort.Write(dataSize, 0, dataSize.Length);
                        arduinoPort.Write(convertedBytes.ToArray(), 0, (int)byteCount);
                    }
                    else
                    {
                        for (long l = 0; l < convertedBytes.Count; l += 16000)
                        {
                            printEnd = false;
                            UInt32 byteCount = 16000;
                            if (l + 16000 >= convertedBytes.Count)
                            {
                                byteCount = (UInt32)convertedBytes.Count & 16000;
                            }
                            var dataSize = new byte[6];
                            dataSize[0] = (byte)((byteCount & 0xFF000000) >> 24);
                            dataSize[1] = (byte)((byteCount & 0x00FF0000) >> 16);
                            dataSize[2] = (byte)((byteCount & 0x0000FF00) >> 8);
                            dataSize[3] = (byte)((byteCount & 0x000000FF) >> 0);
                            dataSize[4] = (byte)0;
                            dataSize[5] = (byte)0;
                            arduinoPort.Write(dataSize, 0, dataSize.Length);
                            arduinoPort.Write(convertedBytes.ToArray(), (int)l, (int)byteCount);
                            while(!printEnd){
                                Thread.Sleep(1);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                printing = false;
            }
        }

        void arduinoPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!printing)
            {
                MessageBox.Show(arduinoPort.ReadExisting());
                SendDataButton.Enabled = true;
            }
            printEnd = true;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortsComboBox.SelectedIndex >= 0)
                {
                    arduinoPort = new SerialPort(PortsComboBox.Text, 9600);
                    arduinoPort.RtsEnable = true;
                    arduinoPort.DtrEnable = true;
                    arduinoPort.Open();
                    arduinoPort.ReadExisting();
                    arduinoPort.DataReceived += arduinoPort_DataReceived;
                    DisConnectButton.Enabled = true;
                    ConnectButton.Enabled = false;
                    SendDataButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DisConnectButton_Click(object sender, EventArgs e)
        {
            arduinoPort.RtsEnable = false;
            arduinoPort.DtrEnable = false;
            arduinoPort.Close();
            arduinoPort = null;
            DisConnectButton.Enabled = false;
            ConnectButton.Enabled = true;
            SendDataButton.Enabled = false;
        }
        
        private void previewRefleshTimer_Tick(object sender, EventArgs e)
        {
            convertButton_Click(null, null);
        }
    }
}
