using ParalelAsync.Database;
using ParalelAsync.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParalelAsync
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString;
        public Form1()
        {
            _connectionString = $"Server=localhost;Database=FarmaciaDB;Trusted_Connection=True;TrustServerCertificate=true;";
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                SQLServer sqlserver = new SQLServer(_connectionString);

                producto = sqlserver.Reader<Producto>("SELECT Id, Nombre, Descripcion FROM Productos WHERE Id=1");

                MessageBox.Show($"{producto.Id} {producto.Nombre} {producto.Descripcion}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
