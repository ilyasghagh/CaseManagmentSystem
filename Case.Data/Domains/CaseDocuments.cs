using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Data.Domains
{
    public class CaseDocuments : BaseEntity
    {
        public string MimeType { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int CaseId { get; set; }
    }
}
