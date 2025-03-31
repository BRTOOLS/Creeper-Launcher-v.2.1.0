using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Creeper_Launcher_v._2._1._0
{
    public partial class Form1: Form
    {
        private string username; // Adiciona a variável username como campo da classe
        private string appDataPath; // Adiciona a variável appDataPath como campo da classe
        private string minecraftPath; // Adiciona a variável minecraftPath como campo da classe

        public Form1()
        {
            InitializeComponent();
            PuxarVersions();
            appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // pega o caminho da pasta appdata
            minecraftPath = Path.Combine(appDataPath, ".minecraft"); // junta o caminho da pasta appdata com a pasta .minecraft
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // bloqueia o resize

        }
        private async void PuxarVersions() // função para puxar as versões do minecraft
        {
            var launcher = new MinecraftLauncher(); // cria uma var para o launcher
            var versions = await launcher.GetAllVersionsAsync(); // puxa todas as versoes da api
            foreach (var item in versions) // para cada item na lista de versoes ele adiciona no combobox
            {
                comboBox1.Items.Add(item.Name); // mostra na combobox
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            MinecraftPath myPath = new MinecraftPath("./games"); // cria uma pasta no projeto chamada games
            MinecraftLauncher launcher = new MinecraftLauncher(myPath); // cria um launcher
            string version = null; // cria uma variavel version
            if (comboBox1.SelectedIndex != -1) // se o index do combobox for diferente de -1 ele pega a versao do installasync exibe e instala
            {
                version = comboBox1.SelectedItem.ToString();
                await launcher.InstallAsync(version);
            }
            else // mas se tu não tiver selecionado nada  ele exibe uma mensagem de erro
            {
                MessageBox.Show("Por favor, selecione uma versão.");
                return;
            }
            var minecraftProcess = await launcher.BuildProcessAsync(version, new MLaunchOption
            {
                Session = MSession.CreateOfflineSession(username), // cria uma sessão offline  
                GameLauncherName = "CreeperLauncher", // nome do launcher  

            });

            
            

            minecraftProcess.Start(); // inicia o minecraft
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            username = textBox1.Text; // pega o texto do textbox e coloca na variavel username

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", minecraftPath); // abre o explorer na pasta .minecraft

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.example.com"));

        }
    }
}
