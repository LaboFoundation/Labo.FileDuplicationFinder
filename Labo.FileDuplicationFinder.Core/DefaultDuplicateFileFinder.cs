// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultDuplicateFileFinder.cs" company="Labo">
//   The MIT License (MIT)
//   
//   Copyright (c) 2014 Bora Akgun
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy of
//   this software and associated documentation files (the "Software"), to deal in
//   the Software without restriction, including without limitation the rights to
//   use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//   the Software, and to permit persons to whom the Software is furnished to do so,
//   subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all
//   copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//   FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//   COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//   CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Defines the DefaultDuplicateFileFinder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.FileDuplicationFinder.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The default duplicate file finder.
    /// </summary>
    public sealed class DefaultDuplicateFileFinder : IDuplicateFileFinder
    {
        /// <summary>
        /// The file hash calculator.
        /// </summary>
        private readonly IFileHashCalculator m_FileHashCalculator;

        /// <summary>
        /// The file enumerator.
        /// </summary>
        private readonly IFileEnumerator m_FileEnumerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDuplicateFileFinder"/> class.
        /// </summary>
        /// <param name="fileHashCalculator">The file hash calculator.</param>
        /// <param name="fileEnumerator">The file enumerator.</param>
        public DefaultDuplicateFileFinder(IFileHashCalculator fileHashCalculator, IFileEnumerator fileEnumerator)
        {
            m_FileHashCalculator = fileHashCalculator;
            m_FileEnumerator = fileEnumerator;
        }

        /// <summary>
        /// Finds the duplicated files for the specified directory path.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>Duplicated file dictionary.</returns>
        public IDictionary<string, List<FileInfo>> FindDuplicates(string directoryPath)
        {
            IDictionary<string, List<FileInfo>> dictionary = new Dictionary<string, List<FileInfo>>(StringComparer.OrdinalIgnoreCase);
            foreach (string fileName in m_FileEnumerator.EnumerateFiles(directoryPath))
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Length == 0)
                {
                    continue;
                }

                byte[] fileHash = m_FileHashCalculator.ComputeHash(fileInfo);
                string fileHashString = BitConverter.ToString(fileHash);
                List<FileInfo> fileInfos;
                if (dictionary.TryGetValue(fileHashString, out fileInfos))
                {
                    fileInfos.Add(fileInfo);                    
                }
                else
                {
                    dictionary.Add(fileHashString, new List<FileInfo> { fileInfo });
                }
            }

            return dictionary.Where(x => x.Value.Count > 1).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}