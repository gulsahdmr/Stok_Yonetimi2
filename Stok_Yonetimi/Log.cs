﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stok_Yonetimi
{
    public partial class Log : Form
    {
        private int kullaniciid;
        public Log(int kullaniciid)
        {
            InitializeComponent();
            this.kullaniciid = kullaniciid;
        }
    }
}
