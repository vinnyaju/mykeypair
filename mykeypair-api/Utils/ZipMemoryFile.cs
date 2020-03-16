using mykeypair_api.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace mykeypair_api.Utils
{
    public class ZipMemoryFile
    {
        public static byte[] ZipItAllFromAndToMemory(CompressionLevel level, params MemoryFile[] memoryFiles)
        {
            using (MemoryStream zipMemStream = new MemoryStream())
            {
                using (ZipArchive zipResult = new ZipArchive(zipMemStream, ZipArchiveMode.Create, true))
                {

                    foreach (MemoryFile item in memoryFiles)
                    {
                        ZipArchiveEntry zipPkItem = zipResult.CreateEntry(item.Name, level);
                        using (Stream entryStream = zipPkItem.Open())
                        {
                            entryStream.Write(item.Data);
                        }

                    }
                }
                return zipMemStream.ToArray();
            }
        }
    }
}
