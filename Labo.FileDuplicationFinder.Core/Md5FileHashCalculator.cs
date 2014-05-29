// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Md5FileHashCalculator.cs" company="Labo">
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
//   Defines the Md5FileHashCalculator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.FileDuplicationFinder.Core
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Md5 file hash calculator class.
    /// </summary>
    public sealed class Md5FileHashCalculator : IFileHashCalculator, IDisposable
    {
        /// <summary>
        /// The md5 crypto service provider
        /// </summary>
        private MD5CryptoServiceProvider m_MD5CryptoServiceProvider;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool m_Disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5FileHashCalculator"/> class.
        /// </summary>
        public Md5FileHashCalculator()
        {
            m_MD5CryptoServiceProvider = new MD5CryptoServiceProvider();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Md5FileHashCalculator"/> class.
        /// </summary>
        ~Md5FileHashCalculator()
        {
            Dispose(false);
        }

        /// <summary>
        /// Computes the hash for the specified file data.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <returns>The file hash.</returns>
        public byte[] ComputeHash(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException("fileInfo");
            }

            using (FileStream fileStream = fileInfo.OpenRead())
            {
                return m_MD5CryptoServiceProvider.ComputeHash(fileStream);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (m_Disposed)
            {
                return;
            }

            if (disposing)
            {
                m_MD5CryptoServiceProvider.Dispose();
                m_MD5CryptoServiceProvider = null;
                m_Disposed = true;
            }
        }
    }
}
