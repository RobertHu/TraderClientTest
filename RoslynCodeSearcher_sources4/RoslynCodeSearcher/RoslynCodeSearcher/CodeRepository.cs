using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Roslyn.Services;
using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;
using System.Windows.Forms;

namespace RoslynCodeSearcher
{
    public static class CodeRepository
    {
        private static List<string> _solutions = new List<string>();
        private static List<IWorkspace> _workspaces = new List<IWorkspace>();
        private static string _startingPath;

        #region Properties

        /// <summary>
        /// The startingpath for the coderepository to search for solutions
        /// </summary>
        public static string StartingPath
        {
            get { return CodeRepository._startingPath; }
            set { CodeRepository._startingPath = value; }
        }

        public static List<IWorkspace> Workspaces
        {
            get
            {
                return _workspaces;
            }
            set
            {
                _workspaces = value;
            }
        }

        public static List<string> Solutions
        {
            get
            {
                return _solutions;
            }
            set
            {
                _solutions = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// From a list of paths to solution (.sln) files, create a list of IWorkspace objects.
        /// </summary>
        /// <param name="solutions"></param>
        /// <returns></returns>
        public static List<IWorkspace> GetWorkspaces(List<string> solutions)
        {
            string currentSolution = "";
            List<IWorkspace> workspaces = new List<IWorkspace>();

            foreach (string solution in solutions)
            {
                //If a solution can not be loaded by Roslyn (because the solution type is not supported anymore),
                //we don't want the process to crash, just continue and write the path of the solution to a log file.
                try
                {
                    currentSolution = solution;
                    IWorkspace workspace = Workspace.LoadSolution(solution);
                    workspaces.Add(workspace);
                }
                catch (NotSupportedException nse)
                {
                    Logger.Log(currentSolution);
                }
                catch (Exception exc)
                {
                    Logger.Log("====================");
                    Logger.Log(exc.Message);
                    Logger.Log(exc.StackTrace);
                    Logger.Log(exc.InnerException.Message);
                }
            }

            return workspaces;
        }

        /// <summary>
        /// Get paths to solution files from a "solutions.txt" file.
        /// Put them in a list of strings.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSolutions(string startingPath)
        {
            //The directory to start from
            string currentDir = startingPath;

            List<string> solutions = new List<string>();
            using (StreamReader file = new StreamReader(startingPath))
            {
                string line = "";
                while ((line = file.ReadLine()) != null)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        solutions.Add(line);
                    }
                }
            }

            return solutions;
        }

        /// <summary>
        /// Search recursively through directories for .sln files, starting with startDir
        /// </summary>
        /// <param name="sDir"></param>
        public static void DirSearch(string startDir, bool firstTime)
        {
            try
            {
                if (firstTime)
                {
                    foreach (string f in Directory.GetFiles(startDir, "*.sln"))
                    {
                        _solutions.Add(f);
                    }
                    firstTime = false;
                }
                foreach (string d in Directory.GetDirectories(startDir))
                {
                    foreach (string f in Directory.GetFiles(d, "*.sln"))
                    {
                        _solutions.Add(f);
                    }
                    DirSearch(d, firstTime);
                }
            }
            catch (System.Exception excpt)
            {
                throw excpt;
            }
        }

        /// <summary>
        /// Search for solutions and create a list of Workspaces for the solutions.
        /// </summary>
        /// <param name="startingPath"></param>
        /// <returns></returns>
        public static List<IWorkspace> RefreshWorkspaces()
        {
            //After this call the Util.Solutions property is filled.
            DirSearch(_startingPath, true);
            //Get all the found .sln files.
            _solutions = Solutions;

            //Load the solutions into workspaces. Non-compatible solution files 
            //(solution files < Visual Studio 2010,2008) will be discarded.
            return GetWorkspaces(_solutions);
        }

        /// <summary>
        /// Create a solutions.txt file with the provided workspaces list
        /// and write it to the provided baseDirectorySolutionsTxtPath.
        /// </summary>
        /// <param name="workspaces"></param>
        /// <param name="baseDirectorySolutionsTxtPath"></param>
        public static void CreateSolutionsTxtFile(List<IWorkspace> workspaces, string baseDirectorySolutionsTxtPath)
        {
            using (StreamWriter writer = new StreamWriter(baseDirectorySolutionsTxtPath))
            {
                foreach (IWorkspace w in workspaces)
                {
                    //FilePath is the full path to the solution file.
                    writer.WriteLine(w.CurrentSolution.FilePath);
                }
            }
        }

        #endregion Methods

    }
}