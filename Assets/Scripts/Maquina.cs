using System.Collections.Generic;

public class Maquina
{
    public string trigger;
    public string destino;
    public Dictionary<string, Estado> estados = new Dictionary<string, Estado>();
    public Contexto Ctx { get; set; }
    public Estado Actual { get; set; }

    public Maquina(List<Estado> lista, Contexto ctx)
    {
        Ctx = ctx;

        foreach (var estado in lista)
        {
            estado.Ctx = ctx;
            estados[estado.Nombre] = estado;
        }
    }

    public void Iniciar(string nombre)
    {
        CambiarA(nombre);
    }

    public void Actualizar(float dt)
    {
        if (Actual == null)
        {
            return;
        }

        destino = Actual.Actualizar(dt);

        if (destino == null || destino == Actual.Nombre)
        {
            return;
        }

        CambiarA(destino);
    }

    public void Disparar(string nuevoTrigger, int valor)
    {
        if (Actual == null)
        {
            return;
        }

        trigger = nuevoTrigger;
        destino = Actual.AlRecibir(trigger, valor);

        if (destino == null || destino == Actual.Nombre)
        {
            return;
        }

        CambiarA(destino);
    }

    public void CambiarA(string nombre)
    {
        if (estados.ContainsKey(nombre) == false)
        {
            return;
        }

        string anterior = null;

        if (Actual != null)
        {
            anterior = Actual.Nombre;
            Actual.Salir(nombre);
        }

        Actual = estados[nombre];
        Actual.Entrar(anterior);
    }
}