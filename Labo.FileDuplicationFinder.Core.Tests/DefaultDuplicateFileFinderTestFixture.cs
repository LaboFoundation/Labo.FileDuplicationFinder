namespace Labo.FileDuplicationFinder.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultDuplicateFileFinderTestFixture
    {
        [Test]
        public void A()
        {
            string filesPath = Path.Combine(Environment.CurrentDirectory, "Files");

            IFileHashCalculator fileHashCalculator = Substitute.For<IFileHashCalculator>();
            fileHashCalculator.ComputeHash(null).ReturnsForAnyArgs(new byte[] { 1, 3 });

            IFileEnumerator fileEnumerator = Substitute.For<IFileEnumerator>();
            fileEnumerator.EnumerateFiles(filesPath).Returns(new List<string>
                                                                 {
                                                                     Path.Combine(filesPath, "EmptyFile.txt"),
                                                                     Path.Combine(filesPath, "TextFile1.txt"),
                                                                     Path.Combine(filesPath, "TextFile1-duplicate.txt")
                                                                 });

            DefaultDuplicateFileFinder defaultDuplicateFileFinder = new DefaultDuplicateFileFinder(fileHashCalculator, fileEnumerator);
            IDictionary<string, List<FileInfo>> duplicates = defaultDuplicateFileFinder.FindDuplicates(filesPath);
            duplicates.ToString();

            fileEnumerator.Received(1).EnumerateFiles(filesPath);

            byte[] computedHash = new Md5FileHashCalculator().ComputeHash(new FileInfo(Path.Combine(filesPath, "TextFile1.txt")));
            string hashStr = BitConverter.ToString(computedHash);
            hashStr.ToString();
        }
    }
}
