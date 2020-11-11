using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SexShop
{
    class Juguetes
    {
        int codigo;
        string descripcion;
        int tipo;
        double precio;
        int medida;
        int color;

        public int pCodigo { get => codigo; set => codigo = value; }
        public string pDescripcion { get => descripcion; set => descripcion = value; }
        public int pTipo { get => tipo; set => tipo = value; }
        public double pPrecio { get => precio; set => precio = value; }
        public int pMedida { get => medida; set => medida = value; }
        public int pColor { get => color; set => color = value; }

        public Juguetes()
        {
            codigo = 0;
            descripcion = "";
            tipo = 0;
            precio = 0;
            medida = 0;
            color = 0;
        }

        //a este ctor no le puse codigo por si lo quieren usar para el insertar nuevo
        public Juguetes(int cod,string descrip, int tipo, double pre, int med, int col)
        {
            codigo = cod;
            descripcion = descrip;
            this.tipo = tipo;
            precio = pre;
            medida = med;
            color = col;
        }

        public override string ToString()
        {
            return codigo + " - " + descripcion;
        }
    }
}
