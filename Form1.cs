using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dosh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Install();
        }

        public void Install()
        {
            button1.Text = "Installing DOS#BASH...";
            Directory.CreateDirectory("C:\\dosh\\");
            // sudo, ls, la, cat, grep, PATH
            progressBar1.Value = 17;
            // SUDO
            string[] lines =
            {
                "@echo Set objShell = CreateObject(\"Shell.Application\") > %temp%\\sudo.tmp.vbs",
                "@echo args = Right(\"%*\", (Len(\"%*\") - Len(\"%1\"))) >> %temp%\\sudo.tmp.vbs",
                "@echo objShell.ShellExecute \"%1\", args, \"\", \"runas\" >> %temp%\\sudo.tmp.vbs",
                "@cscript %temp%\\sudo.tmp.vbs"
            };
            File.WriteAllLines("C:\\dosh\\sudo.cmd", lines);
            progressBar1.Value = 34;
            // LS
            File.WriteAllText("C:\\dosh\\ls.cmd", "@echo off\ndir /w");
            progressBar1.Value = 51;
            // LA
            File.WriteAllText("C:\\dosh\\la.cmd", "@echo off\ndir /r");
            progressBar1.Value = 68;
            // CAT
            File.WriteAllText("C:\\dosh\\cat.cmd", "@echo off\ntype %1");
            progressBar1.Value = 85;
            // PATH
            var name = "PATH";
            var scope = EnvironmentVariableTarget.Machine; // or User
            var oldValue = Environment.GetEnvironmentVariable(name, scope);
            var newValue = oldValue + @";C:\dosh\";
            Environment.SetEnvironmentVariable(name, newValue, scope);
            progressBar1.Value = 100;
            button1.Text = "Install DOS#BASH";
            MessageBox.Show("DOS#BASH successfully installed, restart any CMD windows for changes to take effect.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Remove();
        }

        public void Remove()
        {
            if (!Directory.Exists("C:\\dosh\\"))
            {
                MessageBox.Show("There's nothing to remove!");

            }
            else
            {
                button1.Text = "Removing DOS#BASH...";
                File.Delete("C:\\dosh\\cat.cmd");
                progressBar1.Value = 17;
                File.Delete("C:\\dosh\\la.cmd");
                progressBar1.Value = 34;
                File.Delete("C:\\dosh\\ls.cmd");
                progressBar1.Value = 51;
                File.Delete("C:\\dosh\\sudo.cmd");
                progressBar1.Value = 68;
                Directory.Delete("C:\\dosh\\");
                progressBar1.Value = 85;

                var name = "PATH";
                var scope = EnvironmentVariableTarget.Machine; // or User
                var oldValue = Environment.GetEnvironmentVariable(name, scope);
                var newValue = oldValue.Replace(@";C:\dosh\", "");
                Environment.SetEnvironmentVariable(name, newValue, scope);
                progressBar1.Value = 100;
                button1.Text = "Remove DOS#BASH";
                MessageBox.Show("DOS#BASH successfully removed, restart any CMD windows for changes to take effect.");
            }
        }
    }
}
