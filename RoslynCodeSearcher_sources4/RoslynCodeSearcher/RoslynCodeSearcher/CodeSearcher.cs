using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Roslyn.Services;
using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;
using System.Windows.Forms;
using System.Text;

namespace RoslynCodeSearcher
{
    public class CodeSearcher
    {
        private SearchType _searchType;
        private string _searchText;
        private string _exclude;
        private string _include;

        public CodeSearcher(SearchType searchType, string searchText, string exclude, string include)
        {
            _searchType = searchType;
            _searchText = searchText;
            _exclude = exclude;
            _include = include;
        }

        /// <summary>
        /// Search for the provided searchtext in the sourcecode files of the solutions.
        /// Use the provided SearchType (method, callers, text in method).
        /// Return the result in a string.
        /// </summary>
        /// <returns></returns>
        public string Search()
        {
            string result = "";

            List<string> excludes = CodeSearcher.GetFilters(_exclude);
            List<string> includes = CodeSearcher.GetFilters(_include);

            if (CodeRepository.Workspaces.Count() == 0)
            {
                //Get the solutions from the solutions.txt file and load them into Workspaces
                //If it doesn't exist, this will be checked at the moment user presses the [Search] button.
                CodeRepository.Solutions = CodeRepository.GetSolutions(Constants.BaseDirectorySolutionsTxtPath);

                CodeRepository.Workspaces = CodeRepository.GetWorkspaces(CodeRepository.Solutions);
            }

            if (_searchType == SearchType.SearchTextInMethod)
            {
                result = SearchMethodsForText(CodeRepository.Workspaces, _searchText, excludes, includes);
            }
            else if (_searchType == SearchType.SearchCallers)
            {
                result = SearchCallers(CodeRepository.Workspaces, _searchText, excludes, includes);
            }
            else if (_searchType == SearchType.SearchMethods)
            {
                result = SearchMethods(CodeRepository.Workspaces, _searchText, excludes, includes);
            }

            return result;
        }

        /// <summary>
        /// Search through the code for methods that contain the text textToSearch.
        /// Return the resulting method bodies as a string.
        /// filters are used to filter out files that have paths that contain certain words.
        /// includes are used to only include files that have paths that contain certain words.
        /// </summary>
        /// <param name="workspaces"></param>
        /// <param name="textToSearch"></param>
        /// <param name="filters">Projects / documents to filter by name</param>
        /// <param name="includes">Projects / documents to include by name</param>
        /// <returns></returns>
        public string SearchMethodsForText(List<IWorkspace> workspaces, string textToSearch, List<string> filters, List<string> includes)
        {
            string result = "";

            foreach (IWorkspace w in workspaces)
            {
                ISolution solution = w.CurrentSolution;

                foreach (IProject project in solution.Projects)
                {
                    foreach (IDocument document in project.Documents)
                    {
                        //Filter and include document names containing certain words
                        if (!filters.Any(s => document.FilePath.ToUpper().Contains(s)) &&
                            (
                              includes.Count() == 0 || includes.Any(s => document.FilePath.ToUpper().Contains(s)))
                            )
                        {
                            CommonSyntaxTree syntax = document.GetSyntaxTree();
                            var root = (CompilationUnitSyntax)syntax.GetRoot();

                            var syntaxNodes = from methodDeclaration in root.DescendantNodes()
                                             .Where(x => x is MethodDeclarationSyntax || x is PropertyDeclarationSyntax)
                                              select methodDeclaration;

                            if (syntaxNodes != null && syntaxNodes.Count() > 0)
                            {
                                foreach (MemberDeclarationSyntax method in syntaxNodes)
                                {
                                    if (method != null)
                                    {
                                        string methodText = method.GetFullText();
                                        if (methodText.ToUpper().Contains(textToSearch.ToUpper()))
                                        {
                                            result += GetMethodOrPropertyText(method, document);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Search through the code for calls of a method with name textToSearch.
        /// Return the resulting method bodies as a string.
        /// filters are used to filter out files that have paths that contain certain words.
        /// includes are used to only include files that have paths that contain certain words.
        /// </summary>
        /// <param name="workspaces"></param>
        /// <param name="textToSearch"></param>
        /// <param name="filters">Projects / documents to filter by name</param>
        /// <param name="includes">Projects / documents to include by name</param>
        public string SearchCallers(List<IWorkspace> workspaces, string textToSearch, List<string> filters, List<string> includes)
        {
            string result = "";

            foreach (IWorkspace w in workspaces)
            {
                ISolution solution = w.CurrentSolution;

                foreach (IProject project in solution.Projects)
                {
                    foreach (IDocument document in project.Documents)
                    {
                        //Filter and include document names containing certain words
                        if (!filters.Any(s => document.FilePath.ToUpper().Contains(s)) &&
                            (
                              includes.Count() == 0 || includes.Any(s => document.FilePath.ToUpper().Contains(s)))
                            )
                        {
                            CommonSyntaxTree syntax = document.GetSyntaxTree();
                            var root = (CompilationUnitSyntax)syntax.GetRoot();

                            var invocationAccess = from invoc in root.DescendantNodes().OfType<InvocationExpressionSyntax>()
                                                   select invoc;

                            if (invocationAccess != null && invocationAccess.Count() > 0)
                            {
                                foreach (InvocationExpressionSyntax s in invocationAccess)
                                {
                                    ExpressionSyntax expr = s.Expression;
                                    if (expr is IdentifierNameSyntax)
                                    {
                                        if (((IdentifierNameSyntax)expr).PlainName == textToSearch)
                                        {
                                            var method = from m in s.Ancestors().Where(x => x is MethodDeclarationSyntax)
                                                         select m;

                                            if (method != null && method.Count() > 0)
                                            {
                                                result += GetMethodOrPropertyText(method.First(), document);
                                            }
                                        }
                                    }
                                }
                            }

                            var memberAccess = from memberAcc in root.DescendantNodes().Where(x => x is MemberAccessExpressionSyntax)
                                               where ((MemberAccessExpressionSyntax)memberAcc).Name.GetFullText() == textToSearch
                                               select memberAcc;

                            if (memberAccess != null && memberAccess.Count() > 0)
                            {
                                foreach (MemberAccessExpressionSyntax s in memberAccess)
                                {
                                    var method = from m in s.Ancestors().Where(x => x is MethodDeclarationSyntax || x is PropertyDeclarationSyntax)
                                                 select m;

                                    if (method != null && method.Count() > 0)
                                    {
                                        result += GetMethodOrPropertyText(method.First(), document);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Search through the code for Method declarations that match textToSearch.
        /// Return the resulting method bodies as a string.
        /// filters are used to filter out files that have paths that contain certain words.
        /// includes are used to only include files that have paths that contain certain words.
        /// </summary>
        /// <param name="workspaces"></param>
        /// <param name="textToSearch"></param>
        /// <param name="filters">Projects / documents to filter by name</param>
        /// <param name="includes">Projects / documents to include by name</param>
        public string SearchMethods(List<IWorkspace> workspaces, string textToSearch, List<string> filters, List<string> includes)
        {
            string result = "";

            foreach (IWorkspace w in workspaces)
            {
                ISolution solution = w.CurrentSolution;

                foreach (IProject project in solution.Projects)
                {
                    foreach (IDocument document in project.Documents)
                    {
                        //Filter and include document names containing certain words
                        if (!filters.Any(s => document.FilePath.ToUpper().Contains(s)) &&
                            (
                              includes.Count() == 0 || includes.Any(s => document.FilePath.ToUpper().Contains(s)))
                            )
                        {
                            CommonSyntaxTree syntax = document.GetSyntaxTree();
                            var root = (CompilationUnitSyntax)syntax.GetRoot();

                            var methods = from method in root.DescendantNodes().Where(x => x is MethodDeclarationSyntax)
                                          where ((MethodDeclarationSyntax)method).Identifier.ValueText == textToSearch
                                          select method;

                            if (methods != null && methods.Count() > 0)
                            {
                                foreach (SyntaxNode m in methods)
                                {
                                    result += GetMethodOrPropertyText(m, document);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Get the full text of the method or property body.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        private string GetMethodOrPropertyText(SyntaxNode node, IDocument document)
        {
            StringBuilder resultStringBuilder = new StringBuilder();

            string methodText = node.GetFullText();

            object methodName = node is MethodDeclarationSyntax ? ((MethodDeclarationSyntax)node).Identifier.Value : ((PropertyDeclarationSyntax)node).Identifier.Value;
            resultStringBuilder.AppendLine("//=====================================================================================");
            resultStringBuilder.AppendLine(document.FilePath);
            resultStringBuilder.AppendLine("Method: " + (string)methodName);
            resultStringBuilder.AppendLine(methodText);

            return resultStringBuilder.ToString();
        }

        /// <summary>
        /// If the user provided filters in the filters textbox,
        /// split the string at the "," character.
        /// put the resulting filters in a list of strings
        /// </summary>
        /// <param name="filterText"></param>
        /// <returns></returns>
        public static List<string> GetFilters(string filterText)
        {
            List<string> filters = new List<string>();

            //Remove spaces
            filterText = filterText.Replace(" ", "");
            if (!String.IsNullOrEmpty(filterText))
            {
                //Split string on character , and put pieces in a list of strings
                filters = (from t in filterText.Split(',') select t.ToUpper()).ToList();
            }

            return filters;
        }
    }
}