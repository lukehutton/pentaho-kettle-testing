using System.IO;

namespace Spike.Specs
{
    internal static class TestFolders
    {
        private static string RootRepoPath
        {
            get { return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName; }
        }

        public static string GetBaseJobFilePath()
        {
            return RootRepoPath;
        }

        public static string GetInputFilePath(string testName)
        {
            return Path.Combine(
                RootRepoPath,
                "src",
                "test",
                "data",
                testName.ToLower(),
                "input.txt"
                );
        }

        public static string GetRelativeInputFilePath(string testName)
        {
            return Path.Combine(
                testName.ToLower(),
                "input.txt"
                );
        }

        public static string GetOutputFilePath(string testName)
        {
            return Path.Combine(
                RootRepoPath,
                "build",
                "output",
                testName.ToLower(),
                "output.txt"
                );
        }

        public static string GetExpectedFilePath(string testName)
        {
            return Path.Combine(
                RootRepoPath,
                "src",
                "test",
                "data",
                testName.ToLower(),
                "expected.txt"
                );
        }

        public static string GetRelativeExpectedFilePath(string testName)
        {
            return Path.Combine(
                testName.ToLower(),
                "expected.txt"
                );
        }
    }
}