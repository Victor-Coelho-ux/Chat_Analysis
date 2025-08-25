using ChatAnalysis.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAnalysis.Domain.Entities
{
    internal class ProductAnalysis
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public SentimentTypeEnum SentimentTypeId { get; set; }
        public int Count { get; set; }
    }
}
