namespace ByteBank.Argument
{
    public class MovimentoArgument
    {
        public int CodigoConta { get; set; }
        public int CodigoMovimento { get; set; }
        public decimal saldo_inicial { get; set; }
        public decimal saldo_atual { get; set; }
    }
}
