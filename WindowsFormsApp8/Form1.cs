using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Media;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TimeSpan begin = new TimeSpan(8, 45, 0);
        TimeSpan end = new TimeSpan(18, 45, 0);
        DateTime dt = DateTime.Now;
        SoundPlayer pl = new SoundPlayer(Properties.Resources.Claps);

        private void button1_Click(object sender, EventArgs e)
        {
            if (DateTime.Now.TimeOfDay >= begin && DateTime.Now.TimeOfDay <= end)
            {
                label2.Text = dt.TimeOfDay.ToString();
                label1.Text = "Доброе утро!";
                pl.Play();
                pl.Dispose();
            }
            else
            {
                MessageBox.Show("Время не попадает в диапазон!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
