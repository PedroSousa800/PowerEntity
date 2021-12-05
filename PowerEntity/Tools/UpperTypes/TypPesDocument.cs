using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Tools.UpperTypes
{
    public class TypPesDocument
    {
    }

    public class TYP_PES_DOCUMENT
    {
        public string DOCUMENT_TYPE_CODE { get; set; }
        public string DOCUMENT_TYPE_DESCRIPTION { get; set; }
        public string DOCUMENT_NUMBER { get; set; }
        public TYP_PES_DOCUMENT()
        {

        }
        public TYP_PES_DOCUMENT(string documentTypeCode, string documentTypeDescription, string documentNumber)
        {
            this.DOCUMENT_TYPE_CODE = documentTypeCode;
            this.DOCUMENT_TYPE_DESCRIPTION = documentTypeDescription;
            this.DOCUMENT_NUMBER = documentNumber;

        }
    }

}
