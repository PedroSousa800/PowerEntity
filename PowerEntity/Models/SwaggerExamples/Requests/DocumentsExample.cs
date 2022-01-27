using PowerEntity.Models.Entities;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Models.SwaggerExamples.Requests
{
    public class DocumentsExample : IExamplesProvider<List<Document>>
    {
        public List<Document> GetExamples()
        {
            var _documents = new List<Document>();

            _documents.Add(new Document("C", "Cédula Pessoal / Boletim de Nascimento", "778899"));
            _documents.Add(new Document("U", "Cartão Cidadão", "4420005"));

            return _documents;
        }
    }
}
