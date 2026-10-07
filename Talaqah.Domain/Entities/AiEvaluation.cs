using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class AiEvaluation : BaseEntity
    {
        public int StudentResponseId { get; set; }
        public virtual StudentResponse StudentResponse { get; set; }
            = null!;
        public string ErrorAnalysis { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
    }
}
