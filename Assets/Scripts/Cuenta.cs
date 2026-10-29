using System.Collections.Generic;
using UnityEngine;

public class Cuenta : Estado
{
    private int total;

    public override void Entrar(string anterior)
    {
        total = 0;

        List<Tazon> orden = Ctx.ObtenerOCrear<List<Tazon>>("orden");

        foreach (Tazon renglon in orden)
        {
            total += renglon.costo;
            Debug.Log(renglon.Describir());
        }

        Debug.Log("Total: " + total + " pesos.");
        Ctx.Fijar("total", total);
    }

    public override string AlRecibir(string trigger, int valor)
    {
        if (trigger == "cobrar")
        {
            if (valor <= 0)
            {
                Debug.Log("Pago no valido.");
                return null;
            }

            if (valor < total)
            {
                Debug.Log("Pago insuficiente. Faltan " + (total - valor) + " pesos.");
                return null;
            }

            List<Tazon> orden = Ctx.ObtenerOCrear<List<Tazon>>("orden");
            Dictionary<string, int> vendidos = Ctx.ObtenerOCrear<Dictionary<string, int>>("vendidos");

            foreach (Tazon renglon in orden)
            {
                if (vendidos.ContainsKey(renglon.nombre) == false)
                {
                    vendidos[renglon.nombre] = 0;
                }

                vendidos[renglon.nombre]++;
            }

            int recaudado = Ctx.Obtener<int>("recaudado");
            Ctx.Fijar("recaudado", recaudado + total);

            int atendidos = Ctx.Obtener<int>("atendidos");
            Ctx.Fijar("atendidos", atendidos + orden.Count);

            orden.Clear();

            Debug.Log("Cambio: " + (valor - total) + " pesos.");

            return "Barra";
        }

        if (trigger == "incendio")
        {
            return "Incendio";
        }

        return null;
    }
}