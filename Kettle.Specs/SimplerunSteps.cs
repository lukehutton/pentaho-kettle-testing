using System.IO;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Spike.Specs
{
    [Binding]
    public class SimplerunSteps
    {
        private string _testName;

        [Given(@"I have an input file and expected file for test ""(.*)""")]
        public void GivenIHaveAnInputFileAndExpectedFileForTest(string testName)
        {
            _testName = testName;
            Assert.That(File.Exists(TestFolders.GetInputFilePath(_testName)), "Missing input file");
            Assert.That(File.Exists(TestFolders.GetExpectedFilePath(_testName)), "Missing expected results file");
        }

        [When("I run the transform")]
        public void WhenIRunTheTransform()
        {
            string baseJobDirectory = TestFolders.GetBaseJobFilePath();
            string expectedFile = TestFolders.GetRelativeExpectedFilePath(_testName);
            string inputFile = TestFolders.GetRelativeInputFilePath(_testName);

            CommandExecutor.Execute(string.Format("run-kettle.bat {0} {1} {2} {3}",
                                                  baseJobDirectory,
                                                  expectedFile,
                                                  inputFile,
                                                  _testName));
        }

        [Then("the output file should match the expected file")]
        public void ThenTheOutputFileShouldMatchTheExpectedFile()
        {
            var expectedFile = TestFolders.GetExpectedFilePath(_testName);
            var expectedContent = new StreamReader(expectedFile).ReadToEnd();
            var outputFile = TestFolders.GetOutputFilePath(_testName);
            var outputContent = new StreamReader(outputFile).ReadToEnd();
            Assert.That(outputContent, Is.EqualTo(expectedContent));
        }
    }
}