using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RamenController : MonoBehaviour
{
    private Maquina maquina;
    private Contexto ctx;
    private string ultimo;

    private void Start()
    {
        Debug.Log("Teclas: 1/2/3 pedir | 4 id inexistente | P pagar 2000 | L pagar 20 | Z pagar 0 | C cancelar | I incendio | X cerrar | R reiniciar");

        ctx = new Contexto();
        CargarCatalogo();

        List<Estado> lista = new List<Estado>();

        lista.Add(new Barra());
        lista.Add(new Cocinando());
        lista.Add(new Cuenta());
        lista.Add(new Incendio());
        lista.Add(new Cierre());

        maquina = new Maquina(lista, ctx);
        maquina.Iniciar("Barra");

        ultimo = maquina.Actual.Nombre;
        Debug.Log("Estado actual: " + ultimo);
    }

    private void Update()
    {
        Keyboard teclado = Keyboard.current;

        if (teclado == null || maquina.Actual == null)
        {
            return;
        }

        if (teclado.digit1Key.wasPressedThisFrame)
        {
            maquina.Disparar("ordenar", 1);
        }

        if (teclado.digit2Key.wasPressedThisFrame)
        {
            maquina.Disparar("ordenar", 2);
        }

        if (teclado.digit3Key.wasPressedThisFrame)
        {
            maquina.Disparar("ordenar", 3);
        }

        if (teclado.digit4Key.wasPressedThisFrame)
        {
            maquina.Disparar("ordenar", 4);
        }

        if (teclado.pKey.wasPressedThisFrame)
        {
            maquina.Disparar("cobrar", 2000);
        }

        if (teclado.lKey.wasPressedThisFrame)
        {
            maquina.Disparar("cobrar", 20);
        }

        if (teclado.zKey.wasPressedThisFrame)
        {
            maquina.Disparar("cobrar", 0);
        }

        if (teclado.cKey.wasPressedThisFrame)
        {
            maquina.Disparar("cancelar", 0);
        }

        if (teclado.iKey.wasPressedThisFrame)
        {
            maquina.Disparar("incendio", 0);
        }

        if (teclado.xKey.wasPressedThisFrame)
        {
            maquina.Disparar("cerrar", 0);
        }

        if (teclado.rKey.wasPressedThisFrame)
        {
            maquina.Ctx.Limpiar();
            CargarCatalogo();
            maquina.CambiarA("Barra");
        }

        maquina.Actualizar(Time.deltaTime);

        if (maquina.Actual.Nombre != ultimo)
        {
            ultimo = maquina.Actual.Nombre;
            Debug.Log("Estado actual: " + ultimo);
        }
    }

    private void CargarCatalogo()
    {
        Dictionary<int, Platillo> catalogo = ctx.ObtenerOCrear<Dictionary<int, Platillo>>("catalogo");

        catalogo.Add(1, new Platillo("Shoyu", 90, 6, 1));
        catalogo.Add(2, new Platillo("Miso", 110, 8, 3));
        catalogo.Add(3, new Platillo("Tonkotsu", 140, 10, 2));
    }
}