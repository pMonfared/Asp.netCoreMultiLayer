using System;

namespace SampleFive.DomainLayer.Models
{
    public class BaseFile : BaseEntity
    {
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileContentType { get; set; }
        public string FileSize { get; set; }
        public string FileOnDs { get; set; }

        public ChooseFileTypeForUploadBaseFile FileTypeFilter { get; set; }
    }

    public enum ChooseFileTypeForUploadBaseFile
    {
        Images,
        Files
    }
}