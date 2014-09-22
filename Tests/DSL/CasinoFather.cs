
namespace Tests.DSL
{
    public class CasinoFather
    {
        private Casino casino = new Casino();

        public static implicit operator Casino(CasinoFather father)
        {
            return father.casino;
        }
    }
}