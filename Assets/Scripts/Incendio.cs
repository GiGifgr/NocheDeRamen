using UnityEngine;

public class Incendio : Estado
{
    private string estadoPrevio;
    private float tiempo;

    public override void Entrar(string anterior)
    {
        estadoPrevio = anterior;
        tiempo = 0;

        int incidentes = Ctx.Obtener<int>("incidentes");
        Ctx.Fijar("incidentes", incidentes + 1);

        Debug.Log("Se incendio la estufa. Hay que apagarla.");
    }

    public override string Actualizar(float dt)
    {
        tiempo += dt;

        if (tiempo >= 4)
        {
            return estadoPrevio;
        }

        return null;
    }

    public override string AlRecibir(string trigger, int valor)
    {
        Debug.Log("Evento ignorado durante el incendio.");

        return null;
    }

    public override void Salir(string siguiente)
    {
        Debug.Log("El incidente se resolvio.");
    }
}