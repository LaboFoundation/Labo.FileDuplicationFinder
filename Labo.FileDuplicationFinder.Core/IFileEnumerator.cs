// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileEnumerator.cs" company="Labo">
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
//   Defines the IFileEnumerator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.FileDuplicationFinder.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// File enumerator interface.
    /// </summary>
    public interface IFileEnumerator
    {
        /// <summary>
        /// Enumerates the files in specified directory path recursively.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>Found file names.</returns>
        IEnumerable<string> EnumerateFiles(string directoryPath);
    }
}