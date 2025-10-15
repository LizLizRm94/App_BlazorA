namespace AppBlazor.Client.Services
{
    public class UtilService
    {
        public string obtenerImagen(byte[]? buffer)
        {
            if (buffer == null)
            {
                return "img/istockphoto-1186065957-612x612.jpg";
            }
            else
            {
                return "data:image/jpg;base64," + Convert.ToBase64String(buffer);
            }
        }
    }
}
