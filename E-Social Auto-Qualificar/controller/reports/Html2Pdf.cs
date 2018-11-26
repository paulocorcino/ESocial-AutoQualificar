using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace E_Social_Auto_Qualificar.controller.reports
{
    class Html2Pdf
    {
        

        public static bool ConvertHTML(string _srchtml, string _srcdst, bool isretrato = true)
        {
            string tmppdf = Path.GetTempFileName().Replace(".tmp",".pdf");
            string pdfHtmlToPdfExePath = Path.GetTempPath() + "wkhtmltopdf.exe";

            if (!File.Exists(pdfHtmlToPdfExePath))
            {
                //copia plugin
                File.Delete(Path.GetTempPath() + "wkhtmltopdf.exe");
                File.Copy("plugin/wkhtmltopdf.exe", Path.GetTempPath() + "wkhtmltopdf.exe");                
            }

            using (System.Diagnostics.Process p = new System.Diagnostics.Process())
            {

                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.FileName = String.Format("\"{0}\"",Path.GetFullPath(pdfHtmlToPdfExePath));
                //var src = Path.GetDirectoryName(startInfo.FileName);
                startInfo.Arguments = string.Format("\"{0}\" \"{1}\"", _srchtml, _srcdst);
                startInfo.UseShellExecute = true; // needs to be false in order to redirect output
                //startInfo.RedirectStandardOutput = true;
                //startInfo.RedirectStandardError = true;
                //startInfo.RedirectStandardInput = true;
                //startInfo.WorkingDirectory = Path.GetFullPath(startInfo.FileName);
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo = startInfo;

                //System.Windows.Forms.MessageBox.Show(startInfo.FileName + " " + startInfo.Arguments);

                p.Start();

                // read the output here...
                // var output = p.StandardOutput.ReadToEnd();
                //var errorOutput = p.StandardError.ReadToEnd();

                // ...then wait n milliseconds for exit (as after exit, it can't read the output)
                p.WaitForExit(60000);

                // read the exit code, close process
                int returnCode = p.ExitCode;
                p.Close();

                // if 0 or 2, it worked so return path of pdf
                if ((returnCode == 0) || (returnCode == 2))
                    return true;
                    //File.Move(tmppdf, _srcdst);
            }

            return false;
           
        }
    }
}
