using System.ComponentModel.DataAnnotations.Schema;
using SharedLibrary.Statics;

namespace SharedLibrary.Attributes
{
    public class LangAttr
    {
        public string Ru { get; set; }
        public string Uzb { get; set; }
        public string Taj { get; set; }
        public string Kaa { get; set; }

        [NotMapped]
        public string Current
        {
            get
            {
                switch (Thread.CurrentThread.CurrentCulture.Name)
                {
                    case Language.RU: return Ru;
                    case Language.UZB: return Uzb;
                    case Language.TAJ: return Taj;
                    case Language.KAA: return Kaa;
                    default:
                        return Ru;
                }
            }

        }
    }
}
