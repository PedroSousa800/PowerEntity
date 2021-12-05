using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Model
{
    public class Document
    {
        public string documentTypeCode { get; set; }
        public string documentTypeDescription { get; set; }
        public string documentNumber { get; set; }
        public Document(string documentTypeCode, string documentTypeDescription, string documentNumber)
        {
            this.documentTypeCode = documentTypeCode;
            this.documentTypeDescription = documentTypeDescription;
            this.documentNumber = documentNumber;
        }
    }
 
}
