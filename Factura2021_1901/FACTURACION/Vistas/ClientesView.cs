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
    public partial class ClientesView : Form
    {
        public ClientesView()
        {
            InitializeComponent();
            ClienteController controlador = new ClienteController(this);
        }
    }
}
