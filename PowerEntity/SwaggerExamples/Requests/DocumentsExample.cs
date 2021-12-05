using PowerEntity.Model;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.SwaggerExamples.Requests
{
    public class DocumentsExample : IExamplesProvider<List<Document>>
    {
        public List<Document> GetExamples()
        {
            var documents = new List<Document>();

            documents.Add(new Document("C", "Cédula Pessoal / Boletim de Nascimento", "778899"));
            documents.Add(new Document("U", "Cartão Cidadão", "4420005"));

            return documents;
        }
    }
}
