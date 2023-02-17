using System.Management;
using System.Net;
using System.Reflection;


namespace NuevoOksanita
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ObtenerUsuarioEquipo();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ObtenerUnidades();

          
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            ObtenerIPs();
        }
       
        private void button4_Click_1(object sender, EventArgs e)
        {
            ComprobarPCEnCArga();
        }
  
        private void button5_Click(object sender, EventArgs e)
        {
            ObtenerGestionMemoriaRam();
        }

        private void ObtenerUsuarioEquipo()
        {
            string usuario = SystemInformation.UserName;
            string dominio = SystemInformation.UserDomainName;

            textBox1.Text = "Usuario: " + usuario + Environment.NewLine + "Dominio/Equipo: " + dominio;

        }

        private void ObtenerUnidades()
        {
            DriveInfo[] drives = DriveInfo
                .GetDrives()
                .Where(disco => disco.DriveType == DriveType.Fixed)
                .ToArray();

            foreach (DriveInfo drive in drives)
            {
                double espacioLibre = drive.TotalFreeSpace;
                double espacioTotal = drive.TotalSize;

                double espacioLibrePorcentaje = (espacioLibre / espacioTotal) * 100;

                textBox2.Text = drive.Name + ": " + espacioLibrePorcentaje + " % " + Environment.NewLine;
            }
        }

        private void ObtenerIPs()
        {
            IPAddress[] direciones = Dns.GetHostAddresses(Dns.GetHostName())
             .Where(a => !a.IsIPv6LinkLocal)
             .ToArray();
            
            foreach(IPAddress ip in direciones)
            {
               textBox3.Text  = "La IP es: " + ip.ToString();
            }
        }

        private void ComprobarPCEnCArga()
        {
            Type pw =typeof(PowerStatus);

            PropertyInfo[] propiedades = pw.GetProperties();

            object? valor = propiedades[0].GetValue(SystemInformation.PowerStatus, null);

            textBox4.Text = valor.ToString();
        }

        public void ObtenerGestionMemoriaRam()
        {
            ObjectQuery objectQuery= new ("SELECT * FROM Win32_OperatingSystem");

            ManagementObjectSearcher managementObject = new(objectQuery);
            ManagementObjectCollection collection = managementObject.Get();
            foreach (ManagementObject elemento in collection)
            {
      
               decimal memoriaTotal =
                  Math.Round ( Convert.ToDecimal( elemento["TotalVisibleMemorySize"]) / (1024*1024), 2);

                decimal memoriaLibre =
                    Math.Round(Convert.ToDecimal(elemento["FreePhysicalMemory"]) / (1024 * 1024), 2);

                textBox5.Text =  "Memoria libre: " + memoriaLibre;            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

    }
}