using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Settings
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; }

        public string BlobName { get; set; }

        public string ContainerName { get; set; }
    }
}
