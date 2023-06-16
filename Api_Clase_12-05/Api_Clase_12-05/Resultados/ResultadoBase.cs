using System.Net;

namespace Api_Clase_12_05.Resultados;

public class ResultadoBase
{
    public bool Ok { get; set; } = true;
    public string MensajeError { get; set; } = "";
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    public void SetMensajeError(string mensajeError, HttpStatusCode statusCode)
    {
        Ok = false;
        MensajeError = mensajeError;
        StatusCode = statusCode;
    }
}