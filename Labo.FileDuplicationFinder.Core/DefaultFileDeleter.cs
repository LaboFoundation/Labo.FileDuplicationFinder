// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultFileDeleter.cs" company="Labo">
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
//   Defines the DefaultFileDeleter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.FileDuplicationFinder.Core
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualBasic.FileIO;

    /// <summary>
    /// The default file delete manager class.
    /// </summary>
    public sealed class DefaultFileDeleter : IFileDeleter
    {
        /// <summary>
        /// Deletes the specified files.
        /// </summary>
        /// <param name="files">The files.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public void DeleteFiles(IEnumerable<DeleteFileInfo> files)
        {
            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            foreach (DeleteFileInfo deleteFileInfo in files)
            {
                try
                {
                    FileSystem.DeleteFile(deleteFileInfo.FileName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                    deleteFileInfo.Deleted = true;
                }
                catch (Exception ex)
                {
                    deleteFileInfo.Deleted = false;
                    deleteFileInfo.ErrorMessage = ex.Message;
                }
            }
        }
    }
}