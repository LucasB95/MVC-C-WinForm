using PlenarioForms.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlenarioForms
{
    public partial class Telefonos : Form
    {
        TelefonoDatos tel = new TelefonoDatos();
        public Telefonos()
        {
            InitializeComponent();
            refreshGRIDTelefono();
            refreshText();
            MessageBox.Show("Si no ve nada cargado presione el boton :" + " Actualizar vista");
        }

        private void refreshGRIDTelefono()
        {

            var lista = tel.obtenerTelefonos(label2.Text);


            dataGridView1.Rows.Clear();
            foreach (List<string> tels in lista)
            dataGridView1.Rows.Add(tels.ToArray());         

        }
        private void refreshText()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1[0, e.RowIndex].Value.ToString();
            textBox2.Text = dataGridView1[1, e.RowIndex].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(label2.Text) || string.IsNullOrEmpty(textBox1.Text) )
            {
                MessageBox.Show("No se completaron los datos");
            }
            else if (label2.Text != null && textBox1.Text != null)
            {
                if (tel.AgregarTelefono(label2.Text, textBox1.Text))
                {

                    MessageBox.Show("Agregado con éxito");
                    refreshGRIDTelefono();
                    refreshText();

                }
                else
                {
                    MessageBox.Show("Ocurrio un error");

                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label2.Text) || string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("No se selecciono un telefono");
            }
            else if (label2.Text != null && textBox1.Text != null)
            {
                if (tel.ModificarTelefono(int.Parse(textBox2.Text), textBox1.Text))
                {

                    MessageBox.Show("Modificado con éxito");
                    refreshGRIDTelefono();
                    refreshText();

                }
                else
                {
                    MessageBox.Show("Ocurrio un error");

                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("No se selecciono un telefono");
            }
            else if (textBox2.Text != null)
            { 

                if (tel.EliminarTelefono(int.Parse(textBox2.Text)))
            {
                MessageBox.Show("Eliminado con éxito");
                refreshGRIDTelefono();
                refreshText();
            }
            else
                MessageBox.Show("No se pudo Eliminado el telefono registrado");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            PlenarioForms.Persona form = new Persona();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            refreshGRIDTelefono();
            refreshText();
        }
    }
}
