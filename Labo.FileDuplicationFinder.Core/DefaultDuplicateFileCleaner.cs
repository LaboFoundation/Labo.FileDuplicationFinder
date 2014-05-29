// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultDuplicateFileCleaner.cs" company="Labo">
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
//   Defines the DefaultDuplicateFileCleaner type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.FileDuplicationFinder.Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The default duplicate file cleaner.
    /// </summary>
    public sealed class DefaultDuplicateFileCleaner : IDuplicateFileCleaner
    {
        /// <summary>
        /// The duplicate file finder.
        /// </summary>
        private readonly IDuplicateFileFinder m_DuplicateFileFinder;
        
        /// <summary>
        /// The file delete manager.
        /// </summary>
        private readonly IFileDeleter m_FileDeleter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDuplicateFileCleaner"/> class.
        /// </summary>
        /// <param name="duplicateFileFinder">The duplicate file finder.</param>
        /// <param name="fileDeleter">The file delete manager.</param>
        public DefaultDuplicateFileCleaner(IDuplicateFileFinder duplicateFileFinder, IFileDeleter fileDeleter)
        {
            m_DuplicateFileFinder = duplicateFileFinder;
            m_FileDeleter = fileDeleter;
        }

        /// <summary>
        /// Clears the duplicated files for the specified directory path.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>Deleted file info collection.</returns>
        public IList<DeleteFileInfo> ClearDuplicates(string directoryPath)
        {
            IDictionary<string, List<FileInfo>> duplicates = m_DuplicateFileFinder.FindDuplicates(directoryPath);
            List<DeleteFileInfo> filesToBeDeleted = new List<DeleteFileInfo>(duplicates.Count);

            foreach (KeyValuePair<string, List<FileInfo>> duplicate in duplicates)
            {
                if (duplicate.Value.Count <= 1)
                {
                    continue;
                }

                FileInfo[] files = duplicate.Value.Skip(1).ToArray();
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo fileInfo = files[i];
                    filesToBeDeleted.Add(new DeleteFileInfo(fileInfo.FullName));
                }
            }

            m_FileDeleter.DeleteFiles(filesToBeDeleted);

            return filesToBeDeleted;
        }
    }
}