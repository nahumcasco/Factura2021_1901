﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FACTURACION.Controladores;

namespace FACTURACION.Vistas
{
    public partial class FacturaView : Form
    {
        public FacturaView()
        {
            InitializeComponent();
            FacturaController controlador = new FacturaController(this);
        }
    }
}
