using System;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace Questao1
{
     class ContaBancaria
    {
        private int Numero { get; set; }
        public string Titular { get; set; }
        private double DepositoInicial { get; set; }

        public ContaBancaria(int numero, string titular)
        {
            this.Numero = numero;
            this.Titular = titular;
            this.DepositoInicial = 0;   
        }

        public int GetNumero()
        {
            return this.Numero;
        }
        public double GetSaldo()
        {
            return this.DepositoInicial;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            this.Numero = numero;
            this.Titular = titular;
            this.DepositoInicial = depositoInicial;
        }

        internal void Deposito(double quantia)
        {
            this.DepositoInicial += quantia;
        }

        internal void Saque(double quantia)
        {
            //Para cada saque realizado, a instituição cobra uma taxa de $ 3.50.
            //Observação: a conta pode ficar com saldo negativo se o saldo não for suficiente para realizar o saque e / ou pagar a taxa.
            this.DepositoInicial -= quantia + 3.5;    
        }
    }
}
