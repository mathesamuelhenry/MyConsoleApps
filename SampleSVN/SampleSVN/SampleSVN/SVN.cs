using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace SampleSVN
{
    public class SVN
    {
        /*
         * Inputs Required 
         * - Username
         * - Password
         * - Checkout location
         */

        #region Properties

        public string Username { get; set; }
        public string Password { get; set; }
        public string SvnUrl { get; set; }
        public string Path { get; set; }

        public string ErrorMessage = string.Empty;
        public string OutputMessage = string.Empty;

        private ProcessStartInfo pStartInfo { get; set; }
        private Process process { get; set; }
        private string command { get; set; }
        private StreamReader ErrorStream { get; set; }
        private StreamReader OutputStream { get; set; }

        #endregion

        #region Constructors

        // Initialize SVN process paramaters
        private SVN()
        {
            this.Initialize();
        }

        /// Initialize a new SVN Client with default properties
        public SVN(string username, string password, string svnUrl, string path)
            : this()
        {
            this.Username = username;
            this.Password = password;
            this.SvnUrl = svnUrl;
            this.Path = path;
        }

        /// Initialize a new SVN Client with default properties. Store authentication information 
        public SVN(string username, string password)
            : this()
        {
            this.Username = username;
            this.Password = password;
        }

        #endregion

        #region Initiation Methods

        private void Initialize()
        {
            // Initialize ProcessStartInfo
            pStartInfo = new ProcessStartInfo();
            pStartInfo.RedirectStandardOutput = true;
            pStartInfo.RedirectStandardError = true;
            pStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pStartInfo.FileName = "svn.exe";
            pStartInfo.CreateNoWindow = true;
            pStartInfo.UseShellExecute = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Performs a checkout of SVN Url to Path
        /// </summary>
        public bool Checkout(out string errorMessage, out string outputMessage)
        {
            errorMessage = string.Empty;
            outputMessage = string.Empty;

            if (string.IsNullOrEmpty(this.SvnUrl))
            {
                errorMessage = "Svn Url is empty";
                return false;
            }

            if (string.IsNullOrEmpty(this.Path))
            {
                errorMessage = "Local file system path is empty";
                return false;
            }

            // Create directory (if not already exists)
            this.CreateDirectory(this.Path);

            this.command = string.Format(Constants.SVNCheckoutCommand, this.SvnUrl, this.Path, this.Username, this.Password);

            this.Start(out errorMessage, out outputMessage);

            return true;
        }

        /// <summary>
        /// SVN Checkout Action
        /// </summary>
        public bool Checkout(string svnUrl, string path, out string errorMessage, out string outputMessage)
        {
            errorMessage = string.Empty;
            outputMessage = string.Empty;

            // Create directory (if not already exists)
            this.CreateDirectory(path);

            this.command = string.Format(Constants.SVNCheckoutCommand, svnUrl, path, this.Username, this.Password);

            this.Start(out errorMessage, out outputMessage);

            return true;
        }

        /// <summary>
        /// SVN Update Action
        /// </summary>
        public bool Update(out string errorMessage, out string outputMessage)
        {
            errorMessage = string.Empty;
            outputMessage = string.Empty;

            if (string.IsNullOrEmpty(this.Path))
            {
                errorMessage = "path is empty";
                return false;
            }

            this.Cleanup(this.Path, out errorMessage, out outputMessage);

            this.command = string.Format(Constants.SVNUpdateCommand, this.Path);

            this.Start(out errorMessage, out outputMessage);

            return true;
        }

        /// <summary>
        /// SVN Update Action
        /// </summary>
        public bool Update(string path, out string errorMessage, out string outputMessage)
        {
            this.Cleanup(path, out errorMessage, out outputMessage);

            this.command = string.Format(Constants.SVNUpdateCommand, path);

            this.Start(out errorMessage, out outputMessage);

            return true;
        }

        /// <summary>
        /// SVN Commit
        /// </summary>
        public bool Commit(string path, string message, out string errorMessage, out string outputMessage, out string revisionId)
        {
            revisionId = string.Empty;

            this.Cleanup(path, out errorMessage, out outputMessage);

            this.command = string.Format(Constants.SVNCommitCommand, path, message);

            this.Start(out errorMessage, out outputMessage);

            revisionId = this.GetRevisionIDFromMessage(outputMessage);

            return true;
        }

        /// <summary>
        /// SVN Add and Commit
        /// </summary>
        public bool AddAndCommit(string path, string message, out string errorMessage, out string outputMessage, out string revisionId)
        {
            revisionId = string.Empty;

            this.Cleanup(path, out errorMessage, out outputMessage);

            this.Add(path, out errorMessage, out outputMessage);

            this.command = string.Format(Constants.SVNCommitCommand, path, message);

            this.Start(out errorMessage, out outputMessage);

            revisionId = this.GetRevisionIDFromMessage(outputMessage);

            return true;
        }

        /// <summary>
        /// SVN Add Action
        /// </summary>
        public bool Add(string path, out string errorMessage, out string outputMessage)
        {
            this.command = string.Format(Constants.SVNAddCommand, path);

            this.Start(out errorMessage, out outputMessage);

            return true;
        }

        /// <summary>
        /// SVN Cleanup 
        /// </summary>
        public bool Cleanup(string path, out string errorMessage, out string outputMessage)
        {
            this.command = string.Format(Constants.SVNCleanupCommand, path);

            this.Start(out errorMessage, out outputMessage);

            return true;
        }

        private string GetRevisionIDFromMessage(string message)
        {
            string svnRevisionId = string.Empty;

            if (!string.IsNullOrEmpty(message))
            {
                string[] msgLines = message.Split(Constants.lineDelimiter, StringSplitOptions.None);

                foreach (string line in msgLines)
                {
                    if (line.StartsWith(Constants.CommitedRevisionStartString))
                    {
                        svnRevisionId = line.Replace(Constants.CommitedRevisionStartString, string.Empty).TrimEnd(new char[] { '.' }).Trim();
                    }
                }

                return svnRevisionId;
            }

            return string.Empty;
        }

        // Start the Process
        private void Start(out string errorMessage, out string outputMessage)
        {
            errorMessage = string.Empty;
            outputMessage = string.Empty;
            ErrorStream = null;
            OutputStream = null;

            try
            {
                pStartInfo.Arguments = this.command;
                process = new Process();
                process.StartInfo = pStartInfo;
                process.Start();

                ErrorStream = process.StandardError;
                OutputStream = process.StandardOutput;

                errorMessage = ErrorStream.ReadToEnd();
                outputMessage = OutputStream.ReadToEnd();

                process.WaitForExit();
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
            }
            finally
            {
                if (!process.HasExited)
                    process.Close();
            }
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        #endregion
    }
}
