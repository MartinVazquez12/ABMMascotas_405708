using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMMascotas
{
    public class Parametros
    {
		private string nombre;

		public string Nombre
		{
			get { return nombre; }
			set { nombre = value; }
		}

		private object valor;

		public object Valor
		{
			get { return valor; }
			set { valor = value; }
		}

		public Parametros(string nombre, object valor)
		{
			this.nombre = nombre;
			this.valor = valor;
		}

	}
}
