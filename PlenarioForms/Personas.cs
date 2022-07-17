using PlenarioForms.Datos;

namespace PlenarioForms
{
    public partial class Persona : Form
    {
        PersonasDatos per = new PersonasDatos();
        TelefonoDatos telefono = new TelefonoDatos();

        public Persona()
        {
            InitializeComponent();
            refreshGRIDPersonas();
            refreshText();
        }

        private void F_Load(object sender, EventArgs e)
        {
            //AplicationDbContext aplicationDbContext = new AplicationDbContext();

        }

        private void refreshGRIDPersonas()
        {
            dataGridView1.Rows.Clear();
            foreach (List<string> pers in per.obtenerPersonas())
                dataGridView1.Rows.Add(pers.ToArray());
        }
        private void refreshText()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Text = string.Empty;
            textBox4.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1[0, e.RowIndex].Value.ToString();
            textBox2.Text = dataGridView1[1, e.RowIndex].Value.ToString();
            dateTimePicker1.Text = dataGridView1[2, e.RowIndex].Value.ToString();
            textBox4.Text = dataGridView1[3, e.RowIndex].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("No se completaron los datos");
            }
            else if (textBox2 != null && dateTimePicker1.Text != null && textBox4.Text != null)
            {

                //Usuario usuarioAgregado = new Usuario(int.Parse(textBox2.Text), textBox3.Text, decimal.Parse(textBox4.Text));


                if (per.AgregarPersona(textBox2.Text,DateTime.Parse(dateTimePicker1.Text),decimal.Parse(textBox4.Text)))
                {

                    MessageBox.Show("Agregado con éxito");
                    refreshGRIDPersonas();
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
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("No se selecciono una persona");
            }
            else if (textBox1.Text != null && textBox2 != null && dateTimePicker1.Text != null && textBox4.Text != null)
            { 
           
                if (per.ModificarPersona(int.Parse(textBox1.Text),textBox2.Text, DateTime.Parse(dateTimePicker1.Text), decimal.Parse(textBox4.Text)))
            {
                MessageBox.Show("Modificado con éxito");
                refreshGRIDPersonas();
                refreshText();
            }
            else
            {
                MessageBox.Show("No se pudo modificar el persona");
            }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("No se selecciono una persona");
            }
            else if (textBox1.Text != null)
            {
                if (per.EliminarPersona(int.Parse(textBox1.Text)))
            {
                MessageBox.Show("Eliminado con éxito");
                refreshGRIDPersonas();
                refreshText();
            }
            else
                MessageBox.Show("No se pudo Eliminado el usuario");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("No hay datos de Personas para buscar el telefono");
            }
            else if(textBox1.Text != null)
            {
            this.Hide();

            PlenarioForms.Telefonos tel = new PlenarioForms.Telefonos();
            tel.label2.Text = textBox2.Text;
            tel.Show();
            //tel.label4.Visible = false;

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("No se completo el campo para buscar por nombre");
            }
            else if (textBox3 != null)
            {
                dataGridView1.Rows.Clear();
                foreach (List<string> pers in per.BuscarPersonas(textBox3.Text))
                    dataGridView1.Rows.Add(pers.ToArray());

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            refreshGRIDPersonas();
            refreshText();
        }
    }
}